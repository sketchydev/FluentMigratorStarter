using System.Diagnostics;
using RootNameSpace.Migrations.Schema;
using RootNameSpace.Migrations.Seed;

namespace RootNameSpace.Migrations
{
    public class MigrationManager
    {
        private readonly Options _migrationOptions;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        private ConsoleTraceListener _consoleTraceListener;

        public MigrationManager(Options migrationOptions)
        {
            _migrationOptions = migrationOptions;
            _connectionStringBuilder = new ConnectionStringBuilder(_migrationOptions);
        }

        public void Run()
        {
            if (!_migrationOptions.Silent)
            {
                _consoleTraceListener = new ConsoleTraceListener(true);
                Trace.Listeners.Add(_consoleTraceListener);
            }

            try
            {
                var connectionString = _connectionStringBuilder.GetConnectionString();

                if (_migrationOptions.GetModeTypes().Contains(ModeType.DropAllSchema))
                {
                    new DropAllDatabaseSchemaHandler()
                        .DropAllSchema(_connectionStringBuilder.GetConnectionString());
                    return;
                }

                RunSchemaMigrations(connectionString);
                RunSeedMigrations(connectionString);
            }
            finally
            {
                if (_consoleTraceListener != null)
                {
                    Trace.Flush();
                    _consoleTraceListener.Close();   
                }
            }
        }
        
        private void RunSchemaMigrations(string connectionString)
        {
            var schemaMigrator = new SchemaMigrator(connectionString);

            if (_migrationOptions.GetModeTypes().Contains(ModeType.Destroy))
            {
                schemaMigrator.MigrateDown();
            }

            if (_migrationOptions.GetModeTypes().Contains(ModeType.Schema))
            {
                schemaMigrator.MigrateLatest();
            }
        }

        private void RunSeedMigrations(string connectionString)
        {
            if (!_migrationOptions.GetModeTypes().Contains(ModeType.Seed) ||
                _migrationOptions.GetSeedProfiles().Length == 0)
                return;

            var seedMigrator = new SeedMigrator(connectionString);
            seedMigrator.RunSeeds(_migrationOptions.GetSeedProfiles());
        }

    }
}