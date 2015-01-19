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

using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    public static class ScrudTypes
    {
        #region Static Fields

        public static readonly string[] Bools = {"boolean", "bool"};
        public static readonly string[] Dates = {"date", "time"};
        public static readonly string[] Decimals = {"numeric", "money", "money_strict", "money_strict2", "decimal_strict", "decimal_strict2", "currency"};
        public static readonly string[] Doubles = {"double", "double precision", "float"};
        public static readonly string[] Files = {"bytea"};
        public static readonly string[] Integers = {"integer", "integer_strict", "integer_strict2"};
        public static readonly string[] Longs = {"bigint"};
        public static readonly string[] Shorts = {"smallint"};
        public static readonly string[] Singles = {"real"};
        public static readonly string[] Strings = {"national character varying", "character varying", "national character", "character", "char", "varchar", "nvarchar", "text"};
        public static readonly string[] Timestamps = {"timestamp with time zone", "timestamp without time zone"};

        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")] //Resharper is wrong
        public static string[] TextBoxTypes = Decimals.Union(Doubles).Union(Integers).Union(Longs).Union(Shorts).Union(Singles).Union(Strings).Union(Dates).ToArray();

        #endregion
    }
}