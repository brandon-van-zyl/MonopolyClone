namespace MonopolyClone;

public static class Utility
{
    public static void PrintTable(IEnumerable<string> playerNames, IEnumerable<string> amounts)
    {
        // Step 2: Determine Column Widths
        int nameColumnWidth = "Name".Length;
        int amountColumnWidth = "Amount".Length;

        var enumerable = playerNames.ToList();
        foreach (var name in enumerable)
        {
            if (name.Length > nameColumnWidth)
                nameColumnWidth = name.Length;
        }

        var enumerable1 = amounts.ToList();
        foreach (var amount in enumerable1)
        {
            if (amount.Length > amountColumnWidth)
                amountColumnWidth = amount.Length;
        }
        
        // Step 3: Print the Header with Borders
        string header = $"| { "Name".PadRight(nameColumnWidth) } | { "Amount".PadRight(amountColumnWidth) } |";
        string separator = new string('-', header.Length);
        
        Console.WriteLine(separator);
        Console.WriteLine(header);
        Console.WriteLine(separator);

        // Step 4: Print the Rows with Borders
        for (int i = 0; i < enumerable.Count; i++)
        {
            string row = $"| { enumerable[i].PadRight(nameColumnWidth) } | { enumerable1[i].PadRight(amountColumnWidth) } |";
            Console.WriteLine(row);
        }
        
        // Print the bottom border
        Console.WriteLine(separator);
    }
    
    public static string? GetStringFromUser(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
    
    public static void ClearConsoleLines(int startLine, int numberOfLines, int? lineWidth)
    {
        for (int i = 0; i < numberOfLines; i++)
        {
            // Move cursor to the start of the line to clear
            Console.SetCursorPosition(0, startLine + i);
            // Write a line of spaces to clear the line
            Console.Write(new string(' ', lineWidth ?? Console.WindowWidth));
        }

        // Optionally move the cursor back to the original position
        Console.SetCursorPosition(0, startLine);
    }

}