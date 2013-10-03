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

namespace MixERP.Net.Common.Models.Core
{
    public enum Frequency
    {
        Today,
        MonthStartDate,
        MonthEndDate,
        QuarterStartDate,
        QuarterEndDate,
        HalfStartDate,
        HalfEndDate,
        FiscalYearStartDate,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "YearEnd")]
        FiscalYearEndDate
    }
}
