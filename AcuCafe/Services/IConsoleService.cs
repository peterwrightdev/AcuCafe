﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuCafe.Services
{
    public interface IConsoleService
    {
        string ReadLine();

        void WriteLine(string value);
    }
}
