using System;
using System.Text;

namespace RootNameSpace.Migrations.Core.Builders
{
    public interface IIntegrityTriggerBodyBuilder
    {
        IIntegrityTriggerBodyBuilder SetTriggerColumnName(string triggerColumnName);
        IIntegrityTriggerBodyBuilder SetReferenceTableColumnName(string referenceTableColumnName);
        IIntegrityTriggerBodyBuilder SetReferenceTableName(string referenceTableName);
        IIntegrityTriggerBodyBuilder Add();
        
        string CreateBodySql();
    }

    public class IntegrityTriggerBodyBuilder : IIntegrityTriggerBodyBuilder
    {
        private const string IntegrityCheckSql = @"
    IF EXISTS (SELECT 1 FROM INSERTED WHERE {0} IS NOT NULL)
    BEGIN
        IF NOT EXISTS ( SELECT 1 
                        FROM {1} t
                        JOIN INSERTED i ON i.{0} = t.{2}
                        AND t.ExpiredDateTime IS NULL)
        BEGIN
            RAISERROR('Can not insert {0} that does not exist or has expired.', 16, 1);
			ROLLBACK TRANSACTION;
        END
    END";
        private string _triggerColumnName;
        private string _referenceTableColumnName;
        private string _referenceTableName;

        private readonly StringBuilder _sqlBody = new StringBuilder();

        public IIntegrityTriggerBodyBuilder SetTriggerColumnName(string triggerColumnName)
        {
            _triggerColumnName = triggerColumnName;
            return this;
        }

        public IIntegrityTriggerBodyBuilder SetReferenceTableColumnName(string referenceTableColumnName)
        {
            _referenceTableColumnName = referenceTableColumnName;
            return this;
        }

        public IIntegrityTriggerBodyBuilder SetReferenceTableName(string referenceTableName)
        {
            _referenceTableName = referenceTableName;
            return this;
        }

        public IIntegrityTriggerBodyBuilder Add()
        {
            if (string.IsNullOrWhiteSpace(_triggerColumnName))
                throw new ArgumentException("Trigger Name can not be null");

            if (string.IsNullOrWhiteSpace(_referenceTableName))
                throw new ArgumentException("Reference Table Name can not be null");

            if (string.IsNullOrWhiteSpace(_referenceTableColumnName))
                throw new ArgumentException("Reference Table Column Name can not be null");

            var integritySql = string.Format(IntegrityCheckSql,
                        _triggerColumnName,
                        _referenceTableName,
                        _referenceTableColumnName);

            _sqlBody.AppendLine(integritySql);

            _triggerColumnName = null;
            _referenceTableName = null;
            _referenceTableColumnName = null;

            return this;
        }

        public string CreateBodySql()
        {
            return _sqlBody.ToString();
        }

    }
}