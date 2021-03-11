namespace OrderFormAcceptanceTests.TestData.Models
{
    using OrderFormAcceptanceTests.Domain;

    public class TimeUnitModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        internal TimeUnit? ToTimeUnit() => OrderingEnums.ParseTimeUnit(Name);
    }
}
