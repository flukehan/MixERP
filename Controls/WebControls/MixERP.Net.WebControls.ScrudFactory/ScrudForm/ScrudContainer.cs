/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {        
        private void LoadScrudContainer(Panel p)
        {
            this.AddJavaScript();
            
            this.AddUpdateProgress(p);

            this.AddTitle(p);
            
            AddRuler(p);
            
            this.AddDescription(p);

            this.CreateCommandPanels();
            this.CreateGridPanel();
            this.CreateFormPanel();
            this.AddUpdatePanel(p);
        }

        private void AddJavaScript()
        {
            string script = MixERP.Net.WebControls.ScrudFactory.Helpers.ScrudJavaScriptHelper.GetScript();            
            MixERP.Net.Common.PageUtility.ExecuteJavaScript("scrudScript", script, this.Page);     
        }
    }
}
