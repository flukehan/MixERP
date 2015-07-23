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

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Framework.Contracts.Checklist;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Checklists : WebService
    {
        [WebMethod]
        public List<Checklist> GetFirstSteps()
        {
            List<Checklist> items = new List<Checklist>();

            foreach (FirstStep item in FirstStep.GetAll().OrderBy(o => o.Order))
            {
                items.Add(new Checklist
                {
                    Name = item.Name,
                    Icon = item.Icon,
                    Category = item.Category,
                    CategoryAlias = item.CategoryAlias,
                    Order = item.Order,
                    Description = item.Description,
                    Status = item.Status,
                    Message = item.Message,
                    NavigateUrl = item.NavigateUrl
                });
            }


            return items;
        }
    }

    public class Checklist
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string CategoryAlias { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string NavigateUrl { get; set; }
    }
}