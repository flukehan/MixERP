/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MixERP.Net.WebControls.ReportEngine.Helpers
{
    public class ReportInstaller
    {
        public static void InstallReport(string menuCode, string parentMenuCode, int level, string menuText, string path)
        {
            MixERP.Net.WebControls.ReportEngine.Data.Installer.InstallReport(menuCode, parentMenuCode, level, menuText, path);
        }
    }
}
