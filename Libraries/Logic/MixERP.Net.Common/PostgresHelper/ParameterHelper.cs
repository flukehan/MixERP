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

using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MixERP.Net.Common.PostgresHelper
{
    public static class ParameterHelper
    {
        public static IEnumerable<NpgsqlParameter> AddBigintArrayParameter(Collection<long> items, string parameterPrefix)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (items != null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    collection.Add(new NpgsqlParameter(parameterPrefix + i, items[i]));
                }
            }

            return collection;
        }

        public static string CreateBigintArrayParameter(Collection<long> collection, string pgType, string parameterPrefix)
        {
            if (collection == null)
            {
                return "NULL::" + pgType;
            }

            Collection<string> detailCollection = new Collection<string>();
            for (int i = 0; i < collection.Count; i++)
            {
                detailCollection.Add(string.Format("{0}{1}::{2}", parameterPrefix, i.ToString(CultureInfo.InvariantCulture), pgType));
            }

            return string.Join(",", detailCollection);
        }
    }
}