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
using System.Configuration;

namespace MixERP.Net.Common.Models
{
    public sealed class PostgreSQLServer
    {
        private bool isValid;

        public PostgreSQLServer()
        {
            this.BinDirectory = ConfigurationManager.AppSettings["PostgreSQLBinDirectory"];
            this.DatabaseBackupDirectory = ConfigurationManager.AppSettings["DatabaseBackupDirectory"];
            this.PortNumber = Conversion.TryCastInteger(ConfigurationManager.AppSettings["Port"]);
            this.HostName = ConfigurationManager.AppSettings["Server"];
            this.DatabaseName = ConfigurationManager.AppSettings["Database"];
            this.UserId = ConfigurationManager.AppSettings["UserId"];
            this.Password = ConfigurationManager.AppSettings["Password"];
        }

        public PostgreSQLServer(int portNumber, string hostName, string databaseName, string userId, string password, string binDirectory)
        {
            this.PortNumber = portNumber;
            this.HostName = hostName;
            this.DatabaseName = databaseName;
            this.UserId = userId;
            this.Password = password;
            this.BinDirectory = binDirectory;
        }

        public string BinDirectory { get; set; }

        public string DatabaseBackupDirectory { get; set; }

        public string DatabaseName { get; set; }

        public string HostName { get; set; }

        public bool IsValid
        {
            get { return this.isValid; }
        }

        public string Password { get; set; }

        public int PortNumber { get; set; }
        public string UserId { get; set; }
        public void Validate()
        {
            this.isValid = true;

            if (string.IsNullOrWhiteSpace(this.HostName))
            {
                this.isValid = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(this.DatabaseName))
            {
                this.isValid = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(this.UserId))
            {
                this.isValid = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(this.Password))
            {
                this.isValid = false;
                return;
            }

            if (this.PortNumber <= 0)
            {
                this.isValid = false;
            }

        }
    }
}
