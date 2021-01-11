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
            var date = Test.Pages.PreviewOrderSummary.GetDateOrderSummaryCreatedValue();
            date.Should().NotBeNullOrEmpty();
            var expectedDate = DateTime.Now.ToString("d MMMM yyyy");
            date.Should().EndWithEquivalent(expectedDate);
        }

        [Then(@"the Call Off Ordering Party information is displayed")]
        [Then(@"the Call-off Ordering Party data saved in the order")]
        [Then(@"the Call-off Ordering Party names are concatenated")]
        public void ThenTheCallOffOrderingPartyInformationIsDisplayed()
        {
            var value = Test.Pages.PreviewOrderSummary.GetCallOffOrderingPartyPreviewValue();
            value.Should().NotBeNullOrEmpty();
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var createdAddress = (Address)Context[ContextKeys.CreatedAddress];
            var createdContact = (Contact)Context[ContextKeys.CreatedContact];
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
            var value = Test.Pages.PreviewOrderSummary.GetSupplierPreviewValue();
            value.Should().NotBeNullOrEmpty();
            var createdAddress = (Address)Context[ContextKeys.CreatedSupplierAddress];
            var createdContact = (Contact)Context[ContextKeys.CreatedSupplierContact];
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
            var date = Test.Pages.PreviewOrderSummary.GetCommencementDateValue();
            date.Should().NotBeNullOrEmpty();
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var expectedDate = ((DateTime)order.CommencementDate).ToString("d MMMM yyyy");
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
            id.Should().EndWith(order.OrderId);
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
            var expectedServiceRecipient = (ServiceRecipient)Context[ContextKeys.CreatedServiceRecipient];
            var expectedValue = string.Format("{0} ({1})", expectedServiceRecipient.Name, expectedServiceRecipient.OdsCode);
            name.Should().Be(expectedValue);
        }

        [Then(@"the item ID of each item is displayed")]
        public void ThenTheItemIDOfEachItemIsDisplayed()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var expectedId = $"{expectedOrderItem.OrderId}-{expectedOrderItem.OdsCode}-{expectedOrderItem.OrderItemId}";
            var id = Test.Pages.PreviewOrderSummary.GetItemId();
            id.Should().Be(expectedId);
        }

        [Then(@"the item name of each item is the Additional Service name")]
        [Then(@"the item name of each item is the Associated Service name")]
        [Then(@"the item name of each item is the Catalogue Solution name")]
        public void ThenTheItemNameOfEachItemIsTheCatalogueSolutionName()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var name = Test.Pages.PreviewOrderSummary.GetItemName();
            name.Should().Be(expectedOrderItem.CatalogueItemName);
        }

        [Then(@"the order items recurring cost table is sorted by service recipient name")]
        public void ThenTheOrderItemsRecurringCostTableIsSortedByServiceRecipientName()
        {
            new CommonSteps(Test, Context).AssertListOfStringsIsInAscendingOrder(Test.Pages.PreviewOrderSummary.GetItemRecipientNames());
        }

        [Then(@"the order items one-off cost table is sorted by item name")]
        [Then(@"the order items recurring cost table is second sorted by item name")]
        public void ThenTheOrderItemsRecurringCostTableIsSecondSortedByItemName()
        {
            new CommonSteps(Test, Context).AssertListOfStringsIsInAscendingOrder(Test.Pages.PreviewOrderSummary.GetItemNames());
        }

        [Then(@"the Price unit of order of each item is the concatenation ""\[Price\] \[unit\]""")]
        public void ThenThePriceUnitOfOrderOfEachItemIsTheConcatenation()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var timeDescription = expectedOrderItem.GetTimeUnitPeriod(Test.OrdapiConnectionString);
            var expectedValue = $"{FormatDecimal(expectedOrderItem.Price)} {expectedOrderItem.PricingUnitDescription} {timeDescription}".Trim();

            var price = Test.Pages.PreviewOrderSummary.GetItemPrice();
            price.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per month")]
        [Then(@"the Quantity of each item is the concatenation ""\[Quantity\] \[Estimation period\]"" i\.e\. \[Quantity] per year")]
        public void ThenTheQuantityOfEachItemIsTheConcatenationI_E_QuantityPerPeriod()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var expectedPeriod = expectedOrderItem.GetEstimationPeriod(Test.OrdapiConnectionString);
            var expectedValue = $"{FormatInt(expectedOrderItem.Quantity)} {expectedPeriod}";

            var quantity = Test.Pages.PreviewOrderSummary.GetItemQuantity();
            quantity.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is \[Quantity\]")]
        public void ThenTheQuantityOfEachItemIsQuantity()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var expectedValue = $"{expectedOrderItem.Quantity}";

            var quantity = Test.Pages.PreviewOrderSummary.GetItemQuantity();
            quantity.Should().Be(expectedValue);
        }

        [Then(@"the Quantity of each item is the concatenation \[Quantity\] (.*)")]
        public void ThenTheQuantityOfEachItemIsTheConcatenationOfQuantityAndPerPeriod(string period)
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var expectedQuantityValue = $"{FormatInt(expectedOrderItem.Quantity)} {period}";

            var actualQuantity = Test.Pages.PreviewOrderSummary.GetItemQuantity();
            actualQuantity.Should().Be(expectedQuantityValue);
        }

        [Then(@"the Planned delivery date of each item is displayed")]
        public void ThenThePlannedDeliveryDateOfEachItemIsDisplayed()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var date = Test.Pages.PreviewOrderSummary.GetItemPlannedDate();
            date.Should().Be(expectedOrderItem.DeliveryDate.Value.ToString("d MMMM yyyy"));
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] rounded up to two decimal places")]
        public void ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityRoundedUpToTwoDecimalPlaces()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var expectedCost = expectedOrderItem.Price * expectedOrderItem.Quantity;
            var expectedValue = FormatDecimal(Math.Round(expectedCost, 2));
            var cost = Test.Pages.PreviewOrderSummary.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Then(@"the item year cost of each item is the result of the Flat calculation \[Price] \* \[Quantity] \* 12 rounded up to two decimal places")]
        public void ThenTheItemYearCostOfEachItemIsTheResultOfTheFlatCalculationPriceQuantityTimes12RoundedUpToTwoDecimalPlaces()
        {
            var expectedOrderItem = (OrderItem)Context[ContextKeys.CreatedOrderItem];
            var expectedCost = expectedOrderItem.Price * expectedOrderItem.Quantity * 12;
            var expectedValue = FormatDecimal(Math.Round(expectedCost, 2));
            var cost = Test.Pages.PreviewOrderSummary.GetItemCost();
            cost.Should().Be(expectedValue.ToString());
        }

        [Then(@"the Total cost of contract is the result of the Total cost of contract calculation Total one-off cost \+ \(3 \* Total cost for one year calculation\)")]
        public void ThenTheTotalCostOfContractIsTheResultOfTheTotalCostOfContractCalculationTotalOne_OffCostTotalCostForOneYearCalculation()
        {
            var expectedTotalCost = Context.Get<OrderItemList>(ContextKeys.CreatedOneOffOrderItems).GetTotalOneOffCost();
            var expectedTotalAnnualCost = Context.Get<OrderItemList>(ContextKeys.CreatedRecurringOrderItems).GetTotalAnnualCost();
            var expectedTotalCostOfContract = expectedTotalCost + (3 * expectedTotalAnnualCost);
            var actualTotalCostOfContract = Test.Pages.PreviewOrderSummary.GetTotalOwnershipCost();

            actualTotalCostOfContract.Should().Be(FormatDecimal(expectedTotalCostOfContract));
        }

        [Given(@"a Catalogue Solution is added to the order")]
        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per year is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.EstimationPeriodId = TimeUnit.Year;
            orderItem.Create(Test.OrdapiConnectionString);

            if (!Context.ContainsKey(ContextKeys.CreatedOrderItem))
                Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per month is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.EstimationPeriodId = TimeUnit.Month;
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(Per-Patient\) order type is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariablePer_PatientOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariablePerPatient((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"a catalogue solution with a flat price variable \(Declarative\) order type is saved to the order")]
        public void GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new CatalogueSolutions(Test, Context).GivenTheSupplierAddedToTheOrderHasASolutionWithADeclarativeFlatPrice();
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an additional service with a flat price variable \(Declarative\) order type is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AdditionalServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithADeclarativeFlatPrice();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an additional service with a flat price variable \(On-Demand\) order type with the quantity period per year is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.EstimationPeriodId = TimeUnit.Year;
            orderItem.TimeUnitId = 2;
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an additional service with a flat price variable \(On-Demand\) order type with the quantity period per month is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.EstimationPeriodId = TimeUnit.Month;
            orderItem.TimeUnitId = 1;
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an additional service with a flat price variable \(Patient\) order type is saved to the order")]
        public void GivenAnAdditionalServiceWithAFlatPriceVariablePatientOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AdditionalServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithAPatientFlatPrice();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithVariablePricedPerPatient((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an associated service with a flat price variable \(Declarative\) order type is saved to the order")]
        public void GivenAnAssociatedServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AssociatedServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceDeclarative();
            var orderItem = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an associated service with a flat price variable \(On-Demand\) order type with the quantity period per year is saved to the order")]
        public void GivenAnAssociatedServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AssociatedServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceOnDemand();
            var orderItem = new OrderItem().GenerateAssociatedServiceWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.EstimationPeriodId = TimeUnit.Year;
            orderItem.TimeUnitId = 2;
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"an associated service with a flat price variable \(On-Demand\) order type with the quantity period per month is saved to the order")]
        public void GivenAnAssociatedServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            new AssociatedServices(Test, Context).GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceOnDemand();
            var orderItem = new OrderItem().GenerateAssociatedServiceWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem.EstimationPeriodId = TimeUnit.Month;
            orderItem.TimeUnitId = 1;
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"there are one or more Associated Service items summarised in the Order items \(one-off cost\) table")]
        public void GivenThereAreOneOrMoreAssociatedServiceItemsSummarisedInTheOrderItemsOne_OffCostTable()
        {
            SetOrderCatalogueSectionToComplete();
            var AssociatedServicesSteps = new AssociatedServices(Test, Context);
            AssociatedServicesSteps.SetOrderAssociatedServicesSectionToComplete();
            AssociatedServicesSteps.GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceDeclarative();
            var declarativeOrderItem1 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            var declarativeOrderItem2 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            declarativeOrderItem1.Create(Test.OrdapiConnectionString);
            declarativeOrderItem2.Create(Test.OrdapiConnectionString);

            var createdOrderItems = new OrderItemList(declarativeOrderItem1, declarativeOrderItem2);            
            AddOrderItemsToContext(createdOrderItems);
            Context.Add(ContextKeys.CreatedOneOffOrderItems, createdOrderItems);
        }

        [Given(@"there are one or more Order items summarised in the Order items \(recurring cost\) table")]
        public void GivenThereAreOneOrMoreOrderItemsSummarisedInTheOrderItemsRecurringCostTable()
        {
            var onDemandOrderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            onDemandOrderItem.Create(Test.OrdapiConnectionString);

            var declarativeOrderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            declarativeOrderItem.Create(Test.OrdapiConnectionString);

            var patientOrderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariablePerPatient((Order)Context[ContextKeys.CreatedOrder]);
            patientOrderItem.Create(Test.OrdapiConnectionString);

            var createdOrderItems = new OrderItemList(onDemandOrderItem, declarativeOrderItem, patientOrderItem);

            AddOrderItemsToContext(createdOrderItems);
            Context.Add(ContextKeys.CreatedRecurringOrderItems, createdOrderItems);
        }

        [Then(@"the Total one-off cost is the result of the Total one-off cost calculation")]
        public void ThenTheTotalOne_OffCostIsTheResultOfTheTotalOne_OffCostCalculation()
        {
            var actual = Test.Pages.PreviewOrderSummary.GetTotalOneOffCost();
            var expectedTotalCost = Context.Get<OrderItemList>(ContextKeys.CreatedOrderItems).GetTotalOneOffCost();

            actual.Should().Be(FormatDecimal(expectedTotalCost));
        }

        [Then(@"the Total cost for one year is the result of the Total cost for one year calculation")]
        public void ThenTheTotalCostForOneYearIsTheResultOfTheTotalCostForOneYearCalculation()
        {
            var actual = Test.Pages.PreviewOrderSummary.GetTotalAnnualCost();
            var expectedTotalAnnualCost = Context.Get<OrderItemList>(ContextKeys.CreatedOrderItems).GetTotalAnnualCost();

            actual.Should().Be(FormatDecimal(expectedTotalAnnualCost));
        }

        [Then(@"the Total one-off cost is expressed as (.*) decimal places")]
        public void ThenTheTotalOne_OffCostIsExpressedAsDecimalPlaces(int p0)
        {
            ValueExpressedAsTwoDecimalPlaces(Test.Pages.PreviewOrderSummary.GetTotalOneOffCost());
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
        public void ThenTheTotalMonthlyCostIsTheResultOfTheTotalMonthlyCostCalculation()
        {
            var actual = Test.Pages.PreviewOrderSummary.GetTotalMonthlyCost();
            var expectedTotalMonthlyCost = Context.Get<OrderItemList>(ContextKeys.CreatedOrderItems).GetTotalMonthlyCost();

            actual.Should().Be(FormatDecimal(expectedTotalMonthlyCost));
        }

        [Then(@"the Total monthly cost is expressed as two decimal places")]
        public void ThenTheTotalMonthlyCostIsExpressedAsTwoDecimalPlaces()
        {
            ValueExpressedAsTwoDecimalPlaces(Test.Pages.PreviewOrderSummary.GetTotalMonthlyCost());
        }

        [Given(@"multiple order items with different service recipient have been added to the order")]
        public void GivenMultipleOrderItemsWithDifferentServiceRecipientHaveBeenAddedToTheOrder()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, "yolo", "some name");
            serviceRecipient.Create(Test.OrdapiConnectionString);

            GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder();

            order.OrganisationOdsCode = "yolo";

            GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder();
        }

        [Given(@"multiple order items with the same service recipient have been added to the order")]
        public void GivenMultipleOrderItemsWithTheSameServiceRecipientHaveBeenAddedToTheOrder()
        {
            GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder();
            var orderItem2 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem2.EstimationPeriodId = TimeUnit.Year;
            orderItem2.CatalogueItemName = "AAA item";
            orderItem2.Create(Test.OrdapiConnectionString);
            var orderItem3 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem3.EstimationPeriodId = TimeUnit.Year;
            orderItem3.CatalogueItemName = "5 power up";
            orderItem3.Create(Test.OrdapiConnectionString);
            var orderItem4 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem4.EstimationPeriodId = TimeUnit.Year;
            orderItem4.CatalogueItemName = "™ Tee EMM";
            orderItem4.Create(Test.OrdapiConnectionString);
            var orderItem5 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem5.EstimationPeriodId = TimeUnit.Year;
            orderItem5.CatalogueItemName = "$$ bills yall";
            orderItem5.Create(Test.OrdapiConnectionString);
            var orderItem6 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem6.EstimationPeriodId = TimeUnit.Year;
            orderItem6.CatalogueItemName = "我能";
            orderItem6.Create(Test.OrdapiConnectionString);
            var orderItem7 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem7.EstimationPeriodId = TimeUnit.Year;
            orderItem7.CatalogueItemName = "® registered trademark";
            orderItem7.Create(Test.OrdapiConnectionString);

            var orderItem8 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem8.EstimationPeriodId = TimeUnit.Year;
            orderItem8.CatalogueItemName = ".. dot dot";
            orderItem8.Create(Test.OrdapiConnectionString);

            var orderItem9 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem9.EstimationPeriodId = TimeUnit.Year;
            orderItem9.CatalogueItemName = "(( lb";
            orderItem9.Create(Test.OrdapiConnectionString);

            var orderItem10 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem10.EstimationPeriodId = TimeUnit.Year;
            orderItem10.CatalogueItemName = "))) rb cubed";
            orderItem10.Create(Test.OrdapiConnectionString);

            var orderItem11 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem11.EstimationPeriodId = TimeUnit.Year;
            orderItem11.CatalogueItemName = "+ plus health";
            orderItem11.Create(Test.OrdapiConnectionString);

            var orderItem12 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem12.EstimationPeriodId = TimeUnit.Year;
            orderItem12.CatalogueItemName = "& ampersand";
            orderItem12.Create(Test.OrdapiConnectionString);

            var orderItem13 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem13.EstimationPeriodId = TimeUnit.Year;
            orderItem13.CatalogueItemName = "' single quote";
            orderItem13.Create(Test.OrdapiConnectionString);

            var orderItem14 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem14.EstimationPeriodId = TimeUnit.Year;
            orderItem14.CatalogueItemName = "- well ill be dashed";
            orderItem14.Create(Test.OrdapiConnectionString);

            var orderItem15 = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand((Order)Context[ContextKeys.CreatedOrder]);
            orderItem15.EstimationPeriodId = TimeUnit.Year;
            orderItem15.CatalogueItemName = "@ your side";
            orderItem15.Create(Test.OrdapiConnectionString);
        }

        [Given(@"multiple one-off order items have been added to the order")]
        public void GivenMultipleOne_OffOrderItemsHaveBeenAddedToTheOrder()
        {
            SetOrderCatalogueSectionToComplete();
            var AssociatedServicesSteps = new AssociatedServices(Test, Context);
            AssociatedServicesSteps.SetOrderAssociatedServicesSectionToComplete();
            AssociatedServicesSteps.GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceDeclarative();
            var declarativeOrderItem1 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            var declarativeOrderItem2 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            var declarativeOrderItem3 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            var declarativeOrderItem4 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);
            var declarativeOrderItem5 = new OrderItem().GenerateAssociatedServiceWithFlatPricedDeclarative((Order)Context[ContextKeys.CreatedOrder]);

            declarativeOrderItem1.CatalogueItemName = "® registered trademark";
            declarativeOrderItem2.CatalogueItemName = "BB Band Aid";
            declarativeOrderItem3.CatalogueItemName = "AA Battery powered defibrillators";
            declarativeOrderItem4.CatalogueItemName = "999 Solutions";
            declarativeOrderItem5.CatalogueItemName = "+ plus health";

            declarativeOrderItem1.Create(Test.OrdapiConnectionString);
            declarativeOrderItem2.Create(Test.OrdapiConnectionString);
            declarativeOrderItem3.Create(Test.OrdapiConnectionString);
            declarativeOrderItem4.Create(Test.OrdapiConnectionString);
            declarativeOrderItem5.Create(Test.OrdapiConnectionString);

            var createdOrderItems = new OrderItemList(declarativeOrderItem1, declarativeOrderItem2, declarativeOrderItem3, declarativeOrderItem4, declarativeOrderItem5);
            AddOrderItemsToContext(createdOrderItems);
            Context.Add(ContextKeys.CreatedOneOffOrderItems, createdOrderItems);
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
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.CatalogueSolutionsViewed = 1;
            order.Update(Test.OrdapiConnectionString);
        }

        private void ValueExpressedAsTwoDecimalPlaces(string value)
        {
            const int decimalPointPartIndex = 1;
            var actual = value.Split('.')[decimalPointPartIndex].Length;

            const int expectedDecimalPointLength = 2;
            actual.Should().Be(expectedDecimalPointLength);
        }

        private void AddOrderItemsToContext(OrderItemList inputList)
        {
            if (Context.ContainsKey(ContextKeys.CreatedOrderItems))
            {
                Context.Remove(ContextKeys.CreatedOrderItems);
            }
            Context.Add(ContextKeys.CreatedOrderItems, inputList);
        }
    }
}
