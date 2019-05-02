using System;
using RootNameSpace.Migrations.Core.Builders;
using NUnit.Framework;

namespace Migrations.Tests.Unit.Builders
{
    [TestFixture]
    public class IntegrityTriggerBodyBuilderTests
    {
        private IIntegrityTriggerBodyBuilder _integrityTriggerBodyBuilder;
        private string _expectedTriggerColumnName;
        private string _expectedReferenceTableName;
        private string _expectedReferenceTableColumnName;

        [SetUp]
        public void GivenAnIntegrityTriggerBodyBuilder_WhenBuildingASingleIntegrityCheck()
        {
            _expectedTriggerColumnName = "Column_Name";
            _expectedReferenceTableName = "Table_Name";
            _expectedReferenceTableColumnName = "Ref_Column_Name";

            _integrityTriggerBodyBuilder = new IntegrityTriggerBodyBuilder();
            _integrityTriggerBodyBuilder.SetTriggerColumnName(_expectedTriggerColumnName);
            _integrityTriggerBodyBuilder.SetReferenceTableName(_expectedReferenceTableName);
            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(_expectedReferenceTableColumnName);
            _integrityTriggerBodyBuilder.Add();
        }

        [Test]
        public void ThenTheTriggerBodySqlIsCorrectlyFormatted()
        {
            var bodySql = _integrityTriggerBodyBuilder.CreateBodySql()
                                                      .Replace(Environment.NewLine, string.Empty)
                                                      .Replace('\n', '\0');
            Assert.That(bodySql, Is.EqualTo(GetExpectedBodySql()));
        }

        [Test]
        public void WhenTriggerColumnNameIsNull_ThenAnArgumentExceptionIsThrown()
        {
            _integrityTriggerBodyBuilder.SetReferenceTableName(_expectedReferenceTableName);
            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(_expectedReferenceTableColumnName);

            _integrityTriggerBodyBuilder.SetTriggerColumnName(null);
            Assert.Throws<ArgumentException>(() => _integrityTriggerBodyBuilder.Add());
        }
        
        [Test]
        public void WhenTriggerColumnNameIsEmpty_ThenAnArgumentExceptionIsThrown()
        {
            _integrityTriggerBodyBuilder.SetReferenceTableName(_expectedReferenceTableName);
            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(_expectedReferenceTableColumnName);

            _integrityTriggerBodyBuilder.SetTriggerColumnName(null);
            Assert.Throws<ArgumentException>(() => _integrityTriggerBodyBuilder.Add());
        }

        [Test]
        public void WhenReferenceTableNameIsNull_ThenAnArgumentExceptionIsThrown()
        {
            _integrityTriggerBodyBuilder.SetTriggerColumnName(_expectedTriggerColumnName);
            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(_expectedReferenceTableColumnName);

            _integrityTriggerBodyBuilder.SetReferenceTableName(null);
            Assert.Throws<ArgumentException>(() => _integrityTriggerBodyBuilder.Add());
        }

        [Test]
        public void WhenReferenceTableNameIsEmpty_ThenAnArgumentExceptionIsThrown()
        {
            _integrityTriggerBodyBuilder.SetTriggerColumnName(_expectedTriggerColumnName);
            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(_expectedReferenceTableColumnName);

            _integrityTriggerBodyBuilder.SetReferenceTableName(null);
            Assert.Throws<ArgumentException>(() => _integrityTriggerBodyBuilder.Add());
        }

        [Test]
        public void WhenReferenceTableColumnNameIsNull_ThenAnArgumentExceptionIsThrown()
        {
            _integrityTriggerBodyBuilder.SetTriggerColumnName(_expectedTriggerColumnName);
            _integrityTriggerBodyBuilder.SetReferenceTableName(_expectedReferenceTableName);

            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(null);
            Assert.Throws<ArgumentException>(() => _integrityTriggerBodyBuilder.Add());
        }

        [Test]
        public void WhenReferenceTableColumnNameIsEmpty_ThenAnArgumentExceptionIsThrown()
        {
            _integrityTriggerBodyBuilder.SetTriggerColumnName(_expectedTriggerColumnName);
            _integrityTriggerBodyBuilder.SetReferenceTableName(_expectedReferenceTableName);

            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(null);
            Assert.Throws<ArgumentException>(() => _integrityTriggerBodyBuilder.Add());
        }

        [Test]
        public void WhenAddingMultipleIntegrityChecks_ThenTheChecksAreAppendedTogether()
        {
            _integrityTriggerBodyBuilder.SetTriggerColumnName(_expectedTriggerColumnName);
            _integrityTriggerBodyBuilder.SetReferenceTableName(_expectedReferenceTableName);
            _integrityTriggerBodyBuilder.SetReferenceTableColumnName(_expectedReferenceTableColumnName);
            _integrityTriggerBodyBuilder.Add();

            var bodySql = _integrityTriggerBodyBuilder.CreateBodySql()
                                                      .Replace(Environment.NewLine, string.Empty)
                                                      .Replace('\n', '\0');
            Assert.That(bodySql, Is.EqualTo(GetExpectedBodySql() + GetExpectedBodySql()));
        }

        private string GetExpectedBodySql()
        {
            return string.Format(@"
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
    END",
                        _expectedTriggerColumnName,
                        _expectedReferenceTableName,
                        _expectedReferenceTableColumnName)
                .Replace(Environment.NewLine, string.Empty)
                .Replace('\n', '\0');
        }
    }
}