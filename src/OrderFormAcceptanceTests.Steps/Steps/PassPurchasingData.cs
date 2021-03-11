namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Steps.Utils;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class PassPurchasingData : TestBase
    {
        public PassPurchasingData(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Then(@"a \.CSV is sent to the specified mailbox")]
        public async Task ThenA_CSVIsSentToTheSpecifiedMailboxAsync()
        {
            var currentCount = await Test.EmailServerDriver.GetEmailCountAsync();
            var precount = (int)Context[ContextKeys.EmailCount];
            currentCount.Should().BeGreaterThan(precount);
            var emails = await Test.EmailServerDriver.FindAllEmailsAsync();
            emails.Count.Should().BeGreaterThan(0);
        }
    }
}
