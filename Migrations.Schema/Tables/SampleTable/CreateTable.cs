using FluentMigrator;
using RootNameSpace.Migrations.Core;

namespace RootNameSpace.Migrations.Schema.Tables.SampleTable
{
    [Migration(100)]
    public class CreateTable:Migration
    {
        public override void Up()
        {
            Create.Table(Constants.TableNames.TableNameConstant)
                .InSchema(Constants.DefaultSchemaName)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("ColumnA").AsCustom("varchar(1)");
        }

        public override void Down()
        {
            Delete.Table(Constants.TableNames.TableNameConstant);
        }
    }
}