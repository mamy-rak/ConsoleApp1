﻿using Auction.Application.AddBidder;
using Auction.Application.FindWinner;
using Auction.Application.SetReservePrice;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Application;

public static class RegisterServices
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<AddBidderHandler>();
        services.AddSingleton<FindWinnerHandler>();
        services.AddSingleton<SetReservePriceHandler>();
    }
}
