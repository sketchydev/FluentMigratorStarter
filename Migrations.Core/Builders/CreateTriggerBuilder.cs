using System;
using System.Text;

namespace RootNameSpace.Migrations.Core.Builders
{
    public interface ICreateTriggerBuilder
    {
        ICreateTriggerBuilder SetTriggerName(string triggerName);
        ICreateTriggerBuilder SetSchemaName(string schemaName);
        ICreateTriggerBuilder OnTableName(string tableName);
        ICreateTriggerBuilder AfterInsert(bool afterInsert);
        ICreateTriggerBuilder AfterUpdate(bool afterUpdate);
        ICreateTriggerBuilder SetBodySql(string bodySql);

        string GetCreateSql();
    }

    public class CreateTriggerBuilder : ICreateTriggerBuilder
    {
        private const string CreateTriggerBodyTemplate = @"
CREATE TRIGGER [{0}].[{1}]
    ON [{0}].[{2}]
    AFTER {3}
AS
BEGIN
    SET NOCOUNT ON

    {4}
END";
        private const string DefaultSchema = "dbo";
        private string _triggerName;
        private string _schemaName;
        private string _onTableName;
        private bool _afterInsert;
        private bool _afterUpdate;
        private string _bodySql;

        public ICreateTriggerBuilder SetTriggerName(string triggerName)
        {
            _triggerName = triggerName;
            return this;
        }

        public ICreateTriggerBuilder SetSchemaName(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public ICreateTriggerBuilder OnTableName(string tableName)
        {
            _onTableName = tableName;
            return this;
        }

        public ICreateTriggerBuilder AfterInsert(bool afterInsert)
        {
            _afterInsert = afterInsert;
            return this;
        }

        public ICreateTriggerBuilder AfterUpdate(bool afterUpdate)
        {
            _afterUpdate = afterUpdate;
            return this;
        }

        public ICreateTriggerBuilder SetBodySql(string bodySql)
        {
            _bodySql = bodySql;
            return this;
        }

        public string GetCreateSql()
        {
            if (string.IsNullOrWhiteSpace(_triggerName))
                throw new ArgumentException("Trigger Name can not be null or empty");

            if (string.IsNullOrWhiteSpace(_onTableName))
                throw new ArgumentException("Table name can not be null or empty");

            if (!_afterInsert && !_afterUpdate) 
                throw new ArgumentException("At least AfterInsert or AfterUpdate must be true");

            var schema = !string.IsNullOrWhiteSpace(_schemaName) ? _schemaName : DefaultSchema;

            return string.Format(CreateTriggerBodyTemplate,
                           schema,
                           _triggerName,
                           _onTableName,
                           GetTriggerModes(),
                           _bodySql);
        }

        private string GetTriggerModes()
        {
            var triggerModes = new StringBuilder();
            if (_afterInsert)
                triggerModes.Append("INSERT");
            if (!_afterUpdate) return triggerModes.ToString();

            if (triggerModes.Length > 0)
                triggerModes.Append(", ");
            triggerModes.Append("UPDATE");

            return triggerModes.ToString();
        }
    }
}