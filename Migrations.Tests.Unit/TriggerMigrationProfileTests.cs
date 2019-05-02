using System.Collections.Generic;
using System.Linq;
using RootNameSpace.Migrations.Schema.Triggers;
using NUnit.Framework;
using RootNameSpace.Migrations.Schema.Triggers.SampleTrigger;

namespace Migrations.Tests.Unit
{
    [TestFixture]
    public class TriggerMigrationProfileTests
    {
        private IEnumerable<ITriggerBuilder> _actualTriggerBuilders;
            
        [SetUp]
        public void GivenATriggerMigrationProfile_WhenGetTriggerBuildersIsCalled()
        {
            var triggerMigrationProfile = new TriggerMigrationProfile();
            _actualTriggerBuilders = triggerMigrationProfile.GetTriggerBuilders();
        }

        [Test]
        public void ThenTheCreateTriggerIsReturned()
        {
            Assert.That(_actualTriggerBuilders.Any(builder => builder.GetType() == typeof(CreateTrigger)));
        }
    }
}
