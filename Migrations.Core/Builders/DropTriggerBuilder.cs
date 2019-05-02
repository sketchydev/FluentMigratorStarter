using System;

namespace RootNameSpace.Migrations.Core.Builders
{
    public interface IDropTriggerBuilder
    {
        IDropTriggerBuilder SetTriggerName(string triggerName);
        IDropTriggerBuilder SetSchemaName(string schemaName);
        string GetDropSql();
    }

    public class DropTriggerBuilder : IDropTriggerBuilder
    {
        private const string DefaultSchema = "dbo";
        private string _triggerName;
        private string _schemaName;

        public IDropTriggerBuilder SetTriggerName(string triggerName)
        {
            _triggerName = triggerName;
            return this;
        }

        public IDropTriggerBuilder SetSchemaName(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public string GetDropSql()
        {
            if (string.IsNullOrWhiteSpace(_triggerName))
                throw new ArgumentException("Trigger Name can not be null or empty");

            var schema = !string.IsNullOrWhiteSpace(_schemaName) ? _schemaName : DefaultSchema;

            const string dropTriggerFormat = @"
IF EXISTS (select 1 from sys.triggers where name = '{1}')
    DROP TRIGGER [{0}].[{1}]";
            
            return String.Format(dropTriggerFormat, schema, _triggerName);
        }
    }
}