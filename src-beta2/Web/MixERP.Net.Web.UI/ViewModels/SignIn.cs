using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Web.UI.Data.Office;

namespace MixERP.Net.Web.UI.ViewModels
{
    public class SignIn
    {
        public SignIn()
        {
            this.Challenge = Guid.NewGuid().ToString().Replace("-", "");
        }

        public string Challenge { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<Culture> Cultures { get; set; }

        public class Culture
        {
            public string CultureCode { get; set; }
            public string CultureName { get; set; }
            public string NativeName { get; set; }
        }

        public class Branch
        {
            public int OfficeId { get; set; }
            public string OfficeName { get; set; }
        }
    }
}