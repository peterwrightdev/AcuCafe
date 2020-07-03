using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AcuCafe.Services;

namespace AcuCafe
{
    public static class ServiceLocatorWrapper
    {
        private static IServiceLocator _innerServiceLocator = new ServiceLocator();

        public static IServiceLocator ServiceLocator
        {
            get
            {
                return ServiceLocatorWrapper._innerServiceLocator;
            }
            set
            {
                ServiceLocatorWrapper._innerServiceLocator = value;
            }
        }
    }

    public interface IServiceLocator
    {
        IConsoleService GetConsoleService();

        IFileSystemService GetFileService();
    }

    public class ServiceLocator : IServiceLocator
    {
        public IConsoleService GetConsoleService()
        {
            return new ConsoleService();
        }

        public IFileSystemService GetFileService()
        {
            return new FileSystemService();
        }
    }
}
