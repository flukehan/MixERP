/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class ReportHeader //: CompositeControl
    {
        private string path;
        public string Path 
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
                this.EnsureChildControls();           
            }
        }
    }
}
