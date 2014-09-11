using MixERP.Net.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Common.Models
{
    public class WidgetModel
    {
        public MixERPWidgetBase Widget { get; set; }

        public int RowNumber { get; set; }

        public int ColumnNumber { get; set; }

        public int ColSpan { get; set; }

        public string WidgetSource { get; set; }
    }
}