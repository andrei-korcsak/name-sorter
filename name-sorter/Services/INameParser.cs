using NameSorter.Models;

namespace NameSorter.Services
{
    public interface INameParser
    {
        PersonName Parse(string input);
    }
}
