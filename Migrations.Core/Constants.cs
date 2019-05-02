namespace RootNameSpace.Migrations.Core
{
    public static class Constants
    {
        public const string DefaultSchemaName = "dbo";

        public static class TableNames
        {
            public const string TableNameConstant = "TableNameConstant";
        }

        public static class MigrationProfiles
        {
            public const string TriggerProfile = "Triggers";
            public const string FunctionsProfile = "Functions";
            public const string ViewsProfile = "Views";
        }

        public static class VersionInfo
        {
            public const string TableName = "VersionInfo";
            public const string ColumnName = "Version";
            public const string DescriptionColumnName = "ScriptDescription";
            public const string UniqueIndexName = "IDX_Version";

        }
    }
}