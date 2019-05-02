using System.Data.SqlClient;

namespace RootNameSpace.Migrations
{
    public interface IConnectionStringBuilder
    {
        string GetConnectionString();
    }

    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        private readonly Options _migrationOptions;

        public ConnectionStringBuilder(Options migrationOptions)
        {
            _migrationOptions = migrationOptions;
        }

        public string GetConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(_migrationOptions.ConnectionString))
                return _migrationOptions.ConnectionString;

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = _migrationOptions.Server,
                InitialCatalog = _migrationOptions.Database,
            };

            if (_migrationOptions.IntegratedSecurity)
            {
                sqlConnectionStringBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlConnectionStringBuilder.UserID = _migrationOptions.Username;
                sqlConnectionStringBuilder.Password = _migrationOptions.Password;
            }

            return sqlConnectionStringBuilder.ConnectionString;
        }
    }
}