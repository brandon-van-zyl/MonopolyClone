using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Net.Mime;

namespace MonopolyClone;

public class Player
{
    public Guid Id { get; set; }

    public required string Name { get; set; } = string.Empty;
    
    [MaxLength(2)]
    public required char DisplayName { get; set; }

    public  Account Account { get; set; } = null!;

    public  Board.Coordinate Coordinate { get; set; } = new Board.Coordinate(0, 0);
    
    public required ConsoleColor Color { get; set; }
}

public class Account
{
    public Guid Id { get; set; }

    public Player Player { get; set; } = null!;
    
    public double Amount { get; set; }

}

public class Ledger
{
    public List<Transaction> Transactions { get; set; } = [];

    public void PrintTransactions()
    {
        Console.WriteLine("Transactions:");
        Utility.PrintTable(
            this.Transactions.Select(i => $"From: {i.From.Player.Name}, To: {i.To?.Player.Name}"),
            this.Transactions.Select(i => i.Amount.ToString(CultureInfo.InvariantCulture)));
        Utility.GetStringFromUser("Press enter to continue.");
    }
}

public class Transaction
{
    public Account From { get; set; } = null!;
        
    public Account? To { get; set; } = null!;

    public double Amount { get; set; }

    public Action Action { get; set; } = null!;

}

public class Bank
{
    public Bank(List<BankInput> inputs)
    {
        this.Id = Guid.NewGuid();
        
        this.Accounts = inputs.Select(i => new Account()
        {
            Id = Guid.NewGuid(),
            Player = new Player
            {
                Id = Guid.NewGuid(),
                Name = i.PlayerName,
                DisplayName = i.DisplayName,
                Color = (ConsoleColor)i.Color
            },
            Amount = i.InitialAmount
        }).ToList();
        this.Ledger = new Ledger();
    }
    public Guid Id { get; set; }

    public List<Account> Accounts { get; set; }

    public Ledger Ledger{ get; set; }
    
    public void Transfer(Account fromAccount, Account toAccount, double amount)
    {
        fromAccount.Amount -= amount;
        toAccount.Amount += amount;
        var transaction = new Transaction()
        {
            From = fromAccount,
            To = toAccount,
            Amount = amount
        };
        this.Ledger.Transactions.Add(transaction);
    }

    // item to pay for
    public void Pay(Account accountToPayFrom, double amount)
    {
        accountToPayFrom.Amount -= amount;
        
        var transaction = new Transaction()
        {
            From = accountToPayFrom,
            Amount = amount
        };
        this.Ledger.Transactions.Add(transaction);
    }

  
    
}
public record BankInput(double InitialAmount, string PlayerName, char DisplayName, int Color);