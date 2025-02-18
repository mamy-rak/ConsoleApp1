using Auction.Core;

namespace Auction.Application.SetReservePrice;

public class SetReservePriceHandler(IDataContext dataContext, IPrinter printer)
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IPrinter printer = printer;

    public void Handle(SetReservePriceRequest? request)
    {
        if (request is null)
        {
            printer.Print("Request is null");
            return;
        }

        if (request.ReservePrice < 0) 
        {
            printer.Print("Reserve price cannot be negative");
            return;
        }

        printer.Print($"Setting reserve price to {request.ReservePrice}");
        dataContext.SetReservePrice(request.ReservePrice);
    }
}