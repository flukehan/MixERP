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

using System.Text.RegularExpressions;

namespace MixERP.Net.DbFactory
{
    public static class Sanitizer
    {
        /// <summary>
        ///     Please do not use this function to fix the quotes against SQL injection attack.
        ///     This is not a replacement of parameterized statements.
        ///     Use this function only when you need to sanitize "column names" and/or "table names"
        ///     which cannot be done using standard practices.
        /// </summary>
        /// <param name="identifier">Column name or table name which needs to be sanitized</param>
        /// <returns>
        ///     Only alphabets and underscore are allowed characters in identifier name.
        ///     Anything else than that will be removed.
        /// </returns>
        public static string SanitizeIdentifierName(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                return null;
            }

            if (identifier.Contains("--"))
            {
                return string.Empty;
            }
            if (identifier.Contains("/*"))
            {
                return string.Empty;
            }

            //Only alphabets [a-zA-Z], numbers, and an underscore is allowed.
            return Regex.Replace(identifier, @"[^a-zA-Z0-9_]", "");
        }
    }
}