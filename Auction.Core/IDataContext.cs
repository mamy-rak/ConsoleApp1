namespace Auction.Core;

public interface IDataContext
{
    void AddBidder(Bidder bidder);
    Bidder[] Bidders();
    void SetReservePrice(int reservePrice);
    int ReservePrice();
}
