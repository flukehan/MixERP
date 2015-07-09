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
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void AddScrudScript()
        {
            var script = ScrudJavascriptHelper.GetScript(this.Catalog, this.KeyColumn, this.CustomFormUrl, this.formGridView.ID, this.gridPanel.ID, this.userIdHidden.ID, this.officeCodeHidden.ID, this.titleLabel.ID, this.formPanel.ID, this.cancelButton.ID);
            PageUtility.RegisterJavascript("ScrudFormScript", script, this.Page, true);
        }

        private void LoadScrudContainer(Panel p)
        {
            this.AddTitle(p);

            AddRuler(p);

            this.AddDescription(p);

            this.CreateCommandPanels();
            this.CreateGridPanel();
            this.CreateFormPanel();
            this.AddUpdatePanel(p);

            this.AddScrudScript();
        }
    }
}