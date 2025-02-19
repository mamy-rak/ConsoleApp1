using Auction.Application.FindWinner;
using Auction.Core;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Acution.Test;

public class FindWinnerHandlerTest(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly FindWinnerHandler findWinnerHandler = fixture.ServiceProvider.GetRequiredService<FindWinnerHandler>();
    private readonly IDataContext dataContext = fixture.ServiceProvider.GetRequiredService<IDataContext>();
    private readonly Mock<IPrinter> mockPrinter = fixture.MockPrinter;

    //No winning bidder : no bid is equal to or higher than the reserve price
    [Fact]
    public void FindWinnerHandler_NoWinningBidder()
    {
        //Arrange
        dataContext.SetReservePrice(150);

        dataContext.ResetBidders();
        dataContext.AddBidder(new Bidder("A", [90, 80]));
        dataContext.AddBidder(new Bidder("B", [0]));
        dataContext.AddBidder(new Bidder("C", [95]));
        dataContext.AddBidder(new Bidder("D", [105, 115, 90]));
        dataContext.AddBidder(new Bidder("E", [90, 95, 100]));

        //Act
        findWinnerHandler.Handle();

        //Assert
        mockPrinter.Verify(p => p.Print("No accepted bidder"), Times.Once);
        mockPrinter.Reset();
    }

    //The winning bidder is determined as the one whose bid, higher than the reserve price, is the highest
    [Fact]
    public void FindWinnerHandler_HighestBid()
    {
        //Arrange
        dataContext.SetReservePrice(100);

        dataContext.ResetBidders();
        dataContext.AddBidder(new Bidder("A", [110, 120]));
        dataContext.AddBidder(new Bidder("B", [0]));
        dataContext.AddBidder(new Bidder("C", [125]));
        dataContext.AddBidder(new Bidder("D", [105, 115, 90]));
        dataContext.AddBidder(new Bidder("E", [132, 135, 140]));

        //Act
        findWinnerHandler.Handle();

        //Assert
        mockPrinter.Verify(p => p.Print("Winner : E"), Times.Once);
        mockPrinter.Reset();
    }

    //The winning bidder is determined as the one whose bid, equal to the reserve price
    [Fact]
    public void FindWinnerHandler_EqualToReservePriceBid()
    {
        //Arrange
        dataContext.SetReservePrice(140);

        dataContext.ResetBidders();
        dataContext.AddBidder(new Bidder("A", [110, 120]));
        dataContext.AddBidder(new Bidder("B", [0]));
        dataContext.AddBidder(new Bidder("C", [125]));
        dataContext.AddBidder(new Bidder("D", [105, 115, 90]));
        dataContext.AddBidder(new Bidder("E", [100, 135, 140]));

        //Act
        findWinnerHandler.Handle();

        //Assert
        mockPrinter.Verify(p => p.Print("Winner : E"), Times.Once);
        mockPrinter.Reset();
    }

    //The winning price is the highest bid placed by any non-winning bidder that is above the reserve price
    [Fact]
    public void FindWinnerHandler_HighestBidNonWinningBidder()
    {
        //Arrange
        dataContext.SetReservePrice(100);

        dataContext.ResetBidders();
        dataContext.AddBidder(new Bidder("A", [110, 120]));
        dataContext.AddBidder(new Bidder("B", [0]));
        dataContext.AddBidder(new Bidder("C", [125]));
        dataContext.AddBidder(new Bidder("D", [105, 115, 90]));
        dataContext.AddBidder(new Bidder("E", [132, 135, 140]));

        //Act
        findWinnerHandler.Handle();

        //Assert
        mockPrinter.Verify(p => p.Print("Winning price : 125"), Times.Once);
        mockPrinter.Reset();
    }

    //The winning price is the reserve price itself
    [Fact]
    public void FindWinnerHandler_ReservePrice()
    {
        //Arrange
        dataContext.SetReservePrice(139);

        dataContext.ResetBidders();
        dataContext.AddBidder(new Bidder("A", [110, 120]));
        dataContext.AddBidder(new Bidder("B", [0]));
        dataContext.AddBidder(new Bidder("C", [125]));
        dataContext.AddBidder(new Bidder("D", [105, 115, 90]));
        dataContext.AddBidder(new Bidder("E", [132, 135, 140]));

        //Act
        findWinnerHandler.Handle();

        //Assert
        mockPrinter.Verify(p => p.Print("Winning price : 139"), Times.Once);
        mockPrinter.Reset();
    }
}