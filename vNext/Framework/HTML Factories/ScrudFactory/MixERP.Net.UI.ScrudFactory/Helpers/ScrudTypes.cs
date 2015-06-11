using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MixERP.Net.UI.ScrudFactory.Layout.Form;

namespace MixERP.Net.UI.ScrudFactory.Helpers
{
    public static class ScrudTypes
    {
        public static readonly string[] Bools = {"boolean", "bool"};
        public static readonly string[] Files = {"bytea"};
        public static readonly string[] Timestamps = {"timestamp with time zone", "timestamp without time zone"};

        internal static ILayout GetLayout(string dataType, Config config, FieldConfig fieldConfig)
        {
            string vtype = string.Empty;

            if (Shorts.Contains(dataType) || Integers.Contains(dataType) || Longs.Contains(dataType))
            {
                vtype = "int";

                if (dataType.EndsWith("_strict"))
                {
                    vtype = "int-strict";
                }
                if (dataType.EndsWith("_strict2"))
                {
                    vtype = "int-strict2";
                }

                return new TextBox(config, fieldConfig, vtype);
            }

            if (Decimals.Contains(dataType))
            {
                vtype = "dec";

                if (dataType.EndsWith("_strict"))
                {
                    vtype = "dec-strict";
                }
                if (dataType.EndsWith("_strict2"))
                {
                    vtype = "dec-strict2";
                }

                return new TextBox(config, fieldConfig, vtype);
            }

            if (Bools.Contains(dataType))
            {
                return new Radios(config, fieldConfig);
            }

            if (Dates.Contains(dataType))
            {
                return new TextBox(config, fieldConfig, "date");
            }

            return Files.Contains(dataType)
                ? new TextBox(config, fieldConfig, "file")
                : new TextBox(config, fieldConfig);
        }

        public static readonly string[] Dates = {"date", "time"};

        public static readonly string[] Decimals =
        {
            "numeric", "money", "money_strict", "money_strict2",
            "decimal_strict", "decimal_strict2", "currency"
        };

        public static readonly string[] Doubles = {"double", "double precision", "float"};
        public static readonly string[] Integers = {"integer", "integer_strict", "integer_strict2"};
        public static readonly string[] Longs = {"bigint"};
        public static readonly string[] Shorts = {"smallint"};
        public static readonly string[] Singles = {"real"};

        public static readonly string[] Strings =
        {
            "national character varying", "character varying",
            "national character", "character", "char", "varchar", "nvarchar", "text", "color", "transaction_type"
        };

        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")] //Resharper is wrong
        public static string[] TextBoxTypes =
            Decimals.Union(Doubles)
                .Union(Integers)
                .Union(Longs)
                .Union(Shorts)
                .Union(Singles)
                .Union(Strings)
                .Union(Dates)
                .ToArray();
    }
}