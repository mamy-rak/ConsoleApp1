using Auction.Application;
using Auction.Application.FindWinner;
using Auction.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.RegisterApplicationServices();
serviceCollection.RegisterInfrastructureServices();

var serviceProvider = serviceCollection.BuildServiceProvider();
var app = serviceProvider.GetRequiredService<FindWinnerHandler>();

app.Handle(new FindWinnerRequest([
    new Bidder("A", [110, 140]),
    new Bidder("B", [0]),
    new Bidder("C", [125]),
    new Bidder("D", [105, 115, 90]),
    new Bidder("E", [132, 135, 140]),
], 12));