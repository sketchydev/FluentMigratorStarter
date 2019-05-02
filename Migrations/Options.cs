using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace RootNameSpace.Migrations
{
    public enum ModeType
    {
        Destroy,
        Schema,
        Seed,
        DropAllSchema
    }

    public class Options
    {
        [Option("modes")]
        public string ModeValues { get; set; }

        public IList<ModeType> GetModeTypes()
        {
            return ModeValues.Split(' ').Select(s => (ModeType) Enum.Parse(typeof(ModeType), s, true)).ToList();
        }

        [Option("seed-profiles")]
        public string SeedProfiles { get; set; }

        public string[] GetSeedProfiles()
        {
            if (SeedProfiles != null)
                return SeedProfiles.Split(' ').ToArray();
            return new[] {"static-data"};
        }

        [Option('s', "server")]
        public string Server { get; set; }
        
        [Option('c', "connection-string")]
        public string ConnectionString { get; set; }

        [Option('d', "database")]
        public string Database { get; set; }

        [Option('i', "integrated-security")]
        public bool IntegratedSecurity { get; set; }

        [Option('u', "username")]
        public string Username { get; set; }

        [Option('p', "password")]
        public string Password { get; set; }

        [Option("silent")]
        public bool Silent { get; set; }
    }
}