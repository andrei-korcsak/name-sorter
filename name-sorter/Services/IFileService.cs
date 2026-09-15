using System.Collections.Generic;

namespace NameSorter.Services
{
    public interface IFileService
    {
        IEnumerable<string> ReadAllLines(string path);
        void WriteAllLines(string path, IEnumerable<string> lines);
    }
}
