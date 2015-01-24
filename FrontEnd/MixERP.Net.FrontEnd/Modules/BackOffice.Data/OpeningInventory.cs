/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.Core.Modules.BackOffice.Data
{
   public static class OpeningInventory
    {
       public static bool Exists(int officeId)
       {
           const string sql = "SELECT * FROM transactions.opening_inventory_exists(@OfficeId);";

           using (NpgsqlCommand command = new NpgsqlCommand(sql))
           {
               command.Parameters.AddWithValue("@OfficeId", officeId);

               return Conversion.TryCastBoolean(DbOperation.GetScalarValue(command));
           }
       }


    }
}
