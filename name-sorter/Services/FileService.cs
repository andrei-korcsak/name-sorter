using System.Collections.Generic;
using System.IO;

namespace NameSorter.Services
{
    public class FileService : IFileService
    {
        public IEnumerable<string> ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public void WriteAllLines(string path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}
