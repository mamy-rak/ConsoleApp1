using Auction.Core;

namespace Auction.Application.FindWinner;

public class FindWinnerHandler(IPrinter printer)
{
    public FindWinnerResponse Handle(FindWinnerRequest request)
    {
        //Validation
        if (request is null || request.bidders is null)
        {
            printer.Print("There is no winner");
            return new FindWinnerResponse(null, 0);
        }

        //Validation
        var bidders = request.Bidders
            .Where(bidder => bidder is not null)
            .Where(bidder => bidder.Name is not null)
            .Where(bidder => bidder.MaxBid is not null)
            .Where(bidder => bidder.MaxBid >= request.ReservePrice)
            .OrderByDescending(bidder => bidder.MaxBid)
            .ToArray();

        //Display inputs
        printer.Print($"Reserve price is : {request.ReservePrice}");
        printer.Print("Accepted Bidders are :");
        foreach (Bidder bidder in bidders)
            printer.Print(bidder.ToString());

        //Find Winning Bidder
        var winningBidder = FindWinningBidder(bidders);

        if (winningBidder is null)
        {
            printer.Print("There is no winner");
            return new FindWinnerResponse(null, request.ReservePrice);
        }

        //Find Winning Price
        var winningPrice = FindWinningPrice(bidders, winningBidder, request.ReservePrice);

        printer.Print($"Bidder {winningBidder.Name} wins the auction at the price of {winningPrice} euros");
        return new FindWinnerResponse(winningBidder.Name, winningPrice);
    }

    private Bidder? FindWinningBidder(Bidder[] bidders)
    {
        //No accepted bidder
        if (!bidders.Any())
            return null;

        if (bidders.Length == 1)
            return bidders.First();

        var highestBidders = bidders.Where(bidder => bidder.MaxBid == bidders[0].MaxBid).ToArray();
        if (highestBidders.Length == 1)
            return highestBidders.First();

        //If there are multiple highest bidders, the winning bidder is selected randomly
        return highestBidders[new Random().Next(highestBidders.Length)];
    }

    private int FindWinningPrice(Bidder[] bidders, Bidder? winnigBidder, int reservedPrice)
    {
        //No accepted bidder
        if (winnigBidder is null)
            return reservedPrice;

        var winningPrice = bidders.Select(bidder => bidder.MaxBid).FirstOrDefault(maxBid => maxBid != winnigBidder.MaxBid);

        //The reserve price itself is the winning price
        if (winningPrice is null || winningPrice < reservedPrice)
            winningPrice = reservedPrice;

        return winningPrice.Value;
    }
}
