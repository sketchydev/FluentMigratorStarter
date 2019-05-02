using FluentMigrator;
using RootNameSpace.Migrations.Core;

namespace RootNameSpace.Migrations.Schema.Tables.SampleTable
{
    [Migration(150)]
    public class AddColumn:Migration
    {
        public override void Up()
        {
            Alter.Table(Constants.TableNames.TableNameConstant)
                .AddColumn("ColumnB").AsString(255);
        }

        public override void Down()
        {
            Delete.Column("Column B").FromTable(Constants.TableNames.TableNameConstant);
        }
    }
}