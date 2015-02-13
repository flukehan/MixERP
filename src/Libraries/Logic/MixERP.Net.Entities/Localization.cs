
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace MixERP.Net.Entities.Localization
{
    public partial class PetaPocoDB : Database
    {
        public PetaPocoDB(): base("")
        {
            CommonConstruct();
        }

        public PetaPocoDB(string connectionStringName): base(connectionStringName)
        {
            CommonConstruct();
        }
        
        partial void CommonConstruct();
        
        public interface IFactory
        {
            PetaPocoDB GetInstance();
        }
        
        public static IFactory Factory { get; set; }
        public static PetaPocoDB GetInstance()
        {
            if (_instance!=null)
                return _instance;
                
            if (Factory!=null)
                return Factory.GetInstance();
            else
                return new PetaPocoDB();
        }

        [ThreadStatic] static PetaPocoDB _instance;
        
        public override void OnBeginTransaction()
        {
            if (_instance==null)
                _instance=this;
        }
        
        public override void OnEndTransaction()
        {
            if (_instance==this)
                _instance=null;
        }
        
        public class Record<T> where T:new()
        {
            public static PetaPocoDB repo { get { return PetaPocoDB.GetInstance(); } }
            public bool IsNew() { return repo.IsNew(this); }
            public object Insert() { return repo.Insert(this); }
            public void Save() { repo.Save(this); }
            public int Update() { return repo.Update(this); }
            public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
            public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
            public static int Update(Sql sql) { return repo.Update<T>(sql); }
            public int Delete() { return repo.Delete(this); }
            public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
            public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
            public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
            public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
            public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
            public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
            public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
            public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
            public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
            public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
            public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
            public static T Single(Sql sql) { return repo.Single<T>(sql); }
            public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
            public static T First(Sql sql) { return repo.First<T>(sql); }
            public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
            public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
            public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
            public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
            public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
            public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
            public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
            public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
            public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
            public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
        }
    }
    


    [TableName("localization.resources")]
    [PrimaryKey("resource_id")]
    [ExplicitColumns]
    public class Resource : PetaPocoDB.Record<Resource> 
    {
        [Column("resource_id")] 
        public int ResourceId { get; set; }

        [Column("path")] 
        public string Path { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.localized_resources")]
    [ExplicitColumns]
    public class LocalizedResource : PetaPocoDB.Record<LocalizedResource> 
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

        [Column("localization_id")] 
        public int LocalizationId { get; set; }

    }

    [TableName("localization.cultures")]
    [PrimaryKey("culture_code", autoIncrement=false)]
    [ExplicitColumns]
    public class Culture : PetaPocoDB.Record<Culture> 
    {
        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("culture_name")] 
        public string CultureName { get; set; }

    }

    [TableName("localization.localized_resources_view")]
    [ExplicitColumns]
    public class LocalizedResourcesView : PetaPocoDB.Record<LocalizedResourcesView> 
    {
        [Column("resource")] 
        public string Resource { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.localization_view")]
    [ExplicitColumns]
    public class LocalizationView : PetaPocoDB.Record<LocalizationView> 
    {
        [Column("path")] 
        public string Path { get; set; }

        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.missing_localization_view")]
    [ExplicitColumns]
    public class MissingLocalizationView : PetaPocoDB.Record<MissingLocalizationView> 
    {
        [Column("path")] 
        public string Path { get; set; }

        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [TableName("localization.missing_key_view")]
    [ExplicitColumns]
    public class MissingKeyView : PetaPocoDB.Record<MissingKeyView> 
    {
        [Column("culture_code")] 
        public string CultureCode { get; set; }

        [Column("key")] 
        public string Key { get; set; }

    }

    [FunctionName("get_localization_table")]
    [ExplicitColumns]
    public class DbGetLocalizationTableResult : PetaPocoDB.Record<DbGetLocalizationTableResult> 
    {
        [Column("row_number")] 
        public string RowNumber { get; set; }

        [Column("key")] 
        public string Key { get; set; }

        [Column("invariant_resource")] 
        public string InvariantResource { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }
}


