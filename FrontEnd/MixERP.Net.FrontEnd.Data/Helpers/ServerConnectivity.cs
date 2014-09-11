using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.FrontEnd.Data.Helpers
{
    public static class ServerConnectivity
    {
        public static bool IsDbServerAvailable()
        {
            return DbOperations.IsServerAvailable();
        }
    }
}