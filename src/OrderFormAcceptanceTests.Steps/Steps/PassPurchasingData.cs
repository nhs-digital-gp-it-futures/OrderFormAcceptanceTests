using FluentAssertions;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Threading.Tasks;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    internal sealed class PassPurchasingData : TestBase
    {
        public PassPurchasingData(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"a \.CSV is sent to the specified mailbox")]
        public async Task ThenA_CSVIsSentToTheSpecifiedMailboxAsync()
        {
            var targetEmail = "alicesmith@email.com";
            var currentCount = await EmailServerDriver.GetEmailCountAsync(Test.Url, targetEmail);
            var precount = (int)Context[ContextKeys.EmailCount];
            currentCount.Should().BeGreaterThan(precount);
            var email = (await EmailServerDriver.FindAllEmailsAsync(Test.Url, targetEmail)).Last();
            Context.Add(ContextKeys.SentEmail, email);
        }

        [Then(@"the \.CSV to the desired specification is produced \(call off-id only\)")]
        public void ThenThe_CSVToTheDesiredSpecificationIsProducedCallOff_IdOnly()
        {
            var email = (Email)Context[ContextKeys.SentEmail];
            var attachmentFileName = email.Attachment.Name;
            var attachmentFileType = email.Attachment.ContentType.MediaType;
            attachmentFileName.Should().EndWithEquivalent(".csv");
            attachmentFileType.Should().ContainEquivalentOf("csv");
            email.Attachment.ContentAsString.Should().ContainEquivalentOf("CallOffPartyId");            
        }
    }
}
