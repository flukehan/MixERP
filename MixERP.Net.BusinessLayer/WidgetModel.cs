using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.BusinessLayer
{
    public class WidgetModel
    {
        public MixERPWidget Widget { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string WidgetSource { get; set; }
    }
}
