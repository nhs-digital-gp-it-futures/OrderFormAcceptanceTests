namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class PreviewOrderSummary : TestBase
    {
        public PreviewOrderSummary(UITest test, ScenarioContext context)
            : base(test, context)
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
            var date = Test.Pages.PreviewOrderSummary.GetDateOrderSummaryCreatedValue();
            date.Should().NotBeNullOrEmpty();
            var expectedDate = DateTime.Now.ToString("d MMMM yyyy");
            date.Should().EndWithEquivalent(expectedDate);
        }

        [Then(@"the Call Off Ordering Party information is displayed")]
        [Then(@"the Call-off Ordering Party data saved in the order")]
        [Then(@"the Call-off Ordering Party names are concatenated")]
        public async Task ThenTheCallOffOrderingPartyInformationIsDisplayedAsync()
        {
            var value = Test.Pages.PreviewOrderSummary.GetCallOffOrderingPartyPreviewValue();
            value.Should().NotBeNullOrEmpty();
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var createdAddress = order.OrderingParty.Address;
            var createdContact = order.OrderingPartyContact;
            value.Should().ContainEquivalentOf(createdAddress.Line1);
            value.Should().ContainEquivalentOf(createdAddress.Town);
            value.Should().ContainEquivalentOf(createdAddress.Postcode);
            var concattedName = string.Format("{0} {1}", createdContact.FirstName, createdContact.LastName);
            value.Should().ContainEquivalentOf(concattedName);
        }

        [Then(@"the Supplier information is displayed")]
        [Then(@"the Supplier data saved in the order")]
        [Then(@"the Supplier first name and last name are concatenated")]
        public async Task ThenTheSupplierInformationIsDisplayedAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var value = Test.Pages.PreviewOrderSummary.GetSupplierPreviewValue();
            value.Should().NotBeNullOrEmpty();
            var createdAddress = order.Supplier.Address;
            var createdContact = order.SupplierContact;
            value.Should().ContainEquivalentOf(createdAddress.Line1);

            if (createdAddress.Town is not null)
            {
                value.Should().ContainEquivalentOf(createdAddress.Town);
            }

            value.Should().ContainEquivalentOf(createdAddress.Postcode);
            var concattedName = string.Format("{0} {1}", createdContact.FirstName, createdContact.LastName);
            value.Should().ContainEquivalentOf(concattedName);
        }

        [Then(@"the Commencement date is displayed")]
        [Then(@"the Commencement date data saved in the order")]
        public async Task ThenTheCommencementDateIsDisplayedAsync()
        {
            var date = Test.Pages.PreviewOrderSummary.GetCommencementDateValue();
            date.Should().NotBeNullOrEmpty();
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedDate = order.CommencementDate?.ToString("d MMMM yyyy");
            date.Should().EndWithEquivalent(expectedDate);
        }

        [Then(@"Order items \(one-off cost\) table is displayed")]
        public void ThenOrderItemsOne_OffCostTableIsDisplayed()
        {
            Test.Pages.PreviewOrderSummary.OneOffCostsTableIsDisplayed().Should().BeTrue();
        }

        [Then(@"Order items \(recurring cost\) table is displayed")]
        public void ThenOrderItemsRecurringCostTableIsDisplayed()
        {
            Test.Pages.PreviewOrderSummary.RecurringCostsTableIsDisplayed().Should().BeTrue();
        }

        [Then(@"the total one-off cost is displayed")]
        public void ThenTheTotalOne_OffCostIsDisplayed()
        {
            var value = Test.Pages.PreviewOrderSummary.GetTotalOneOffCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the total monthly cost is displayed")]
        public void ThenTheTotalMonthlyCostIsDisplayed()
        {
            var value = Test.Pages.PreviewOrderSummary.GetTotalMonthlyCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the total annual cost is displayed")]
        public void ThenTheTotalAnnualCostIsDisplayed()
        {
            var value = Test.Pages.PreviewOrderSummary.GetTotalAnnualCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"the Total cost of contract is displayed")]
        [Then(@"the total cost of ownership is displayed")]
        public void ThenTheTotalCostOfOwnershipIsDisplayed()
        {
            var value = Test.Pages.PreviewOrderSummary.GetTotalOwnershipCost();
            value.Should().NotBeNullOrEmpty();
        }

        [Then(@"it displays the Call-off Agreement ID data")]
        public void ThenItDisplaysTheCall_OffAgreementIDData()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var id = Test.Pages.OrderForm.GetCallOffId();
            id.Should().EndWith(order.CallOffId.ToString());
        }

        [Then(@"the Order description data saved in the order")]
        public void ThenTheOrderDescriptionDataSavedInTheOrder()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var actualValue = Test.Pages.OrderForm.GetOrderDescription();
            actualValue.Should().BeEquivalentTo(order.Description);
        }

        [Then(@"the Order items \(recurring cost\) table is populated")]
        public void ThenTheOrderItemsRecurringCostTableIsPopulated()
        {
            Test.Pages.PreviewOrderSummary.RecurringCostsTableIsPopulated().Should().BeTrue();
        }

        [Then(@"the Service Instance ID of each saved Catalogue Solution item is displayed")]
        public void ThenTheServiceInstanceIDOfEachSavedCatalogueSolutionItemIsDisplayed()
        {
            Test.Pages.PreviewOrderSummary.ServiceInstanceIdColumn().Should().BeTrue();
        }

        [Then(@"the Order items \(one off\) table is populated")]
        public void ThenTheOrderItemsOneOffTableIsPopulated()
        {
            Test.Pages.PreviewOrderSummary.OneOffCostsTableIsPopulated().Should().BeTrue();
        }

        [Then(@"the Recipient name \(ODS code\) of each item is the concatenation ""\[Service Recipient name\] \[\(ODS code\)\]""")]
        public void ThenTheRecipientNameODSCodeOfEachItemIsTheConcatenation()
        {
            var name = Test.Pages.PreviewOrderSummary.GetItemRecipientName();
            var expectedServiceRecipient = new ServiceRecipient(string.Empty, string.Empty); // placeholder until I can access the preview page
            var expectedValue = string.Format("{0} ({1})", expectedServiceRecipient.Name, expectedServiceRecipient.OdsCode);
            name.Should().Be(expectedValue);
        }

        [Then(@"the item ID of each item is displayed")]
        public void ThenTheItemIDOfEachItemIsDisplayed()
        {
            var id = Test.Pages.PreviewOrderSummary.GetItemId();
            id.Should().NotBeNullOrEmpty();
        }

        [Then(@"the item name of each item is the Additional Service name")]
        [Then(@"the item name of each item is the Associated Service name")]
        [Then(@"the item name of each item is the Catalogue Solution name")]
        public async Task ThenTheItemNameOfEachItemIsTheCatalogueSolutionNameAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var name = Test.Pages.PreviewOrderSummary.GetItemName();
            name.Should().Be(expectedOrderItem.CatalogueItem.Name);
        }

        [Then(@"the order items recurring cost table is sorted by service recipient name")]
        public void ThenTheOrderItemsRecurringCostTableIsSortedByServiceRecipientName()
        {
            CommonSteps.AssertListOfStringsIsInAscendingOrder(Test.Pages.PreviewOrderSummary.GetItemRecipientNames());
        }

        [Then(@"the order items one-off cost table is sorted by item name")]
        [Then(@"the order items recurring cost table is second sorted by item name")]
        public void ThenTheOrderItemsRecurringCostTableIsSecondSortedByItemName()
        {
            CommonSteps.AssertListOfStringsIsInAscendingOrder(Test.Pages.PreviewOrderSummary.GetItemNames());
        }

        [Then(@"the Price unit of order of each item is the concatenation ""\[Price\] \[unit\]""")]
        public async Task ThenThePriceUnitOfOrderOfEachItemIsTheConcatenationAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var expectedValue = $"{FormatDecimal(expectedOrderItem.Price.Value)} {expectedOrderItem.PricingUnit.Description}".Trim();

            if (expectedOrderItem.ProvisioningType != ProvisioningType.OnDemand)
            {
                expectedValue += $" {expectedOrderItem.PriceTimeUnit.Value.Description()}";
            }

            var price = Test.Pages.PreviewOrderSummary.GetItemPrice();
            price.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per month")]
        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per year")]
        public async Task ThenTheQuantityOfEachItemIsTheConcatenationI_E_QuantityPerPeriodAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var expectedPeriod = expectedOrderItem.EstimationPeriod;
            var expectedValue = $"{FormatInt(expectedOrderItem.OrderItemRecipients[0].Quantity)} {expectedPeriod.Value.Description()}";

            var quantity = Test.Pages.PreviewOrderSummary.GetItemQuantity();
            quantity.Should().ContainEquivalentOf(expectedValue);
        }

        [Then(@"the Quantity of each item is \[Quantity\]")]
        public async Task ThenTheQuantityOfEachItemIsQuantityAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var expectedValue = $"{expectedOrderItem.OrderItemRecipients[0].Quantity}";

            var quantity = Test.Pages.PreviewOrderSummary.GetItemQuantity();
            quantity.Should().ContainEquivalentOf(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation \[Quantity\] (.*)")]
        public async Task ThenTheQuantityOfEachItemIsTheConcatenationOfQuantityAndPerPeriodAsync(string period)
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var expectedQuantityValue = $"{expectedOrderItem.OrderItemRecipients[0].Quantity} {period}";

            var actualQuantity = Test.Pages.PreviewOrderSummary.GetItemQuantity();
            actualQuantity.Should().ContainEquivalentOf(expectedQuantityValue);
        }

        [Then(@"the Planned delivery date of each item is displayed")]
        public async Task ThenThePlannedDeliveryDateOfEachItemIsDisplayedAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var date = Test.Pages.PreviewOrderSummary.GetItemPlannedDate();
            date.Should().Be(expectedOrderItem.DefaultDeliveryDate.Value.ToString("d MMMM yyyy"));
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] rounded up to two decimal places")]
        public async Task ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityRoundedUpToTwoDecimalPlacesAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var expectedValue = FormatDecimal(Math.Round(expectedOrderItem.CalculateTotalCostPerYear(), 2));
            var cost = Test.Pages.PreviewOrderSummary.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] \* 12 rounded up to two decimal places")]
        public async Task ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityTimes12RoundedUpToTwoDecimalPlacesAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var expectedOrderItem = order.OrderItems[0];
            var expectedCost = expectedOrderItem.CalculateTotalCostPerYear();
            var expectedValue = FormatDecimal(Math.Round(expectedCost, 2));
            var cost = Test.Pages.PreviewOrderSummary.GetItemCostPerYear();
            cost.Should().Be(expectedValue.ToString());
        }

        [Then(@"the Total cost of contract is the result of the Total cost of contract calculation Total one-off cost \+ \(3 \* Total cost for one year calculation\)")]
        public async Task ThenTheTotalCostOfContractIsTheResultOfTheTotalCostOfContractCalculationTotalOne_OffCostTotalCostForOneYearCalculationAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var actualTotalCostOfContract = Test.Pages.PreviewOrderSummary.GetTotalOwnershipCost();

            actualTotalCostOfContract.Should().Be(FormatDecimal(order.CalculateTotalOwnershipCost()));
        }

        [Then(@"the Total one-off cost is the result of the Total one-off cost calculation")]
        public async Task ThenTheTotalOne_OffCostIsTheResultOfTheTotalOne_OffCostCalculationAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var actual = Test.Pages.PreviewOrderSummary.GetTotalOneOffCost();

            actual.Should().Be(FormatDecimal(order.CalculateCostPerYear(CostType.OneOff)));
        }

        [Then(@"the Total cost for one year is the result of the Total cost for one year calculation")]
        public async Task ThenTheTotalCostForOneYearIsTheResultOfTheTotalCostForOneYearCalculationAsync()
        {
            var actual = Test.Pages.PreviewOrderSummary.GetTotalAnnualCost();
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);

            actual.Should().Be(FormatDecimal(order.CalculateCostPerYear(CostType.Recurring) + order.CalculateCostPerYear(CostType.OneOff)));
        }

        [Then(@"the Total cost for one year is expressed as two decimal places")]
        public void ThenItIsExpressedAsTwoDecimalPlaces()
        {
            ValueExpressedAsTwoDecimalPlaces(Test.Pages.PreviewOrderSummary.GetTotalAnnualCost());
        }

        [Then(@"the Total cost of contract is expressed as two decimal places")]
        public void ThenTheTotalCostOfContractIsExpressedAsTwoDecimalPlaces()
        {
            ValueExpressedAsTwoDecimalPlaces(Test.Pages.PreviewOrderSummary.GetTotalOwnershipCost());
        }

        [Then(@"the Total monthly cost is the result of the Total monthly cost calculation")]
        public async Task ThenTheTotalMonthlyCostIsTheResultOfTheTotalMonthlyCostCalculationAsync()
        {
            var actual = Test.Pages.PreviewOrderSummary.GetTotalMonthlyCost();
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);

            actual.Should().Be(FormatDecimal(order.CalculateCostPerYear(CostType.Recurring) / 12));
        }

        [Then(@"the Total monthly cost is expressed as two decimal places")]
        public void ThenTheTotalMonthlyCostIsExpressedAsTwoDecimalPlaces()
        {
            ValueExpressedAsTwoDecimalPlaces(Test.Pages.PreviewOrderSummary.GetTotalMonthlyCost());
        }

        [Given(@"multiple order items with the same service recipient have been added to the order")]
        public async Task GivenMultipleOrderItemsWithTheSameServiceRecipientHaveBeenAddedToTheOrder()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];

            Random rng = new();

            var numAddedItems = rng.Next(5);

            for (int i = 0; i < numAddedItems; i++)
            {
                CatalogueItemType type;

                if (i % 2 == 0)
                {
                    type = CatalogueItemType.Solution;
                }
                else
                {
                    type = CatalogueItemType.AdditionalService;
                }

                await OrderItemHelper.CreateOrderItem(
                order,
                type,
                CataloguePriceType.Flat,
                ProvisioningType.OnDemand,
                DbContext,
                Test.BapiConnectionString);
            }
        }

        [Given(@"multiple order items with different service recipient have been added to the order")]
        public async Task GivenMultipleOrderItemsWithDifferentServiceRecipientHaveBeenAddedToTheOrderAsync()
        {
            var commonSteps = new CommonSteps(Test, Context);

            await commonSteps.GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder(5);
        }

        [Given(@"multiple one-off order items have been added to the order")]
        public async Task GivenMultipleOne_OffOrderItemsHaveBeenAddedToTheOrderAsync()
        {
            var commonSteps = new CommonSteps(Test, Context);

            await commonSteps.GivenAnAssociatedServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder();
        }

        private static string FormatDecimal(decimal price)
        {
            return price.ToString("#,0.00", new NumberFormatInfo { NumberGroupSeparator = "," });
        }

        private static string FormatInt(int quantity)
        {
            return quantity.ToString("#,0", new NumberFormatInfo { NumberGroupSeparator = "," });
        }

        private static void ValueExpressedAsTwoDecimalPlaces(string value)
        {
            const int decimalPointPartIndex = 1;
            var actual = value.Split('.')[decimalPointPartIndex].Length;

            const int expectedDecimalPointLength = 2;
            actual.Should().Be(expectedDecimalPointLength);
        }
    }
}
