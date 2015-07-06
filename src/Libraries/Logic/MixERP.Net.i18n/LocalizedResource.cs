using PetaPoco;

namespace MixERP.Net.i18n
{
    [ExplicitColumns]
    internal class LocalizedResource : PetaPocoDB.Record<LocalizedResource>
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("resource_class")]
        public string ResourceClass { get; set; }

        [Column("key")]
        public string Key { get; set; }

        [Column("original")]
        public string Original { get; set; }

        [Column("translated")]
        public string Translated { get; set; }
    }
}