using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class Agents
    {
        public static DataTable GetAgentDataTable()
        {
            return FormHelper.GetTable("core", "agents", "agent_id");
        }
    }
}