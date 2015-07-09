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

using MixERP.Net.Framework;
using System.Collections.ObjectModel;

namespace MixERP.Net.ApplicationState
{
    public static class Dates
    {
        private const string appDatesKey = "ApplicationDates";

        public static Collection<FrequencyDates> GetFrequencyDates(string catalog)
        {
            object dates = CacheFactory.GetFromDefaultCacheByKey(catalog + appDatesKey);

            if (dates == null)
            {
                return new Collection<FrequencyDates>();
            }

            return dates as Collection<FrequencyDates>;
        }

        public static void SetApplicationDates(string catalog, Collection<FrequencyDates> applicationDates)
        {
            CacheFactory.AddToDefaultCache(catalog + appDatesKey, applicationDates);
        }
    }
}