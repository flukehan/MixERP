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

using PetaPoco;

namespace MixERP.Net.Common.Helpers
{
    public static class DbConfig
    {
        public static string GetMixERPParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.mixerp WHERE key=@0";
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static string GetAttachmentParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.attachment_factory WHERE key=@0";
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static string GetCurrencylayerParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.currency_layer WHERE key=@0";
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static string GetOpenExchangeRatesParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.open_exchange_rates WHERE key=@0"; 
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static string GetDbParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.db_paramters WHERE key=@0";
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static string GetMessagingParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.messaging WHERE key=@0";
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static string GetScrudParameter(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.scrud_factory WHERE key=@0";
            return Factory.Scalar<string>(catalog, sql, key);
        }

        public static bool GetSwitch(string catalog, string key)
        {
            const string sql = "SELECT value FROM config.switches WHERE key=@0";
            return Factory.Scalar<bool>(catalog, sql, key);
        }
    }
}