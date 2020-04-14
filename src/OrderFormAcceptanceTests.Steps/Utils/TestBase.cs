using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    public abstract class TestBase
    {
        internal readonly ScenarioContext Context;
        internal readonly UITest Test;

        protected TestBase(UITest test, ScenarioContext context)
        {
            Test = test;
            Context = context;
        }
    }
}
