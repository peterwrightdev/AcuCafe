using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Services
{
    public interface IFileSystemService
    {
        void WriteTextToPath(string path, string contents);
    }
}
