using System;
using System.Linq;
using NameSorter.Services;
using Xunit;

namespace NameSorter.Tests
{
    public class NameSorterTests
    {
        // Test: sorter should order names by last name primarily (Adams, Brown, Smith)
        [Fact]
        public void Sorter_SortsByLastNameThenGivenNames()
        {
            Console.WriteLine("Test: Sorter_SortsByLastNameThenGivenNames - verifies primary sorting by last name");
            var parser = new NameParser();
            var sorter = new DefaultNameSorter();

            var inputs = new[] { "John Smith", "Alice Brown", "Bob Adams" };
            var parsed = inputs.Select(parser.Parse);

            var sorted = sorter.Sort(parsed).Select(p => p.ToString()).ToList();

            var expected = new[] { "Bob Adams", "Alice Brown", "John Smith" };
            Assert.Equal(expected, sorted);
        }

        // Test: when last and nearer-given names are equal, a name with fewer given names
        // (shorter sequence) should come before a longer one (e.g., "John Doe" before "John Michael Doe").
        [Fact]
        public void Sorter_ShorterGivenNamesComesFirstWhenPrefixEqual()
        {
            Console.WriteLine("Test: Sorter_ShorterGivenNamesComesFirstWhenPrefixEqual - verifies shorter given-name sequence sorts before longer when prefix-equal");
            var parser = new NameParser();
            var sorter = new DefaultNameSorter();

            var inputs = new[] { "John Doe", "John Michael Doe" };
            var parsed = inputs.Select(parser.Parse);

            var sorted = sorter.Sort(parsed).Select(p => p.ToString()).ToList();

            var expected = new[] { "John Doe", "John Michael Doe" };
            Assert.Equal(expected, sorted);
        }
    }
}
