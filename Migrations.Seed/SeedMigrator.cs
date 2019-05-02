using RootNameSpace.Migrations.Core;

namespace RootNameSpace.Migrations.Seed
{
    public class SeedMigrator
    {
        private readonly string _connectionString;

        public SeedMigrator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RunSeeds(string[] seedProfiles)
        {
            foreach (var seedProfile in seedProfiles)
            {
                IMigrator migrator = new Migrator(_connectionString, typeof(SeedMigrator).Assembly, seedProfile);
                migrator.Migrate(runner => runner.MigrateUp());
            }
        }
    }
}