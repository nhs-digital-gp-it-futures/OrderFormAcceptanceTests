using FluentAssertions;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    internal sealed class PassPurchasingData : TestBase
    {
        public PassPurchasingData(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"a \.CSV is sent to the specified mailbox")]
        public async System.Threading.Tasks.Task ThenA_CSVIsSentToTheSpecifiedMailboxAsync()
        {
            var targetEmail = "noreply@buyingcatalogue.nhs.uk";
            var currentCount = await EmailServerDriver.GetEmailCountAsync(Test.Url, targetEmail);
            var precount = (int)Context["EmailCount"];
            currentCount.Should().BeGreaterThan(precount);
            var email = (await EmailServerDriver.FindAllEmailsAsync(Test.Url, targetEmail)).Last();
            Context.Add("SentEmail", email);
        }

        [Then(@"the \.CSV to the desired specification is produced \(call off-id only\)")]
        public void ThenThe_CSVToTheDesiredSpecificationIsProducedCallOff_IdOnly()
        {
            var email = (Email)Context["SentEmail"];
            //assert attachment is .csv and contains column name call-off-id?
            Context.Pending();
        }


    }
}
