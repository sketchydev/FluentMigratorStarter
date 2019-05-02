using System;
using RootNameSpace.Migrations.Core.Builders;
using NUnit.Framework;

namespace Migrations.Tests.Unit.Builders
{
    [TestFixture]
    public class DropTriggerBuilderTests
    {
        private string _expectedTriggerName;
        private string _expectedSchemaName;
        private IDropTriggerBuilder _dropTriggerBuilder;

        const string DropTriggerFormat = @"
IF EXISTS (select 1 from sys.triggers where name = '{1}')
    DROP TRIGGER [{0}].[{1}]";

        [SetUp]
        public void GivenADropTriggerBuilder_WhenBuildingTheSql()
        {
            _expectedTriggerName = "testTrigger";
            _expectedSchemaName = "testSchema";

            _dropTriggerBuilder = new DropTriggerBuilder();
            _dropTriggerBuilder.SetTriggerName(_expectedTriggerName);
            _dropTriggerBuilder.SetSchemaName(_expectedSchemaName);
        }

        [Test]
        public void ThenTheDropTriggerSqlIsCorrectlyFormatted()
        {
            Assert.That(_dropTriggerBuilder.GetDropSql(),
                Is.EqualTo(String.Format(DropTriggerFormat, _expectedSchemaName, _expectedTriggerName)));
        }

        [Test]
        public void WhenSchemaIsNull_ThenSchemaIsDefaultedToDbo()
        {
            _dropTriggerBuilder.SetSchemaName(null);
            Assert.That(_dropTriggerBuilder.GetDropSql(),
                Is.EqualTo(String.Format(DropTriggerFormat, "dbo", _expectedTriggerName)));
        }

        [Test]
        public void WhenSchemaIsEmpty_ThenSchemaIsDefaultedToDbo()
        {
            _dropTriggerBuilder.SetSchemaName(string.Empty);
            Assert.That(_dropTriggerBuilder.GetDropSql(),
                Is.EqualTo(String.Format(DropTriggerFormat, "dbo", _expectedTriggerName)));
        }

        [Test]
        public void WhenTriggerIsNull_ThenArgumentExceptionIsThrown()
        {
            _dropTriggerBuilder.SetTriggerName(null);
            Assert.Throws<ArgumentException>(() => _dropTriggerBuilder.GetDropSql());
        }

        [Test]
        public void WhenTriggerIsEmpty_ThenArgumentExceptionIsThrown()
        {
            _dropTriggerBuilder.SetTriggerName(string.Empty);
            Assert.Throws<ArgumentException>(() => _dropTriggerBuilder.GetDropSql());
        }
    }
}