using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: name-sorter <path-to-unsorted-names-file>");
            return;
        }

        string inputPath = args[0];
        string outputPath = "sorted-names-list.txt";

        // Read all names
        var names = File.ReadAllLines(inputPath)
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line => line.Trim())
                        .ToList();

        // Sort by last name (case-insensitive), then by given names (right-to-left), case-insensitive
        var sorted = names
            .Select(n => new { Original = n, Tokens = n.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) })
            .OrderBy(x => x.Tokens.Last(), StringComparer.OrdinalIgnoreCase)
            .ThenBy(x => x.Tokens, new GivenNamesComparer())
            .Select(x => x.Original)
            .ToList();

        // Print to console
        foreach (var name in sorted)
        {
            Console.WriteLine(name);
        }

        // Write to output file
        File.WriteAllLines(outputPath, sorted);
    }
}

// Comparer that compares given-name token arrays starting from the name nearest the last name
// (i.e., right-to-left across the given names), case-insensitive.
class GivenNamesComparer : IComparer<string[]>
{
    public int Compare(string[]? x, string[]? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        // Treat the last token as the last name; compare the given names from right to left
        int xGiven = Math.Max(0, x.Length - 1);
        int yGiven = Math.Max(0, y.Length - 1);

        var comparer = StringComparer.OrdinalIgnoreCase;
        int min = Math.Min(xGiven, yGiven);

        for (int i = 0; i < min; i++)
        {
            // index of given name starting from one before last name and moving left
            string xi = x[x.Length - 2 - i];
            string yi = y[y.Length - 2 - i];
            int c = comparer.Compare(xi, yi);
            if (c != 0) return c;
        }

        // All compared given names equal -> shorter given-names list sorts earlier
        return xGiven.CompareTo(yGiven);
    }
}
