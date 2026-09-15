using System.Collections.Generic;
using NameSorter.Models;

namespace NameSorter.Services
{
    public interface INameSorter
    {
        IEnumerable<PersonName> Sort(IEnumerable<PersonName> names);
    }
}
