namespace RootNameSpace.Migrations
{
    public static class Program
    {
        public static void Main(string[] args)
        {
                var options = new Options();
                CommandLine.Parser.Default.ParseArguments(args, options);

                var migrationManager = new MigrationManager(options);
                migrationManager.Run();
            
            
        }

    }
}