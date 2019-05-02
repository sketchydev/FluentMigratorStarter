using System.Collections;
using System.Collections.Generic;

namespace RootNameSpace.Migrations.Seed.TestData.SampleDataTypeA
{
    public class SampleDataTypeATestData : IEnumerable<SampleClass>
    {
        private static readonly SampleClass RecordOne = new SampleClass
        {
            Id = 12345,
            ColumnA = "A",
            ColumnB = "This Is A longer string"
        };

        private static readonly SampleClass RecordTwo = new SampleClass
        {
            Id = 54321,
            ColumnA = "S",
            ColumnB = "some text"
        };
        
        private readonly IEnumerable<SampleClass> _data = new List<SampleClass>
        {
            RecordOne,
            RecordTwo
        };

        public IEnumerator<SampleClass> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class SampleClass
    {
        public long Id { get; set; }
        public string ColumnA { get; set; }
        public string ColumnB { get; set; }
        
    }
}