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

using System;

namespace MixERP.Net.Common.Models
{
    public class ApplicationDateModel
    {
        public ApplicationDateModel()
        {
        }

        public ApplicationDateModel(int officeId, DateTime today, DateTime monthStartDate, DateTime monthEndDate, DateTime quarterStartDate, DateTime quarterEndDate, DateTime fiscalHalfStartDate, DateTime fiscalHalfEndDate, DateTime fiscalYearStartDate, DateTime fiscalYearEndDate, bool newDayStarted, DateTime? forcedLogOffTimestamp = null)
        {
            this.OfficeId = officeId;
            this.Today = today;
            this.MonthStartDate = monthStartDate;
            this.MonthEndDate = monthEndDate;
            this.QuarterStartDate = quarterStartDate;
            this.QuarterEndDate = quarterEndDate;
            this.FiscalHalfStartDate = fiscalHalfStartDate;
            this.FiscalHalfEndDate = fiscalHalfEndDate;
            this.FiscalYearStartDate = fiscalYearStartDate;
            this.FiscalYearEndDate = fiscalYearEndDate;
            this.NewDayStarted = newDayStarted;
            this.ForcedLogOffTimestamp = forcedLogOffTimestamp;
        }

        public DateTime FiscalHalfEndDate { get; set; }
        public DateTime FiscalHalfStartDate { get; set; }
        public DateTime FiscalYearEndDate { get; set; }
        public DateTime FiscalYearStartDate { get; set; }
        public DateTime? ForcedLogOffTimestamp { get; set; }
        public DateTime MonthEndDate { get; set; }
        public DateTime MonthStartDate { get; set; }
        public bool NewDayStarted { get; set; }
        public int OfficeId { get; set; }
        public DateTime QuarterEndDate { get; set; }
        public DateTime QuarterStartDate { get; set; }
        public DateTime Today { get; set; }
    }
}