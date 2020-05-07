using OrderFormAcceptanceTests.Steps.Utils;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    public sealed class Hooks : TestBase
    {
        public Hooks(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Test.Driver?.Close();
            Test.Driver?.Quit();
        }
    }
}
