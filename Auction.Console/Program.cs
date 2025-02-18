using Auction.Application;
using Auction.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.RegisterApplicationServices();
serviceCollection.RegisterInfrastructureServices();
serviceCollection.AddSingleton<Auction.Interface.Console>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<Auction.Interface.Console>();
app.Run();