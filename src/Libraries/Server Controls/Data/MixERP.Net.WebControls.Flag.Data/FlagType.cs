using System.Collections.Generic;
using PetaPoco;

namespace MixERP.Net.WebControls.Flag.Data
{
    public static class FlagType
    {
        public static IEnumerable<Entities.Core.FlagType> GetFlagTypes(string catalog)
        {
            const string sql = "SELECT * FROM core.flag_types ORDER by flag_type_id;";
            return Factory.Get<Entities.Core.FlagType>(catalog, sql);
        }
    }
}