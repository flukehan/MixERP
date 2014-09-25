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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web;
using System.Web.UI;

[assembly: WebResource("MixERP.Net.WebControls.ScrudFactory.ScrudItemSelector.js", "application/x-javascript", PerformSubstitution = true)]

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
        private void AddJavaScript()
        {
            JavaScriptHelper.AddJSReference(this.Page, "MixERP.Net.WebControls.ScrudFactory.ScrudItemSelector.js", "scrud_item_selector", this.GetType());
        }
    }
}