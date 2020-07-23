using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class PreviewOrderSummary : TestBase
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
            value.Should().NotBeNullOrEmpty();
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
            Test.Pages.OrderForm.OneOffCostsTableIsDisplayed().Should().BeTrue();
        }

        [Then(@"Order items \(recurring cost\) table is displayed")]
        public void ThenOrderItemsRecurringCostTableIsDisplayed()
        {
            Test.Pages.OrderForm.RecurringCostsTableIsDisplayed().Should().BeTrue();
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
            var expectedId = $"{expectedOrderItem.OrderId}-{expectedOrderItem.OdsCode}-{expectedOrderItem.OrderItemId}";
            var id = Test.Pages.OrderForm.GetItemId();
            id.Should().Be(expectedId);
        }

        [Then(@"the item name of each item is the Additional Service name")]
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
            var timeDescription = expectedOrderItem.GetTimeUnitPeriod(Test.ConnectionString);
            var expectedValue = $"{FormatDecimal(expectedOrderItem.Price)} {expectedOrderItem.PricingUnitDescription} {timeDescription}".Trim();

            var price = Test.Pages.OrderForm.GetItemPrice();
            price.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per month")]
        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per year")]
        public void ThenTheQuantityOfEachItemIsTheConcatenationI_E_QuantityPerPeriod()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedPeriod = expectedOrderItem.GetEstimationPeriod(Test.ConnectionString);
            var expectedValue = $"{FormatInt(expectedOrderItem.Quantity)} {expectedPeriod}";

            var quantity = Test.Pages.OrderForm.GetItemQuantity();
            quantity.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation \[Quantity\] (.*)")]
        public void ThenTheQuantityOfEachItemIsTheConcatenationOfQuantityAndPerPeriod(string period)
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedQuantityValue = $"{FormatInt(expectedOrderItem.Quantity)} {period}";

            var actualQuantity = Test.Pages.OrderForm.GetItemQuantity();
            actualQuantity.Should().Be(expectedQuantityValue);
        }

        [Then(@"the Planned delivery date of each item is displayed")]
        public void ThenThePlannedDeliveryDateOfEachItemIsDisplayed()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var date = Test.Pages.OrderForm.GetItemPlannedDate();
            date.Should().Be(expectedOrderItem.DeliveryDate.Value.ToString("d MMMM yyyy"));
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] rounded up to two decimal places")]
        public void ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityRoundedUpToTwoDecimalPlaces()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedCost = expectedOrderItem.Price * expectedOrderItem.Quantity;
            var expectedValue = FormatDecimal(Math.Round(expectedCost, 2));
            var cost = Test.Pages.OrderForm.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] \* 12 rounded up to two decimal places")]
        public void ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityTimes12RoundedUpToTwoDecimalPlaces()
        {
            var expectedOrderItem = (OrderItem)Context["CreatedOrderItem"];
            var expectedCost = expectedOrderItem.Price * expectedOrderItem.Quantity * 12;
            var expectedValue = FormatDecimal(Math.Round(expectedCost, 2));
            var cost = Test.Pages.OrderForm.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Given(@"a Catalogue Solution is added to the order")]
        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per year is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context["CreatedOrder"]);
            orderItem.EstimationPeriodId = 2;
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per month is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context["CreatedOrder"]);
            orderItem.EstimationPeriodId = 1;
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(Per-Patient\) order type is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariablePer_PatientOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariablePerPatient((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(Declarative\) order type is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new CatalogueSolutions(Test, Context).GivenTheSupplierAddedToTheOrderHasASolutionWithADeclarativeFlatPrice();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableDeclarative((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"an additional service with a flat price variable \(Declarative\) order type is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AdditionalServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithADeclarativeFlatPrice();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithDeclarative((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"an additional service with a flat price variable \(On-Demand\) order type with the quantity period per year is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceWithFlatPricedVariableOnDemand((Order)Context["CreatedOrder"]);
            orderItem.EstimationPeriodId = 2;
            orderItem.TimeUnitId = 2;
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"an additional service with a flat price variable \(On-Demand\) order type with the quantity period per month is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceWithFlatPricedVariableOnDemand((Order)Context["CreatedOrder"]);
            orderItem.EstimationPeriodId = 1;
            orderItem.TimeUnitId = 1;
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"an additional service with a flat price variable \(Patient\) order type is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariablePatientOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AdditionalServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithAPatientFlatPrice();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithVariablePricedPerPatient((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
        }

        [Given(@"there are one or more Order items summarised in the Order items \(recurring cost\) table")]
        public void GivenThereAreOneOrMoreOrderItemsSummarisedInTheOrderItemsRecurringCostTable()
        {
            var onDemandOrderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context["CreatedOrder"]);
            onDemandOrderItem.Create(Test.ConnectionString);

            var declarativeOrderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableDeclarative((Order)Context["CreatedOrder"]);
            declarativeOrderItem.Create(Test.ConnectionString);

            var patientOrderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariablePerPatient((Order)Context["CreatedOrder"]);
            patientOrderItem.Create(Test.ConnectionString);
        }

        [Then(@"the Total cost for one year is the result of the Total cost for one year calculation")]
        public void ThenTheTotalCostForOneYearIsTheResultOfTheTotalCostForOneYearCalculation()
        {
            var actual = Test.Pages.OrderForm.GetTotalAnnualCost();

            const string expectedTotalAnnualCost = "1,316,535.00";
            actual.Should().Be(expectedTotalAnnualCost);
        }

        [Then(@"the Total cost for one year is expressed as two decimal places")]
        public void ThenItIsExpressedAsTwoDecimalPlaces()
        {
            const int decimalPointPartIndex = 1;
            const int expectedDecimalPointLength = 2;

            var actual = Test.Pages.OrderForm.GetTotalAnnualCost().Split('.')[decimalPointPartIndex].Length;

            actual.Should().Be(expectedDecimalPointLength);
        }

        [Then(@"the Total monthly cost is the result of the Total monthly cost calculation")]
        public void ThenTheTotalMonthlyCostIsTheResultOfTheTotalMonthlyCostCalculation()
        {
            var actual = Test.Pages.OrderForm.GetTotalMonthlyCost();

            const string expectedTotalAnnualCost = "109,711.25";
            actual.Should().Be(expectedTotalAnnualCost);
        }

        [Then(@"the Total monthly cost is expressed as two decimal places")]
        public void ThenTheTotalMonthlyCostIsExpressedAsTwoDecimalPlaces()
        {
            const int decimalPointPartIndex = 1;
            const int expectedDecimalPointLength = 2;

            var actual = Test.Pages.OrderForm.GetTotalMonthlyCost().Split('.')[decimalPointPartIndex].Length;

            actual.Should().Be(expectedDecimalPointLength);
        }

        private static string FormatDecimal(decimal price)
        {
            return price.ToString("#,0.00", new NumberFormatInfo { NumberGroupSeparator = "," });
        }

        private static string FormatInt(int quantity)
        {
            return quantity.ToString("#,0", new NumberFormatInfo { NumberGroupSeparator = "," });
        }

        private void SetOrderCatalogueSectionToComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.CatalogueSolutionsViewed = 1;
            order.Update(Test.ConnectionString);
        }
    }
}
