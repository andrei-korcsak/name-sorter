using System;
using System.Collections.Generic;
using System.Linq;

namespace NameSorter.Models
{
    public sealed class PersonName
    {
        public IReadOnlyList<string> GivenNames { get; }
        public string LastName { get; }

        public PersonName(IEnumerable<string> givenNames, string lastName)
        {
            if (givenNames is null) throw new ArgumentNullException(nameof(givenNames));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name must not be empty", nameof(lastName));

            var given = givenNames.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            if (given.Length < 1 || given.Length > 3)
                throw new ArgumentException("A name must have at least 1 and at most 3 given names.", nameof(givenNames));

            GivenNames = Array.AsReadOnly(given);
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Join(" ", GivenNames) + " " + LastName;
        }
    }
}
