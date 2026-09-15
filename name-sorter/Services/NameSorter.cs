using System;
using System.Collections.Generic;
using System.Linq;
using NameSorter.Models;

namespace NameSorter.Services
{
    public class DefaultNameSorter : INameSorter
    {
        public IEnumerable<PersonName> Sort(IEnumerable<PersonName> names)
        {
            if (names is null) throw new ArgumentNullException(nameof(names));

            return names.OrderBy(n => n.LastName, StringComparer.OrdinalIgnoreCase)
                        .ThenBy(n => n.GivenNames.ToArray(), new GivenNamesComparer())
                        .ToList();
        }

        // Comparer that compares given-name arrays right-to-left (closest to last name first)
        private class GivenNamesComparer : IComparer<string[]>
        {
            public int Compare(string[]? x, string[]? y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (x is null) return -1;
                if (y is null) return 1;

                int xGiven = x.Length;
                int yGiven = y.Length;
                var comparer = StringComparer.OrdinalIgnoreCase;
                int min = Math.Min(xGiven, yGiven);

                for (int i = 1; i <= min; i++)
                {
                    // compare from the rightmost given name backwards
                    string xi = x[xGiven - i];
                    string yi = y[yGiven - i];
                    int c = comparer.Compare(xi, yi);
                    if (c != 0) return c;
                }

                // shorter given-names list sorts earlier
                return xGiven.CompareTo(yGiven);
            }
        }
    }
}
