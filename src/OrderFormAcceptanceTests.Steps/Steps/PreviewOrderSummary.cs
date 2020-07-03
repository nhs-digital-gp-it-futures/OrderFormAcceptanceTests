using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
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

        [Given(@"the Order Summary is displayed")]
        public void GivenTheOrderSummaryIsDisplayed()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            WhenTheUserChoosesToPreviewTheOrderSummary();
            ThenTheOrderSummaryIsPresented();
        }

        [Then(@"the date the Order Summary was produced")]
        public void ThenTheDateTheOrderSummaryWasProduced()
        {
            var date = Test.Pages.OrderForm.GetDateOrderSummaryCreatedValue();
            date.Should().NotBeNullOrEmpty();
            var expectedDate = DateTime.Now.ToString("dd MMM yyyy");
            date.Should().BeEquivalentTo(expectedDate);
        }

        [Then(@"the Call Off Ordering Party information is displayed")]
        [Then(@"the Call-off Ordering Party data saved in the order")]
        [Then(@"the Call-off Ordering Party names are concatenated")]
        public void ThenTheCallOffOrderingPartyInformationIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetCallOffOrderingPartyPreviewValue();
            value.Should().NotBeNullOrEmpty();
            var order = (Order)Context["CreatedOrder"];            
            var createdAddress = (Address)Context["CreatedAddress"];
            var createdContact = (Contact)Context["CreatedContact"];
            value.Should().ContainEquivalentOf(order.OrganisationName);
            value.Should().ContainEquivalentOf(createdAddress.Line1);
            value.Should().ContainEquivalentOf(createdAddress.Town);
            value.Should().ContainEquivalentOf(createdAddress.Postcode);
            var concattedName = string.Format("{0} {1}", createdContact.FirstName, createdContact.LastName);
            value.Should().ContainEquivalentOf(concattedName);
        }

        [Then(@"the Supplier information is displayed")]
        [Then(@"the Supplier data saved in the order")]
        [Then(@"the Supplier first name and last name are concatenated")]
        public void ThenTheSupplierInformationIsDisplayed()
        {
            var value = Test.Pages.OrderForm.GetSupplierPreviewValue();
            value.Should().NotBeNullOrEmpty();
            var order = (Order)Context["CreatedOrder"];
            var createdAddress = (Address)Context["CreatedSupplierAddress"];
            var createdContact = (Contact)Context["CreatedSupplierContact"];
            value.Should().ContainEquivalentOf(order.OrganisationName);
            value.Should().ContainEquivalentOf(createdAddress.Line1);
            value.Should().ContainEquivalentOf(createdAddress.Town);
            value.Should().ContainEquivalentOf(createdAddress.Postcode);
            var concattedName = string.Format("{0} {1}", createdContact.FirstName, createdContact.LastName);
            value.Should().ContainEquivalentOf(concattedName);
        }

        [Then(@"the Commencement date is displayed")]
        [Then(@"the Commencement date data saved in the order")]
        public void ThenTheCommencementDateIsDisplayed()
        {
            var date = Test.Pages.OrderForm.GetCommencementDateValue();
            date.Should().NotBeNullOrEmpty();
            var order = (Order)Context["CreatedOrder"];
            var expectedDate = ((DateTime)order.CommencementDate).ToString("dd MMM yyyy");
            date.Should().BeEquivalentTo(expectedDate);
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

        [Then(@"it displays the Call-off Agreement ID data")]
        public void ThenItDisplaysTheCall_OffAgreementIDData()
        {
            var order = (Order)Context["CreatedOrder"];
            var id = Test.Pages.OrderForm.GetCallOffId();
            id.Should().BeEquivalentTo(order.OrderId);
        }

        [Then(@"the Order description data saved in the order")]
        public void ThenTheOrderDescriptionDataSavedInTheOrder()
        {
            var order = (Order)Context["CreatedOrder"];
            var actualValue = Test.Pages.OrderForm.GetOrderDescription();
            actualValue.Should().BeEquivalentTo(order.Description);
        }

    }
}
