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

using MixERP.Net.Common.Helpers;
using System;
using System.Reflection;

namespace MixERP.Net.WebControls.ScrudFactory.Helpers
{
    internal static class ScrudLocalizationHelper
    {
        internal static string GetResourceString(Assembly assembly, string resourceClassName, string key)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            string fullyQualifiedResourceClassName = assembly.GetName().Name + ".Resources." + resourceClassName;
            return LocalizationHelper.GetResourceString(assembly, fullyQualifiedResourceClassName, key);
        }
    }
}