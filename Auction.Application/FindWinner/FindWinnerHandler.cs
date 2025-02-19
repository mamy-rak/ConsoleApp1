using Auction.Core;

namespace Auction.Application.FindWinner;

public class FindWinnerHandler(IPrinter printer, IDataContext dataContext)
{
    private readonly IPrinter printer = printer;
    private readonly IDataContext dataContext = dataContext;

    public void Handle()
    {
        var reservePrice = dataContext.ReservePrice();
        var bidders = dataContext.Bidders()
            .Where(bidder => bidder is not null)
            .Where(bidder => bidder.Name is not null)
            .Where(bidder => bidder.MaxBid >= reservePrice)
            .OrderByDescending(bidder => bidder.MaxBid)
            .ToArray();

        //No accepted bidder
        if (bidders.Length == 0) 
        {
            printer.Print("No accepted bidder");
            return;
        }

        //Winning Bidder : the first bidder with highest bid
        var winningBidder = bidders[0];

        if (bidders.Length == 1)
        {
            printer.Print($"Winner : {winningBidder.Name}");
            printer.Print($"Winning price : {reservePrice}");
            return;
        }

        //Winning Price : the highest bid placed by any non-winning bidder
        var winningPrice = bidders[1].MaxBid;

        //The reserve price itself is the winning price
        if (!winningPrice.HasValue || winningPrice < reservePrice)
            winningPrice = reservePrice;

        printer.Print($"Winner : {winningBidder.Name}");
        printer.Print($"Winning price : {winningPrice.Value}");
    }
}
