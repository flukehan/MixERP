
using MixERP.Net.Entities.Contracts;
using PetaPoco;

namespace MixERP.Net.Entities.Localization
{

    [TableName("localization.resources")]
    [PrimaryKey("resource_id")]
    [ExplicitColumns]
    public class Resource : PetaPocoDB.Record<Resource> , IPoco
    {
        [Column("resource_id")] 
        public int ResourceId { get; set; }

        [Column("resource_class")] 
        public string ResourceClass { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.cultures")]
    [PrimaryKey("culture_code", autoIncrement=false)]
    [ExplicitColumns]
    public class Culture : PetaPocoDB.Record<Culture> , IPoco
    {
        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("culture_name")] 
        public string CultureName { get; set; }

    }

    [TableName("localization.localized_resources")]
    [ExplicitColumns]
    public class LocalizedResource : PetaPocoDB.Record<LocalizedResource> , IPoco
    {
        [Column("resource_id")] 
        public int ResourceId { get; set; }

        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.resource_view")]
    [ExplicitColumns]
    public class ResourceView : PetaPocoDB.Record<ResourceView> , IPoco
    {
        [Column("resource_class")] 
        public string ResourceClass { get; set; }

        [Column("culture")] 
        public string Culture { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.localized_resource_view")]
    [ExplicitColumns]
    public class LocalizedResourceView : PetaPocoDB.Record<LocalizedResourceView> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [FunctionName("get_localization_table")]
    [ExplicitColumns]
    public class DbGetLocalizationTableResult : PetaPocoDB.Record<DbGetLocalizationTableResult> , IPoco
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


