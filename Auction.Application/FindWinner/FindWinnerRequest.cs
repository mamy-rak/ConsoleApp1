namespace Auction.Application.FindWinner;

public record FindWinnerRequest(Bidder[] bidders, int reservePrice)
{
    public Bidder[] Bidders { get; } = bidders;
    public int ReservePrice { get; } = reservePrice;
}

public record Bidder(string name, int[]? bids)
{
    public string Name { get; } = name;
    public int? MaxBid { get; } = bids?.Max();

    public override string ToString()
    {
        if (bids is null)
            return $"- Bidder {Name} with no bids";
        return $"- Bidder {Name} with bids [{string.Join(", ", bids.OrderBy(bid => bid))}]";
    }
}
