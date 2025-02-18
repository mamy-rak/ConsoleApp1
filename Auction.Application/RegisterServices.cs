using Auction.Application.FindWinner;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Application;

public static class RegisterServices
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<FindWinnerHandler>();
    }
}
