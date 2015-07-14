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
using System.IO.Compression;
using System.Security;
using System.Security.Permissions;
using System.Web.Hosting;

namespace MixERP.Net.Common.Helpers
{
    public static class FileSystemHelper
    {
        public static bool IsDirectoryWritable(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                return false;
            }

            FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Write, HostingEnvironment.MapPath(directory));
            PermissionSet permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(permission);

            return permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet);
        }

        public static void Unzip(string archive, string destnation)
        {
            ZipFile.ExtractToDirectory(archive, destnation);
        }
    }
}