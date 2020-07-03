using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Services
{
    public class ConsoleService : IConsoleService
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
