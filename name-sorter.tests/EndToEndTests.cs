using System;
using System.IO;
using Xunit;
using NameSorter;

namespace NameSorter.Tests
{
    public class EndToEndTests : IDisposable
    {
        private readonly string _workingDir;

        public EndToEndTests()
        {
            _workingDir = Path.Combine(Path.GetTempPath(), "name-sorter-tests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_workingDir);
            Directory.SetCurrentDirectory(_workingDir);
        }

        // Test: running the application end-to-end should create "sorted-names-list.txt" and
        // subsequent runs should overwrite the file with updated sorted output.
        [Fact]
        public void RunningApp_CreatesAndOverwrites_SortedNamesFile()
        {
            // Arrange
            Console.WriteLine("Test: RunningApp_CreatesAndOverwrites_SortedNamesFile - verifies app creates sorted-names-list.txt and overwrites it on subsequent runs");
            string inputPath = Path.Combine(_workingDir, "unsorted.txt");
            string outputPath = Path.Combine(_workingDir, "sorted-names-list.txt");

            File.WriteAllLines(inputPath, new[] { "John Smith", "Alice Brown" });

            // Act - first run
            int rc1 = NameSorterApp.Main(new[] { inputPath });

            // Assert first run produced file
            Assert.Equal(0, rc1);
            Assert.True(File.Exists(outputPath));
            string[] firstContents = File.ReadAllLines(outputPath);
            Assert.Contains("Alice Brown", firstContents);

            // Modify input and run again
            File.WriteAllLines(inputPath, new[] { "Zed Alpha", "Yvonne Zebra" });

            int rc2 = NameSorterApp.Main(new[] { inputPath });
            Assert.Equal(0, rc2);

            // Assert file overwritten (contents changed)
            string[] secondContents = File.ReadAllLines(outputPath);
            Assert.Contains("Yvonne Zebra", secondContents);
            Assert.DoesNotContain("Alice Brown", secondContents);
        }

        public void Dispose()
        {
            try
            {
                Directory.SetCurrentDirectory(Path.GetTempPath());
                if (Directory.Exists(_workingDir)) Directory.Delete(_workingDir, true);
            }
            catch { }
        }
    }
}
