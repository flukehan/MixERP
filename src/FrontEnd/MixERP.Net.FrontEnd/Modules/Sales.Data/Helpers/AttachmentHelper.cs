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

using MixERP.Net.Entities.Core;
using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace MixERP.Net.Core.Modules.Sales.Data.Data
{
    public static class AttachmentHelper
    {
        public static IEnumerable<NpgsqlParameter> AddAttachmentParameter(Collection<Attachment> attachments)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (attachments != null && attachments.Count > 0)
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@Comment" + i, attachments[i].Comment));
                    collection.Add(new NpgsqlParameter("@FilePath" + i, attachments[i].FilePath));
                    collection.Add(new NpgsqlParameter("@OriginalFileName" + i, attachments[i].OriginalFileName));
                    collection.Add(new NpgsqlParameter("@Extension" + i, Path.GetExtension(attachments[i].OriginalFileName)));
                }
            }

            return collection;
        }

        public static string CreateAttachmentModelParameter(Collection<Attachment> attachments)
        {
            if (attachments == null || attachments.Count.Equals(0))
            {
                return "NULL::core.attachment_type";
            }

            Collection<string> collection = new Collection<string>();

            for (int i = 0; i < attachments.Count; i++)
            {
                collection.Add(string.Format(CultureInfo.InvariantCulture, "ROW(@Comment{0}, @FilePath{0}, @OriginalFileName{0}, @Extension{0})::core.attachment_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", collection);
        }
    }
}