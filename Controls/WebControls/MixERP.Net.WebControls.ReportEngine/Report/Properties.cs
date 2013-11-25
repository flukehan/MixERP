/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ReportEngine
{
    public partial class Report : CompositeControl
    {
        public string Path { get; set; }
        public bool AutoInitialize { get; set; }
        public string ReportNotFoundErrorMessage { get; set; }
        public string InvalidLocationErrorMessage { get; set; }
        public string RunningTotalText { get; set; }

        /// <summary>
        /// Collection of each datasources' parameter collection.
        /// The datasource parameter collection is a collection of
        /// parameters stored in KeyValuePair.
        /// </summary>

        private Collection<Collection<KeyValuePair<string, string>>> parameterCollection = new Collection<Collection<KeyValuePair<string, string>>>();
        public Collection<Collection<KeyValuePair<string, string>>> ParameterCollection 
        {
            get
            {
                return this.parameterCollection;
            }
        }

        public void AddParameterToCollection(Collection<KeyValuePair<string, string>> parameter)
        {
            this.parameterCollection.Add(parameter);
        }
    }
}
