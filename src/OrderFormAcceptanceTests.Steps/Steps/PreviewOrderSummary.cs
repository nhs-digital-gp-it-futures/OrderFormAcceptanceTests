using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class PreviewOrderSummary : TestBase
    {

        public PreviewOrderSummary(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [When(@"the User chooses to preview the Order Summary")]
        public void WhenTheUserChoosesToPreviewTheOrderSummary()
        {
            Test.Pages.OrderForm.ClickPreviewOrderButton();
        }

        [Then(@"the Order Summary is presented")]
        public void ThenTheOrderSummaryIsPresented()
        {
            Test.Pages.OrderForm.TextDisplayedInPageTitle("Order summary").Should().BeTrue();
        }

        [Then(@"the date the Order Summary was produced")]
        public void ThenTheDateTheOrderSummaryWasProduced()
        {
            var date = Test.Pages.OrderForm.GetDateOrderSummaryCreatedValue();
            date.Should().NotBeNullOrEmpty();
        }

        [Then(@"the Call Off Ordering Party information is displayed")]
        public void ThenTheCallOffOrderingPartyInformationIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetCallOffOrderingPartyPreviewValue();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the Supplier information is displayed")]
        public void ThenTheSupplierInformationIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetSupplierPreviewValue();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the Commencement date is displayed")]
        public void ThenTheCommencementDateIsDisplayed()
        {
            var date = Test.Pages.OrderForm.GetCommencementDateValue();
            date.Should().NotBeNullOrEmpty();
        }

        [Then(@"Order items \(one-off cost\) table is displayed")]
        public void ThenOrderItemsOne_OffCostTableIsDisplayed()
        {
            Test.Pages.OrderForm.OneOffCostsTableIsDiaplyed().Should().BeTrue();
        }

        [Then(@"Order items \(recurring cost\) table is displayed")]
        public void ThenOrderItemsRecurringCostTableIsDisplayed()
        {
            Test.Pages.OrderForm.RecurringCostsTableIsDiaplyed().Should().BeTrue();
        }

        [Then(@"the total one-off cost is displayed")]
        public void ThenTheTotalOne_OffCostIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetTotalOneOffCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the total monthly cost is displayed")]
        public void ThenTheTotalMonthlyCostIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetTotalMonthlyCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the total annual cost is displayed")]
        public void ThenTheTotalAnnualCostIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetTotalAnnualCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the total cost of ownership is displayed")]
        public void ThenTheTotalCostOfOwnershipIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetTotalOwnershipCost();
            value.Should().NotBeNullOrEmpty();
        }


    }
}
