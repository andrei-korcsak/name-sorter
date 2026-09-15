using System;
using System.Linq;
using NameSorter.Models;

namespace NameSorter.Services
{
    public class NameParser : INameParser
    {
        public PersonName Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Input is empty", nameof(input));

            var tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 2 || tokens.Length > 4)
                throw new ArgumentException("A valid name must have between 2 and 4 tokens (1-3 given names and 1 last name).", nameof(input));

            var last = tokens.Last();
            var given = tokens.Take(tokens.Length - 1);
            return new PersonName(given, last);
        }
    }
}
