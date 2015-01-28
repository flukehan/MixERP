using MixERP.Net.Entities.Office;
using PetaPoco;

namespace MixERP.Net.Entities.Models.Core
{
    [TableName("get_offices")] //office.get_offices()
    [ExplicitColumns]
    public class OfficeType : PetaPocoDB.Record<OfficeType>
    {
        [Column("address")]
        public string Address { get; set; }

        [Column("office_code")]
        public string OfficeCode { get; set; }

        [Column("office_id")]
        public int OfficeId { get; set; }

        [Column("office_name")]
        public string OfficeName { get; set; }
    }
}