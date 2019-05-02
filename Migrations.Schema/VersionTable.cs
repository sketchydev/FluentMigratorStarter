using RootNameSpace.Migrations.Core;
using FluentMigrator.VersionTableInfo;

namespace RootNameSpace.Migrations.Schema
{
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        public string SchemaName => Constants.DefaultSchemaName;
        public string TableName => Constants.VersionInfo.TableName;
        public string ColumnName => Constants.VersionInfo.ColumnName;
        public string DescriptionColumnName => Constants.VersionInfo.DescriptionColumnName;
        public string UniqueIndexName => Constants.VersionInfo.UniqueIndexName;
    }
}