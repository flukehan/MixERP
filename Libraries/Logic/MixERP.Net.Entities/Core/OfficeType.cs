using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixERP.Net.Entities.Office;
using PetaPoco;

namespace MixERP.Net.Entities.Core
{
    [TableName("get_offices")]//office.get_offices()
    [ExplicitColumns]
    public class OfficeType : Office.PetaPocoDB.Record<OfficeType>
    {
        [Column("office_id")]
        public int OfficeId { get; set; }

        [Column("office_code")]
        public string OfficeCode { get; set; }

        [Column("office_name")]
        public string OfficeName { get; set; }

        [Column("address")]
        public string Address { get; set; }

    }
}
