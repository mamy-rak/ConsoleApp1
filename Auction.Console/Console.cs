using Auction.Application.AddBidder;
using Auction.Application.FindWinner;
using Auction.Application.SetReservePrice;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Interface;
internal class Console(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public void Run()
    {
        var setReservePriceHandler = serviceProvider.GetRequiredService<SetReservePriceHandler>();
        var addBidderHandler = serviceProvider.GetRequiredService<AddBidderHandler>();
        var findWinnerHandler = serviceProvider.GetRequiredService<FindWinnerHandler>();

        setReservePriceHandler.Handle(new SetReservePriceRequest(12));

        addBidderHandler.Handle(new AddBidderRequest("A", [110, 140]));
        addBidderHandler.Handle(new AddBidderRequest("B", [0]));
        addBidderHandler.Handle(new AddBidderRequest("C", [125]));
        addBidderHandler.Handle(new AddBidderRequest("D", [105, 115, 90]));
        addBidderHandler.Handle(new AddBidderRequest("E", [132, 135, 140]));

        findWinnerHandler.Handle();
    }
}