using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Office;

namespace MixERP.Net.Web.UI.Data.Office
{
    public static class Offices
    {
        public static IEnumerable<DbGetOfficesResult> GetOffices()
        {
            return Factory.Get<DbGetOfficesResult>("SELECT * FROM office.get_offices();");
        }
    }
}
