using Auction.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Infrastructure;

public static class RegisterServices
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IPrinter, PrinterConsole>();
        services.AddSingleton<IDataContext, DataContextInMemory>();
    }
}