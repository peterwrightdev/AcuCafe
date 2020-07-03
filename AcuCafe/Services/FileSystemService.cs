using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Services
{
    public class FileSystemService : IFileSystemService
    {
        public void WriteTextToPath(string path, string contents)
        {
            System.IO.File.WriteAllText(path, contents);
        }
    }
}
