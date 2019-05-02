using RootNameSpace.Migrations.Core;

namespace RootNameSpace.Migrations.Schema
{
    public class SchemaMigrator
    {
        private readonly IMigrator _schemaMigrator;
        private readonly IMigrator _triggerMigrator;
        private readonly IMigrator _functionMigrator;
        private readonly IMigrator _viewMigrator;

        public SchemaMigrator(string connectionString)
        {
            _schemaMigrator = new Migrator(connectionString, typeof (SchemaMigrator).Assembly);
            _triggerMigrator = new Migrator(connectionString, typeof(SchemaMigrator).Assembly, Constants.MigrationProfiles.TriggerProfile);
            _functionMigrator = new Migrator(connectionString, typeof(SchemaMigrator).Assembly, Constants.MigrationProfiles.FunctionsProfile);
            _viewMigrator = new Migrator(connectionString, typeof(SchemaMigrator).Assembly, Constants.MigrationProfiles.ViewsProfile);
        }

        public void MigrateLatest()
        {
            _schemaMigrator.Migrate(runner => runner.MigrateUp());
            _triggerMigrator.Migrate(runner => runner.MigrateUp());
            _functionMigrator.Migrate(runner => runner.MigrateUp());
            _viewMigrator.Migrate(runner => runner.MigrateUp());
        }

        public void MigrateDown()
        {
            _schemaMigrator.Migrate(runner => runner.MigrateDown(0));
        }
    }
}