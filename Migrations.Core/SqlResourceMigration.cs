using System;
using System.IO;
using System.Reflection;
using FluentMigrator;

namespace RootNameSpace.Migrations.Core
{
    public abstract class SqlResourceMigration : Migration
    {
        protected void ExecuteResourceSql(Type migrationType)
        {
            var migrationAssembly = migrationType
                .Assembly;

            foreach (var resourceName in migrationAssembly
                .GetManifestResourceNames())
            {
                if (!resourceName.StartsWith(migrationType.Namespace))
                {
                    continue;
                }

                var sql = ReadSqlResource(resourceName, migrationAssembly);
                Execute.Sql(sql);
            }
        }

        private static string ReadSqlResource(string resourceName, Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}