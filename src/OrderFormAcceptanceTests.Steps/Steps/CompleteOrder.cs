using Bogus.Extensions;
using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class CompleteOrder : TestBase
    {
        public CompleteOrder(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [When(@"the User chooses to complete the Order")]
        public void WhenTheUserChoosesToCompleteTheOrder()
        {
            Test.Pages.OrderForm.ClickCompleteOrderButton();
        }

        [Then(@"the confirm complete order screen is displayed")]
        public void ThenTheConfirmCompleteOrderScreenIsDisplayed()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Complete order").Should().BeTrue();
        }

        [Then(@"there is specific content related to the User answering 'yes' on the Funding Source question")]
        public void ThenThereIsSpecificContentRelatedToTheUserAnsweringYesOnTheFundingSourceQuestion()
        {
            Test.Pages.CompleteOrder.FundingSourceYesContentIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to complete order")]
        public void ThenThereIsAControlToCompleteOrder()
        {
            Test.Pages.CompleteOrder.CompleteOrderButtonIsDisplayed().Should().BeTrue();
        }

    }
}
