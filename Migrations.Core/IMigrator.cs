using System;
using FluentMigrator.Runner;

namespace RootNameSpace.Migrations.Core
{
    public interface IMigrator
    {
        void Migrate(Action<IMigrationRunner> runnerAction);
    }
}