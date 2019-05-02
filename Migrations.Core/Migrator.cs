using System;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace RootNameSpace.Migrations.Core
{
    public class Migrator : IMigrator
    {
        private readonly string _connectionString;
        private readonly Assembly _migrationAssembly;
        private readonly string _profileName;

        public Migrator(string connectionString, Assembly migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        public Migrator(string connectionString, Assembly migrationAssembly, string profileName)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
            _profileName = profileName;
        }

        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            try
            {
                var options = new MigrationOptions { PreviewOnly = false, Timeout = 0 };
                var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2012ProcessorFactory();

                var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Trace.WriteLine(s));
                var migrationContext = new RunnerContext(announcer);

                if (_profileName != null)
                    migrationContext.Profile = _profileName;

                var processor = factory.Create(_connectionString, announcer, options);
                var runner = new MigrationRunner(_migrationAssembly, migrationContext, processor);
                runnerAction(runner);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error migrating the Database:");
                System.Diagnostics.Trace.WriteLine(_connectionString);

                System.Diagnostics.Trace.WriteLine(ex);

                throw;
            }
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; private set; }
        }
    }
}