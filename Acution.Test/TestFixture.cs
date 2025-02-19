﻿using Auction.Application.FindWinner;
using Auction.Core;
using Auction.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Acution.Test;
public class TestFixture
{
    public IServiceProvider ServiceProvider { get; private set; }
    public Mock<IPrinter> MockPrinter { get; private set; }

    public TestFixture()
    {
        var serviceCollection = new ServiceCollection();

        MockPrinter = new Mock<IPrinter>();

        serviceCollection.AddSingleton<FindWinnerHandler>();
        serviceCollection.AddSingleton<IDataContext, DataContextInMemory>();
        serviceCollection.AddSingleton(MockPrinter.Object);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}
