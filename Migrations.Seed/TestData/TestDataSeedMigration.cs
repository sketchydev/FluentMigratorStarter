using System.Linq;
using RootNameSpace.Migrations.Core;
using RootNameSpace.Migrations.Seed.Convertors;
using FluentMigrator;
using RootNameSpace.Migrations.Seed.TestData.SampleDataTypeA;

namespace RootNameSpace.Migrations.Seed.TestData
{
    [Profile(DataSeedProfileName)]
    public class TestDataSeedMigration : Migration
    {
        private const string DataSeedProfileName = "test-data";
        public override void Up()
        {
            new SampleDataTypeATestData()
                .ToList()
                .ForEach(record => Insert.IntoTable(Constants.TableNames.TableNameConstant)
                                   .Row(record.ToSeedObject()));
            //Other Seed types would go here
        }

        public override void Down()
        {
        }
    }
}