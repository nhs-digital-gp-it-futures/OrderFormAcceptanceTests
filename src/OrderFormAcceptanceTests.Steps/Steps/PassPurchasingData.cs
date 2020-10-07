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
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using Bogus.DataSets;
using OrderFormAcceptanceTests.TestData;

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
            var currentCount = await Test.EmailServerDriver.GetEmailCountAsync();
            var precount = (int)Context[ContextKeys.EmailCount];
            currentCount.Should().BeGreaterThan(precount);
            var email = (await Test.EmailServerDriver.FindAllEmailsAsync()).Last();
            Context.Add(ContextKeys.SentEmail, email);
        }

        [Then(@"the \.CSV to the desired specification is produced \(call off-id only\)")]
        public void ThenThe_CSVToTheDesiredSpecificationIsProducedCallOff_IdOnly()
        {            
            var email = (Email)Context[ContextKeys.SentEmail];
            var attachmentFileName = email.Attachments.First().FileName;
            var attachmentFileType = email.Attachments.First().ContentType.MediaType;
            attachmentFileName.Should().EndWithEquivalent(".csv");
            attachmentFileType.Should().ContainEquivalentOf("csv");
            var data = email.Attachments.First().AttachmentData;
            var decoded = Encoding.UTF8.GetString(data.ToArray());
            var order = ((List<Order>)Context[ContextKeys.CreatedIncompleteOrders]).First();
            decoded.Should().ContainEquivalentOf("Call off Ordering Party Id");
            decoded.Should().ContainEquivalentOf(order.OrderId);
        }
    }
}
