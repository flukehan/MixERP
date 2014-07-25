/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
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
            var script = ScrudJavaScriptHelper.GetScript(this.KeyColumn, this.CustomFormUrl);            
            PageUtility.ExecuteJavaScript("scrudScript", script, this.Page);     
        }

        public bool ListContainNames(List<string> listNames)
        {
            if (listNames == null) return false;
            if (listNames.Any())
            {
                if (listNames.ElementAt(0).Contains("smith"))
                {
                    throw new Exception("should not contain smith");
                }
                if (listNames.ElementAt(0).Contains("david"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
