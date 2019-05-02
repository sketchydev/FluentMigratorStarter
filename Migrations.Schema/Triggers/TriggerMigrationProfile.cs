using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RootNameSpace.Migrations.Core;
using FluentMigrator;

namespace RootNameSpace.Migrations.Schema.Triggers
{
    [Profile(Constants.MigrationProfiles.TriggerProfile)]
    public class TriggerMigrationProfile : Migration
    {
        public override void Up()
        {
            foreach (var triggerBuilder in GetTriggerBuilders())
            {
                Execute.Sql(triggerBuilder.DropTriggerSql());
                Execute.Sql(triggerBuilder.CreateTriggerSql());
            }
        }

        public override void Down()
        {
        }

        public IEnumerable<ITriggerBuilder> GetTriggerBuilders()
        {
            return Assembly
                .GetAssembly(GetType())
                .GetTypes()
                .Where(type => type.GetInterfaces().Any(iface => iface == typeof (ITriggerBuilder)))
                .Select(type => (ITriggerBuilder)Activator.CreateInstance(type));
        }

    }
}