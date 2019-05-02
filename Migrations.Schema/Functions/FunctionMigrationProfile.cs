using RootNameSpace.Migrations.Core;
using FluentMigrator;

namespace RootNameSpace.Migrations.Schema.Functions
{
    [Profile(Constants.MigrationProfiles.FunctionsProfile)]
    public class FunctionMigrationProfile : SqlResourceMigration
    {
        public override void Up()
        {
            ExecuteResourceSql(typeof(FunctionMigrationProfile));
        }

        public override void Down()
        {
        }
    }
}