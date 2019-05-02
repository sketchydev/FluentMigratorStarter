using RootNameSpace.Migrations.Core;
using FluentMigrator;

namespace RootNameSpace.Migrations.Schema.Views
{
    [Profile(Constants.MigrationProfiles.ViewsProfile)]
    public class ViewMigrationProfile : SqlResourceMigration
    {
        public override void Up()
        {
            ExecuteResourceSql(typeof(ViewMigrationProfile));
        }

        public override void Down()
        {
        }
    }
}