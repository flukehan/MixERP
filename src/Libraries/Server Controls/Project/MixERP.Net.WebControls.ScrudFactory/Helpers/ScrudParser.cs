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

using MixERP.Net.Common;
using System.Linq;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    internal class ScrudParser
    {
        public static object ParseValue(string value, string dataType)
        {
            if (ScrudTypes.Strings.Contains(dataType))
            {
                return Conversion.TryCastString(value);
            }

            if (ScrudTypes.Shorts.Contains(dataType))
            {
                return Conversion.TryCastShort(value);
            }

            if (ScrudTypes.Integers.Contains(dataType))
            {
                return Conversion.TryCastInteger(value);
            }

            if (ScrudTypes.Longs.Contains(dataType))
            {
                return Conversion.TryCastLong(value);
            }

            if (ScrudTypes.Decimals.Contains(dataType))
            {
                return Conversion.TryCastDecimal(value);
            }

            if (ScrudTypes.Doubles.Contains(dataType))
            {
                return Conversion.TryCastDouble(value);
            }

            if (ScrudTypes.Singles.Contains(dataType))
            {
                return Conversion.TryCastSingle(value);
            }

            if (ScrudTypes.Dates.Contains(dataType))
            {
                return Conversion.TryCastDate(value);
            }

            if (ScrudTypes.Bools.Contains(dataType))
            {
                return Conversion.TryCastBoolean(value);
            }


            return null;
        }
    }
}