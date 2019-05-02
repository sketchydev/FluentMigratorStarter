using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RootNameSpace.Migrations.Schema;
using FluentMigrator;
using NUnit.Framework;

namespace Migrations.Tests.Unit
{
    [TestFixture]
    public class MigrationVersionTests
    {
        private List<Type> _migrationTypes;
        private long _maxMigrationVersion;

        [SetUp]
        public void GivenASetOfMigrations_WhenLookingAtTheVersion()
        {
            _migrationTypes = typeof (SchemaMigrator).Assembly
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof (Migration)))
                .ToList();

            _maxMigrationVersion = long.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
        }

        [Test]
        public void ThenTheVersionShouldNotBeInTheFuture()
        {
            foreach (var migrationType in _migrationTypes)
            {
                var migrationAttribute = migrationType.GetCustomAttribute<MigrationAttribute>();
                
                if (migrationAttribute == null)
                    continue;
                
                if (migrationAttribute.Version > _maxMigrationVersion)
                    Assert.Fail("Migration {0} ({1}) can't be created with a future timestamp", migrationType.Name, migrationAttribute.Version);
            }
        }
    }
}