using Auction.Core;

namespace Auction.Application.AddBidder;

public class AddBidderHandler(IDataContext dataContext, IPrinter printer)
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IPrinter printer = printer;

    public void Handle(AddBidderRequest? request)
    {
        if (request is null)
        {
            printer.Print("Request is null");
            return;
        }

        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
        {
            printer.Print("Name cannot be null or empty");
            return;
        }

        if (request.Bids is null)
        {
            printer.Print("Bids cannot be empty");
            return;
        }

        var acceptedBids = request.Bids.Where(bid => bid >= 0).ToArray();

        printer.Print($"Adding bidder {request.Name} with bids {string.Join(", ", acceptedBids)}");

        var bidder = new Bidder(request.Name, acceptedBids);
        dataContext.AddBidder(bidder);
    }
}