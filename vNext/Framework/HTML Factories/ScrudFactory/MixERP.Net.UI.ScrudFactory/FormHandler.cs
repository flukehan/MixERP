using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MixERP.Net.Common.Base;
using MixERP.Net.UI.ScrudFactory.Data;
using MixERP.Net.UI.ScrudFactory.Models;
using MixERP.Net.UI.ScrudFactory.Resources;

namespace MixERP.Net.UI.ScrudFactory
{
    public static class FormHandler
    {
        public static void Insert(Field[] fields, int userId, Config config)
        {
            if (userId <= 0)
            {
                throw new InvalidOperationException(Errors.InvalidUserId);
            }

            if (config.DenyAdd)
            {
                throw new MixERPException(Titles.AccessIsDenied);
            }

            FormHelper.InsertRecord(userId, config.TableSchema, config.Table, config.KeyColumn, ToKeyValuePairs(fields));
        }

        public static void Update(object id, Field[] fields, int userId, Config config)
        {
            if (config.DenyEdit)
            {
                throw new MixERPException(Titles.AccessIsDenied);
            }

            string[] exclusion = { "" };

            if (!string.IsNullOrWhiteSpace(config.ExcludeEdit))
            {
                exclusion = config.ExcludeEdit.Split(',').Select(x => x.Trim().ToUpperInvariant()).ToArray();
            }


            FormHelper.UpdateRecord(userId, config.TableSchema, config.Table, ToKeyValuePairs(fields), config.KeyColumn, id,
                string.Empty, exclusion);
        }

        private static Collection<KeyValuePair<string, object>> ToKeyValuePairs(Field[] fields)
        {
            Collection<KeyValuePair<string, object>> pairs = new Collection<KeyValuePair<string, object>>();

            foreach (Field field in fields)
            {
                pairs.Add(new KeyValuePair<string, object>(field.Key, field.Value));    
            }

            return pairs;
        }



        public static void Delete(Config config, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            if (config.DenyDelete)
            {
                throw new MixERPException(Titles.AccessIsDenied);
            }

            FormHelper.DeleteRecord(config.TableSchema, config.Table, config.KeyColumn, id);
        }
    }
}
