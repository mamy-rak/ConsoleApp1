using Auction.Core;

namespace Auction.Infrastructure;

public class PrinterConsole : IPrinter
{
    public void Print(string message)
    {
        Console.WriteLine(message);
    }
}
