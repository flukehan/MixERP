/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    [ToolboxData("<{0}:ScrudForm runat=server></{0}:ScrudForm>")]
    public partial class ScrudForm : CompositeControl
    {
        Panel scrudContainer;
        private string imageColumn = string.Empty;

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.TableSchema))
            {
                throw new ApplicationException("The property 'TableSchema' cannot be left blank.");
            }

            if (string.IsNullOrWhiteSpace(this.Table))
            {
                throw new ApplicationException("The property 'Table' cannot be left blank.");
            }
            
            if (string.IsNullOrWhiteSpace(this.ViewSchema))
            {
                throw new ApplicationException("The property 'ViewSchema' cannot be left blank.");
            }

            if (string.IsNullOrWhiteSpace(this.View))
            {
                throw new ApplicationException("The property 'View' cannot be left blank.");
            }

            if (string.IsNullOrWhiteSpace(this.KeyColumn))
            {
                throw new ApplicationException("The property 'KeyColumn' cannot be left blank.");
            }


        }
        
        protected override void CreateChildControls()
        {
            this.Validate();

            scrudContainer = new Panel();

            this.LoadScrudContainer(scrudContainer);

            this.LoadTitle();
            this.LoadDescription();
            
            this.LoadGrid();
            
            this.InitializeScrudControl();
            
            this.Controls.Add(scrudContainer);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }
        
        protected override void Render(HtmlTextWriter w)
        {
            scrudContainer.RenderControl(w);
        }
    }
}
