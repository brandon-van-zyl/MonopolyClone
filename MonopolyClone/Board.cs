namespace MonopolyClone;

public class Board
{
    public List<Coordinate> PlayableBlocks { get; set; } = [];
    public static List<List<string>> board { get; set; } =
    [
        [" x ", " x ", " x ", " x ", " x ", " x ", " x ", " x ", " x ", " x ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", "   ", " x "],
        [" x ", " x ", " x ", " x ", " x ", " x ", " x ", " x ", " x ", " x ", " x "],
    ];

    public void PrintBoard()
    {
        Console.SetCursorPosition(GameFunctions.BoardCursorPosition.Left, GameFunctions.BoardCursorPosition.Top);
        var playableBlocks = new List<Coordinate>();
        for (var y = 0; y < board.Count; y++){
            for (var x = 0; x < board[y].Count; x++)
            {
                var currentBlock = board[y][x];
                if (currentBlock.Contains('x'))
                {
                    playableBlocks.Add(new Coordinate(x, y));
                }
                Console.Write(currentBlock.PadLeft(currentBlock.Length));
            }
            Console.WriteLine();
        }

        PlayableBlocks = playableBlocks;
        GameFunctions.BoardCursorPosition = (
            GameFunctions.BoardCursorPosition.Left,
            GameFunctions.BoardCursorPosition.Top,
            Console.GetCursorPosition().Top);
    }
    
    public void UpdateBoard(List<Player> players)
    {
        Console.SetCursorPosition(GameFunctions.BoardCursorPosition.Left, GameFunctions.BoardCursorPosition.Top);
        var newPLayerPos = players.Select(i =>
        {
            i.Coordinate = PlayableBlocks[Random.Shared.Next(PlayableBlocks.Count)];
            return i;
        }).ToList();
        var currentConsolePos = Console.GetCursorPosition();
        for (var y = 0; y < board.Count; y++){
            for (var x = 0; x < board[y].Count; x++)
            {
                var currentBlock = board[y][x];
                var playersAtCord = newPLayerPos
                    .Where(i => i.Coordinate.X == x && i.Coordinate.Y == y).ToList();
                var displayNames = string.Join(", ", playersAtCord.Select(i => i.DisplayName));
                newPLayerPos.RemoveAll(i => i.Coordinate.X == x && i.Coordinate.Y == y);
        
                if (playersAtCord.Any())
                {
                    if (playersAtCord.Count == 1)
                    {
                        Console.ForegroundColor = playersAtCord[0].Color;
                        Console.Write($" {displayNames} ".PadLeft(displayNames.Length));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($" {displayNames} ".PadLeft(displayNames.Length));
                    }
                }
                else
                {
                    Console.Write(currentBlock.PadLeft(displayNames.Length));
                
                }
            }
            Console.WriteLine();
        }
        GameFunctions.BoardCursorPosition = (
            GameFunctions.BoardCursorPosition.Left,
            GameFunctions.BoardCursorPosition.Top,
            Console.GetCursorPosition().Top);
    }
    
    public record Coordinate(int X, int Y);
}