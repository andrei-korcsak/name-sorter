using System;
using System.Linq;
using NameSorter.Models;
using NameSorter.Services;

namespace NameSorter
{
    // Entry point for the console application; named to reflect the app's purpose.
    class NameSorterApp
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: name-sorter <path-to-unsorted-names-file>");
                return 1;
            }

            string inputPath = args[0];
            string outputPath = "sorted-names-list.txt";

            IFileService fileService = new FileService();
            INameParser parser = new NameParser();
            INameSorter sorter = new DefaultNameSorter();

            try
            {
                var lines = fileService.ReadAllLines(inputPath)
                                       .Where(l => !string.IsNullOrWhiteSpace(l))
                                       .Select(l => l.Trim());

                var names = lines.Select(parser.Parse);
                var sorted = sorter.Sort(names).Select(n => n.ToString()).ToList();

                foreach (var s in sorted) Console.WriteLine(s);

                fileService.WriteAllLines(outputPath, sorted);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return 2;
            }
        }
    }
}
