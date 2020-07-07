﻿using FluentAssertions;
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

        [StepDefinition(@"the Order Summary is displayed")]
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
            var expectedDate = DateTime.Now.ToString("d MMMM yyyy");
            date.Should().EndWithEquivalent(expectedDate);
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
            value.Should().NotBeNullOrEmpty();;
            var createdAddress = (Address)Context["CreatedSupplierAddress"];
            var createdContact = (Contact)Context["CreatedSupplierContact"];
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
            var expectedDate = ((DateTime)order.CommencementDate).ToString("d MMMM yyyy");
            date.Should().EndWithEquivalent(expectedDate);
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
            id.Should().EndWith(order.OrderId);
        }

        [Then(@"the Order description data saved in the order")]
        public void ThenTheOrderDescriptionDataSavedInTheOrder()
        {
            var order = (Order)Context["CreatedOrder"];
            var actualValue = Test.Pages.OrderForm.GetOrderDescription();
            actualValue.Should().BeEquivalentTo(order.Description);
        }

        [Then(@"the Order items \(recurring cost\) table is populated")]
        public void ThenTheOrderItemsRecurringCostTableIsPopulated()
        {
            Test.Pages.OrderForm.RecurringCostsTableIsPopulated().Should().BeTrue();
        }

        [Then(@"the Recipient name \(ODS code\) of each item is the concatenation ""\[Service Recipient name\] \[\(ODS code\)\]""")]
        public void ThenTheRecipientNameODSCodeOfEachItemIsTheConcatenation()
        {
            var name = Test.Pages.OrderForm.GetItemRecipientName();
            var expectedServiceRecipient = (ServiceRecipient)Context["CreatedServiceRecipient"];
            var expectedValue = string.Format("{0} ({1})", expectedServiceRecipient.Name, expectedServiceRecipient.OdsCode);
            name.Should().Be(expectedValue);
        }

        [Then(@"the item ID of each item is displayed")]
        public void ThenTheItemIDOfEachItemIsDisplayed()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var id = Test.Pages.OrderForm.GetItemId();
            id.Should().Be(expectedOrderItem.OrderItemId.ToString());
        }

        [Then(@"the item name of each item is the Catalogue Solution name")]
        public void ThenTheItemNameOfEachItemIsTheCatalogueSolutionName()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var name = Test.Pages.OrderForm.GetItemName();
            name.Should().Be(expectedOrderItem.CatalogueItemName);
        }

        [Then(@"the Price unit of order of each item is the concatenation ""\[Price\] \[unit\]""")]
        public void ThenThePriceUnitOfOrderOfEachItemIsTheConcatenation()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedValue = string.Format("{0} {1}", expectedOrderItem.Price, expectedOrderItem.PricingUnitDescription);
            var price = Test.Pages.OrderForm.GetItemPrice();
            price.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per year")]
        [Then(@"the Quantity of each item is the concatenation ""(.*)"" i\.e\. \[Quantity] per month")]
        public void ThenTheQuantityOfEachItemIsTheConcatenationI_E_QuantityPerPeriod()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedValue = string.Format("{0} {1}", expectedOrderItem.Quantity, expectedOrderItem.EstimationPeriodId);
            var quantity = Test.Pages.OrderForm.GetItemQuantity();
            quantity.Should().Be(expectedValue);
        }

        [Then(@"the Planned delivery date of each item is displayed")]
        public void ThenThePlannedDeliveryDateOfEachItemIsDisplayed()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var date = Test.Pages.OrderForm.GetItemPlannedDate();
            date.Should().Be(expectedOrderItem.DeliveryDate.ToString("d MMMM YYYY"));
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] rounded up to two decimal places")]
        public void ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityRoundedUpToTwoDecimalPlaces()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedCost = expectedOrderItem.Price * expectedOrderItem.Quantity;
            var expectedValue = Math.Round(expectedCost, 2);
            var cost = Test.Pages.OrderForm.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] \* 12 rounded up to two decimal places")]
        public void ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityTimes12RoundedUpToTwoDecimalPlaces()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedCost = expectedOrderItem.Price * expectedOrderItem.Quantity * 12;
            var expectedValue = Math.Round(expectedCost, 2);
            var cost = Test.Pages.OrderForm.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per year is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            var orderItem = GenerateOrderItemWithFlatPricedVariableOnDemand();
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per month is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            var orderItem = GenerateOrderItemWithFlatPricedVariableOnDemand();
            orderItem.EstimationPeriodId = 1;
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        private OrderItem GenerateOrderItemWithFlatPricedVariableOnDemand()
        {
            var order = (Order)Context["CreatedOrder"];
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-001",
                CatalogueItemTypeId = 1,
                CatalogueItemName = "Write on Time",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = 3,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = "consultations",
                PricingUnitName = "consultation",
                PricingUnitDescription = "per consultation",
                TimeUnitId = null,
                CurrencyCode = "GBP",
                Quantity = 1111,
                EstimationPeriodId = 2,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 1001.010M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

    }
}