namespace MonopolyClone;

public static class GameFunctions
{
    private static (int Left, int Top, int Offset) _boardCursorPosition;

    public static (int Left, int Top, int Offset) BoardCursorPosition
    {
        get => _boardCursorPosition;
        set
        {
            Utility.ClearConsoleLines(BoardCursorPosition.Offset, Console.CursorTop, default);
            _boardCursorPosition = value;
        }
    }

    public enum Actions
    {
        Transfer = 0,
        Pay = 1,
        Ledger = 2,
    }
    public static void Main(Bank bank, Board board)
    {
        while (true)
        {
            board.PrintBoard();
            StartPrompts(bank);
            board.UpdateBoard(bank.Accounts.Select(i => i.Player).ToList());
        }
       
    }

    public static void StartPrompts(Bank bank)
    {
        Console.SetCursorPosition(0, BoardCursorPosition.Offset);
        
        while (true)
        {
            Console.SetCursorPosition(0, BoardCursorPosition.Offset);
            
            Utility.PrintTable(bank.Accounts.Select(i => i.Player.Name).ToArray(), bank.Accounts.Select(i => i.Amount.ToString()).ToArray());


            Console.WriteLine("\nActions: ");
            for (var i = 0; i < Enum.GetValuesAsUnderlyingType<Actions>().Length; i++)
            {
                Console.WriteLine($"{i}: {(Actions)i}");
            }

            var rawOption = Utility.GetStringFromUser("Please select an action to perform: ");
            try
            {
                if (rawOption is null) continue;
                var option = int.Parse(rawOption);
                var action = (Actions)option;

                switch (action)
                {
                    case Actions.Transfer:
                        TransferPrompts(bank);
                        break;
                    case Actions.Pay:
                        PayPrompts(bank);
                        break;
                    case Actions.Ledger:
                        bank.Ledger.PrintTransactions();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception _)
            {
                Console.WriteLine("Please provide a valid action number.");
            }
        }
    }

    public static void TransferPrompts(Bank bank)
    {

        int? playerTransferFrom = null;
        int? playerTransferTo = null;
        double? amount = null;
        var accounts = bank.Accounts;
        while (true)
        {
            Console.SetCursorPosition(0, BoardCursorPosition.Offset);
            
            while (true)
            {
                Console.SetCursorPosition(0, BoardCursorPosition.Offset);

                Console.WriteLine("\nPlayers: ");

                for (var i = 0; i < accounts.Count; i++)
                {
                    var account = accounts[i];
                    var player = account.Player;
                    Console.WriteLine($"{i}: {player.Name}");
                }

                var promptMessage = playerTransferFrom == null ? "from" : playerTransferTo == null ? "to":"";
                var rawOption = Utility.GetStringFromUser($"Please select player to transfer {promptMessage}: ");

                try
                {
                    if (rawOption is null) continue;
                    var option = int.Parse(rawOption);
                    if (playerTransferFrom == null)
                    {
                        playerTransferFrom = option;
                    } 
                    else
                    {
                        playerTransferTo = option;
                    }
                    if (playerTransferFrom != null && playerTransferTo != null) break;
                }
                catch (Exception _)
                {
                    Console.WriteLine("Please provide a valid player number.");
                }

            }
            while (true)
            {
                var rawOption = Utility.GetStringFromUser("Please enter an amount to transfer: ");

                try
                {
                    if (rawOption is null) continue;
                    var option = double.Parse(rawOption);
                    amount ??= option;
                }
                catch (Exception _)
                {
                    Console.WriteLine("Please provide a valid amount number/decimal.");
                }

                if (amount != null) break;
            }

            bank.Transfer(accounts[(int)playerTransferFrom], accounts[(int)playerTransferTo], (double)amount);
            Console.Clear();
            break;
        }
    }
    
     public static void PayPrompts(Bank bank)
    {
        Console.SetCursorPosition(0, BoardCursorPosition.Offset);

        int? playerTransferFrom = null;
        double? amount = null;
        var accounts = bank.Accounts;
        while (true)
        {
            Console.WriteLine("|--------------------------------------------------------------------------------------|");
            
            while (true)
            {
                Console.WriteLine("\nPlayers: ");

                for (var i = 0; i < accounts.Count; i++)
                {
                    var account = accounts[i];
                    var player = account.Player;
                    Console.WriteLine($"{i}: {player.Name}");
                }

              
                var rawOption = Utility.GetStringFromUser($"Please select player to transfer from: ");

                try
                {
                    if (rawOption is null) continue;
                    var option = int.Parse(rawOption);
                    playerTransferFrom ??= option; 
                    if (playerTransferFrom != null) break;
                }
                catch (Exception _)
                {
                    Console.WriteLine("Please provide a valid player number.");
                }

            }
            while (true)
            {
                var rawOption = Utility.GetStringFromUser("Please enter an amount to transfer: ");

                try
                {
                    if (rawOption is null) continue;
                    var option = double.Parse(rawOption);
                    amount ??= option;
                }
                catch (Exception _)
                {
                    Console.WriteLine("Please provide a valid amount number/decimal.");
                }

                if (amount != null) break;
            }

            bank.Pay(accounts[(int)playerTransferFrom], (double)amount);
            Console.Clear();
            break;
        }
    }
    
  
   
}
