
using MixERP.Net.Entities.Contracts;
using PetaPoco;
using System;

namespace MixERP.Net.Entities.Public
{

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

    [FunctionName("each")]
    [ExplicitColumns]
    public class DbEachResult : PetaPocoDB.Record<DbEachResult> , IPoco
    {
        [Column("key")] 
        public string Key { get; set; }

        [Column("value")] 
        public string Value { get; set; }

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

