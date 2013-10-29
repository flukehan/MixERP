using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixERP.Net.BusinessLayer.DBFactory
{
    public static class ServerConnectivity
    {
        public static bool IsDBServerAvailable()
        {
            return MixERP.Net.DatabaseLayer.DBFactory.ServerConnectivity.IsDBServerAvailable();
        }
    }
}
