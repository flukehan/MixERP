using System.Collections.Generic;
using MixERP.Net.Entities;

namespace MixERP.Net.WebControls.Flag.Data
{
    public static class FlagType
    {
        public static IEnumerable<Entities.Core.FlagType> GetFlagTypes()
        {
            return Factory.Get<Entities.Core.FlagType>("SELECT * FROM core.flag_types ORDER by flag_type_id;");
        }
    }
}