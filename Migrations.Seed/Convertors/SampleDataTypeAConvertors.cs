using RootNameSpace.Migrations.Seed.TestData.SampleDataTypeA;

namespace RootNameSpace.Migrations.Seed.Convertors
{
    public static class SampleDataTypeAConvertors
    {
        public static object ToSeedObject(this SampleClass sampleClass)
        {
            return new
            {
                sampleClass.Id,
                sampleClass.ColumnA,
                sampleClass.ColumnB
            };
        }
    }
}