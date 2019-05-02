using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace RootNameSpace.Migrations
{
    public class DropAllDatabaseSchemaHandler
    {
        private SqlConnection _connection;

        public void DropAllSchema(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            using (_connection)
            {
                _connection.Open();

                Console.WriteLine("Droppping all database schema");

                DropTables();
                DropViews();
                DropFunctions();
            }
        }

        private void DropTables()
        {
            var tableNames = GetTableNames();

            while (tableNames.Count > 0)
            {
                foreach (var tableName in tableNames)
                {
                    try
                    {
                        _connection.Execute("DROP TABLE " + tableName );
                        Console.WriteLine("Sucessfully dropped table: " + tableName);
                    }
                    catch
                    {
                        // ignored
                    }
                }
                tableNames = GetTableNames();
            }
        }

        private List<string> GetTableNames()
        {
            return _connection.Query<string>("SELECT name FROM sys.tables ORDER BY 1").ToList();
        }

        private void DropViews()
        {
            foreach (var viewName in GetViewNames())
            {
                _connection.Execute("DROP VIEW " + viewName );
                Console.WriteLine("Sucessfully dropped view: " + viewName);
            }
        }

        private IEnumerable<string> GetViewNames()
        {
            return _connection.Query<string>("SELECT name FROM sys.views ORDER BY 1");
        }

        private void DropFunctions()
        {
            foreach (var functionName in GetFunctionNames())
            {
                _connection.Execute("DROP FUNCTION " + functionName);
                Console.WriteLine("Sucessfully dropped view: " + functionName);
            }
        }

        private IEnumerable<string> GetFunctionNames()
        {
            return _connection.Query<string>("SELECT name FROM sys.objects WHERE type IN ('FN', 'IF', 'TF') ORDER BY 1");
        }
    }
}