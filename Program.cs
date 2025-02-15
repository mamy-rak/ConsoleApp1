var result = FindWinner([
    new Bidder("A", [110, 130]),
    new Bidder("B", [0]),
    new Bidder("C", [125]),
    new Bidder("D", [105, 115, 90]),
    new Bidder("E", [132, 135, 140]),
], 12);

Console.WriteLine(result.AnnounceWinner());

Result FindWinner(Bidder[] bidders, int reservePrice)
{
    //Validation
    bidders = bidders
        .Where(bidder => bidder is not null)
        .Where(bidder => bidder.Name is not null)
        .Where(bidder => bidder.MaxBid >= reservePrice)
        .OrderByDescending(bidder => bidder.MaxBid)
        .ToArray();

    //Display inputs
    Console.WriteLine($"Reserve price is : {reservePrice}");
    Console.WriteLine("Bidders are :");
    foreach (Bidder bidder in bidders)
        Console.WriteLine(bidder);
    
    //No accepted bidder
    if (bidders.Length == 0)
        return new Result(null, reservePrice);

    //Winning Bidder
    var winningBidder = bidders[0];

    //The highest bid placed by any non-winning bidder
    if (bidders.Length == 1)
        return new Result(winningBidder, reservePrice);

    //Winning Price
    var winningPrice = bidders[1].MaxBid;

    //The reserve price itself is the winning price
    if (winningPrice < reservePrice)
        winningPrice = reservePrice;

    return new Result(winningBidder, winningPrice);
}

record Bidder(string name, int[] bids)
{
    public string Name { get; } = name;
    public int MaxBid { get; } = bids.Max();

    public override string ToString()
    {
        return $"- Bidder {Name} with bids [{string.Join(", ", bids.OrderBy(bid => bid))}]";
    }
}

record Result(Bidder? winningBidder, int winningPrice)
{
    public string AnnounceWinner()
    {
        if (winningBidder is null)
            return "No winner";

        return $"Bidder {winningBidder.Name} wins the auction at the price of {winningPrice} euros";
    }
}