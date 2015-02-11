using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Utility.Installer
{
    public interface IInstaller
    {
        void Install();
        bool IsInstalled { get; set; }
        string Name { get; }
    }
}
