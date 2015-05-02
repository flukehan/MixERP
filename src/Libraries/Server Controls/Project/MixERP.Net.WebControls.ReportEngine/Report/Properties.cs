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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report
    {
        private readonly Collection<Collection<KeyValuePair<string, object>>> parameterCollection = new Collection<Collection<KeyValuePair<string, object>>>();

        public bool AutoInitialize { get; set; }

        public string ImageButtonCssClass { get; set; }

        public string InvalidLocationErrorMessage { get; set; }

        public bool NoHeader { get; set; }

        /// <summary>
        /// Collection of each datasources' parameter collection.
        /// The datasource parameter collection is a collection of
        /// parameters stored in KeyValuePair.
        /// </summary>
        public Collection<Collection<KeyValuePair<string, object>>> ParameterCollection
        {
            get
            {
                return this.parameterCollection;
            }
        }

        public string Path { get; set; }

        public string ReportNotFoundErrorMessage { get; set; }

        public string RunningTotalText { get; set; }
        public void AddParameterToCollection(Collection<KeyValuePair<string, object>> parameter)
        {
            this.parameterCollection.Add(parameter);
        }
    }
}