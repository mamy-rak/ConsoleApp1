using Auction.Core;

namespace Auction.Infrastructure;

public class DataContextInMemory : IDataContext
{
    private readonly List<Bidder> _bidders = new();
    private int _reservePrice;

    public void ResetBidders()
    {
        _bidders.Clear();
    }

    public void AddBidder(Bidder bidder)
    {
        _bidders.Add(bidder);
    }

    public Bidder[] Bidders()
    {
        return _bidders.ToArray();
    }

    public void SetReservePrice(int reservePrice)
    {
        _reservePrice = reservePrice;
    }

    public int ReservePrice()
    {
        return _reservePrice;
    }
}
