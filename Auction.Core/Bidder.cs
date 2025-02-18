namespace Auction.Core;

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
