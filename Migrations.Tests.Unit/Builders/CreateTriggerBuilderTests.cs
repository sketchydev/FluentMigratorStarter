using System;
using System.Text;
using RootNameSpace.Migrations.Core.Builders;
using NUnit.Framework;

namespace Migrations.Tests.Unit.Builders
{
    [TestFixture]
    public class CreateTriggerBuilderTests
    {
        private ICreateTriggerBuilder _createTriggerBuilder;
        private string _expectedTriggerName;
        private string _expectedSchemaName;
        private string _expectedTableName;
        private bool _expectedAfterInsert;
        private bool _expectedAfterUpdate;
        private string _expectedBodySql;

        [SetUp]
        public void GivenACreateTriggerBuilder_WhenBuildingTheSql()
        {
            _expectedTriggerName = "testTrigger";
            _expectedSchemaName = "testSchema";
            _expectedTableName = "testTable";
            _expectedAfterInsert = true;
            _expectedAfterUpdate = true;
            _expectedBodySql = "SELECT 1 FROM Test";

            _createTriggerBuilder = new CreateTriggerBuilder();
            _createTriggerBuilder.SetTriggerName(_expectedTriggerName);
            _createTriggerBuilder.SetSchemaName(_expectedSchemaName);
            _createTriggerBuilder.OnTableName(_expectedTableName);
            _createTriggerBuilder.AfterInsert(_expectedAfterInsert);
            _createTriggerBuilder.AfterUpdate(_expectedAfterUpdate);            
            _createTriggerBuilder.SetBodySql(_expectedBodySql);
        }

        [Test]
        public void ThenTheCreateTriggerSqlShouldBeCorrectlyFormatted()
        {
            Assert.That(_createTriggerBuilder.GetCreateSql(), Is.EqualTo(GetExpectedCreateTriggerSql(_expectedSchemaName)));
        }

        [Test]
        public void WhenSchemaIsNull_ThenDefaultDboSchemaShouldBeUsed()
        {
            _createTriggerBuilder.SetSchemaName(null);
            Assert.That(_createTriggerBuilder.GetCreateSql(), Is.EqualTo(GetExpectedCreateTriggerSql("dbo")));
        }

        [Test]
        public void WhenSchemaIsEmpty_ThenDefaultDboSchemaShouldBeUsed()
        {
            _createTriggerBuilder.SetSchemaName(string.Empty);
            Assert.That(_createTriggerBuilder.GetCreateSql(), Is.EqualTo(GetExpectedCreateTriggerSql("dbo")));
        }

        [Test]
        public void WhenTriggerNameIsNull_ThenArgumentExceptionIsThrown()
        {
            _createTriggerBuilder.SetTriggerName(null);
            Assert.Throws<ArgumentException>(() => _createTriggerBuilder.GetCreateSql());
        }

        [Test]
        public void WhenTriggerNameIsEmpty_ThenFormatExceptionIsThrown()
        {
            _createTriggerBuilder.SetTriggerName(string.Empty);
            Assert.Throws<ArgumentException>(() => _createTriggerBuilder.GetCreateSql());
        }

        [Test]
        public void WhenOnTableNameIsNull_ThenArgumentExceptionIsThrown()
        {
            _createTriggerBuilder.OnTableName(null);
            Assert.Throws<ArgumentException>(() => _createTriggerBuilder.GetCreateSql());
        }

        [Test]
        public void WhenOnTableIsEmpty_ThenArgumentExceptionIsThrown()
        {
            _createTriggerBuilder.OnTableName(string.Empty);
            Assert.Throws<ArgumentException>(() => _createTriggerBuilder.GetCreateSql());
        }

        [Test]
        public void WhenInsertAndUpdateAreBothFalse_ThenAnArgumentExceptionIsThrown()
        {
            _createTriggerBuilder.AfterInsert(false);
            _createTriggerBuilder.AfterUpdate(false);
            Assert.Throws<ArgumentException>(() => _createTriggerBuilder.GetCreateSql());   
        }

        private string GetExpectedCreateTriggerSql(string expectedSchemaName)
        {
            return string.Format(@"
CREATE TRIGGER [{0}].[{1}]
    ON [{0}].[{2}]
    AFTER {3}
AS
BEGIN
    SET NOCOUNT ON

    {4}
END", 
                           expectedSchemaName, 
                           _expectedTriggerName,
                           _expectedTableName,
                           GetExpectedTriggerModes(),
                           _expectedBodySql);
        }

        private string GetExpectedTriggerModes()
        {
            var triggerModes = new StringBuilder();
            if (_expectedAfterInsert)
                triggerModes.Append("INSERT");
            if (!_expectedAfterUpdate) return triggerModes.ToString();
            
            if (triggerModes.Length > 0)
                triggerModes.Append(", ");
            triggerModes.Append("UPDATE");
            
            return triggerModes.ToString();
        }
    }
}