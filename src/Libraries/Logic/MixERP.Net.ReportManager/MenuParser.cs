using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.ReportManager
{
    internal class MenuParser
    {
        internal MenuParser(string content)
        {
            this.Content = content;
        }

        internal string Content { get; set; }

        internal ReportMenu Parse()
        {
            throw new NotImplementedException("Todo");
        }
    }
}
