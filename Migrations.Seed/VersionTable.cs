using RootNameSpace.Migrations.Core;
using FluentMigrator.VersionTableInfo;

namespace RootNameSpace.Migrations.Seed
{
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        public string SchemaName { get { return Constants.DefaultSchemaName; } }
        public string TableName { get { return Constants.VersionInfo.TableName; } }
        public string ColumnName { get { return Constants.VersionInfo.ColumnName; } }
        public string DescriptionColumnName { get { return Constants.VersionInfo.DescriptionColumnName; } }
        public string UniqueIndexName { get { return Constants.VersionInfo.UniqueIndexName; } }
    }
}