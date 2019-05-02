using RootNameSpace.Migrations.Core;
using RootNameSpace.Migrations.Core.Builders;

namespace RootNameSpace.Migrations.Schema.Triggers.SampleTrigger
{
    public class CreateTrigger : ITriggerBuilder
    {
        public string DropTriggerSql()
        {
            var dropTriggerBuilder = new DropTriggerBuilder();
            dropTriggerBuilder.SetTriggerName("trSampleTrigger");

            return dropTriggerBuilder.GetDropSql();
        }

        public string CreateTriggerSql()
        {
            var integrityBodyBuilder = new IntegrityTriggerBodyBuilder();
            integrityBodyBuilder.SetTriggerColumnName("Column A")
                                .SetReferenceTableName(Constants.TableNames.TableNameConstant)
                                .Add();

            var createTriggerBuilder = new CreateTriggerBuilder();
            createTriggerBuilder.SetTriggerName("trSampleTrigger")
                                .OnTableName(Constants.TableNames.TableNameConstant)
                                .AfterInsert(true)
                                .AfterUpdate(true)
                                .SetBodySql(integrityBodyBuilder.CreateBodySql());

            return createTriggerBuilder.GetCreateSql();
        }
    }
}