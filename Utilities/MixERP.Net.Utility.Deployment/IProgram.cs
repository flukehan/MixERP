using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Utility.Installer
{
    interface IProgram
    {
        bool IsInstalled { get; }
    }
}
