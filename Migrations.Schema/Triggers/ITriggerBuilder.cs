namespace RootNameSpace.Migrations.Schema.Triggers
{
    public interface ITriggerBuilder
    {
        string DropTriggerSql();
        string CreateTriggerSql();
    }
}