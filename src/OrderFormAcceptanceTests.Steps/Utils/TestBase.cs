namespace OrderFormAcceptanceTests.Steps.Utils
{
    using TechTalk.SpecFlow;

    public abstract class TestBase
    {
        protected TestBase(UITest test, ScenarioContext context)
        {
            Test = test;
            Context = context;
        }

        internal ScenarioContext Context { get; }

        internal UITest Test { get; }
    }
}
