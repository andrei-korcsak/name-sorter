using System;
using NameSorter.Services;
using Xunit;

namespace NameSorter.Tests
{
    public class NameParserTests
    {
        // Test: parser should reject names with more than 3 given names (4+ given tokens + last name -> invalid)
        [Fact]
        public void Parse_MoreThanThreeGivenNames_ThrowsArgumentException()
        {
            // Arrange
            Console.WriteLine("Test: Parse_MoreThanThreeGivenNames_ThrowsArgumentException - parser should reject names with more than 3 given names");
            var parser = new NameParser();
            string input = "One Two Three Four Five"; // 5 tokens -> 4 given names + last name -> invalid

            // Act & Assert
            Assert.Throws<ArgumentException>(() => parser.Parse(input));
        }
    }
}
