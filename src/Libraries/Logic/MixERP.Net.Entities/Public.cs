
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixERP.Net.Entities.Public
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
    


    [TableName("public.db_stat")]
    [ExplicitColumns]
    public class DbStat : PetaPocoDB.Record<DbStat> , IPoco
    {
        [Column("relname")] 
        public string Relname { get; set; }

        [Column("last_vacuum")] 
        public DateTime? LastVacuum { get; set; }

        [Column("last_autovacuum")] 
        public DateTime? LastAutovacuum { get; set; }

        [Column("last_analyze")] 
        public DateTime? LastAnalyze { get; set; }

        [Column("last_autoanalyze")] 
        public DateTime? LastAutoanalyze { get; set; }

        [Column("vacuum_count")] 
        public long? VacuumCount { get; set; }

        [Column("autovacuum_count")] 
        public long? AutovacuumCount { get; set; }

        [Column("analyze_count")] 
        public long? AnalyzeCount { get; set; }

        [Column("autoanalyze_count")] 
        public long? AutoanalyzeCount { get; set; }

    }

    [FunctionName("crosstab2")]
    [ExplicitColumns]
    public class DbCrosstab2Result : PetaPocoDB.Record<DbCrosstab2Result> , IPoco
    {
        [Column("row_name")] 
        public string RowName { get; set; }

        [Column("category_1")] 
        public string Category1 { get; set; }

        [Column("category_2")] 
        public string Category2 { get; set; }

    }

    [FunctionName("crosstab3")]
    [ExplicitColumns]
    public class DbCrosstab3Result : PetaPocoDB.Record<DbCrosstab3Result> , IPoco
    {
        [Column("row_name")] 
        public string RowName { get; set; }

        [Column("category_1")] 
        public string Category1 { get; set; }

        [Column("category_2")] 
        public string Category2 { get; set; }

        [Column("category_3")] 
        public string Category3 { get; set; }

    }

    [FunctionName("poco_get_table_function_definition")]
    [ExplicitColumns]
    public class DbPocoGetTableFunctionDefinitionResult : PetaPocoDB.Record<DbPocoGetTableFunctionDefinitionResult> , IPoco
    {
        [Column("column_name")] 
        public string ColumnName { get; set; }

        [Column("is_nullable")] 
        public string IsNullable { get; set; }

        [Column("udt_name")] 
        public string UdtName { get; set; }

        [Column("column_default")] 
        public string ColumnDefault { get; set; }

    }

    [FunctionName("each")]
    [ExplicitColumns]
    public class DbEachResult : PetaPocoDB.Record<DbEachResult> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

    }

    [FunctionName("crosstab4")]
    [ExplicitColumns]
    public class DbCrosstab4Result : PetaPocoDB.Record<DbCrosstab4Result> , IPoco
    {
        [Column("row_name")] 
        public string RowName { get; set; }

        [Column("category_1")] 
        public string Category1 { get; set; }

        [Column("category_2")] 
        public string Category2 { get; set; }

        [Column("category_3")] 
        public string Category3 { get; set; }

        [Column("category_4")] 
        public string Category4 { get; set; }

    }

    [TableName("public.tablefunc_crosstab_2")]
    [ExplicitColumns]
    public class TablefuncCrosstab2 : PetaPocoDB.Record<TablefuncCrosstab2> , IPoco
    {
        [Column("row_name")] 
        public string RowName { get; set; }

        [Column("category_1")] 
        public string Category1 { get; set; }

        [Column("category_2")] 
        public string Category2 { get; set; }

    }

    [TableName("public.tablefunc_crosstab_3")]
    [ExplicitColumns]
    public class TablefuncCrosstab3 : PetaPocoDB.Record<TablefuncCrosstab3> , IPoco
    {
        [Column("row_name")] 
        public string RowName { get; set; }

        [Column("category_1")] 
        public string Category1 { get; set; }

        [Column("category_2")] 
        public string Category2 { get; set; }

        [Column("category_3")] 
        public string Category3 { get; set; }

    }

    [TableName("public.tablefunc_crosstab_4")]
    [ExplicitColumns]
    public class TablefuncCrosstab4 : PetaPocoDB.Record<TablefuncCrosstab4> , IPoco
    {
        [Column("row_name")] 
        public string RowName { get; set; }

        [Column("category_1")] 
        public string Category1 { get; set; }

        [Column("category_2")] 
        public string Category2 { get; set; }

        [Column("category_3")] 
        public string Category3 { get; set; }

        [Column("category_4")] 
        public string Category4 { get; set; }

    }
}

