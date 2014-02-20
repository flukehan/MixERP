/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class Maintenance
    {
        public static void Vacuum()
        {
            DatabaseLayer.Helpers.Maintenance.Vacuum();
        }

        public static void VacuumFull()
        {
            DatabaseLayer.Helpers.Maintenance.VacuumFull();
        }

        public static void Analyze()
        {
            DatabaseLayer.Helpers.Maintenance.Analyze();
        }
    }
}
