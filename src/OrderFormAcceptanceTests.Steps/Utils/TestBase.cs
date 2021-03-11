namespace OrderFormAcceptanceTests.Steps.Utils
{
    using OrderFormAcceptanceTests.Persistence.Data;
    using TechTalk.SpecFlow;

    public abstract class TestBase
    {
        protected TestBase(UITest test, ScenarioContext context)
        {
            Test = test;
            Context = context;
            DbContext = DbContext ?? (OrderingDbContext)Context[ContextKeys.DbContext];
        }

        internal ScenarioContext Context { get; }

        internal UITest Test { get; }

        internal OrderingDbContext DbContext { get; }
    }
}
