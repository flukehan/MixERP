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

using MixERP.Net.Utility.SqlBundler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MixERP.Net.Utility.SqlBundler.Helpers
{
    public static class IOHelper
    {
        public static void WriteBundles(string root, IEnumerable<SQLBundle> bundles)
        {
            foreach (var bundle in bundles)
            {
                string filePath = Path.Combine(root, bundle.FileName);
                Console.WriteLine("Writing bundle {0}", filePath);
                File.WriteAllText(filePath, bundle.Script, Encoding.UTF8);
            }
        }
    }
}