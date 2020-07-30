using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class AdditionalServices : TestBase
    {
        public AdditionalServices(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Given(@"there are no Additional Services in the order")]
        public void GivenThereAreNoAdditionalServicesInTheOrder()
        {
            Context.Should().NotContainKey("CreatedAdditionalServiceOrderItem");
            var order = (Order)Context["CreatedOrder"];
            var searchedOrderItem = new OrderItem().RetrieveByOrderId(Test.ConnectionString, order.OrderId, 2);
            searchedOrderItem.Should().BeEmpty();
        }

        [Given(@"an Additional Service is added to the order")]
        public void GivenAnAdditionalServiceIsAddedToTheOrder()
        {
            new OrderForm(Test, Context).GivenTheAdditionalServicesSectionIsComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithFlatPricedPerPatient((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedAdditionalServiceOrderItem", orderItem);
        }

        [StepDefinition(@"the User is able to manage the Additional Services section")]
        [When(@"the User has chosen to manage the Additional Service section")]
        public void ThenTheUserIsAbleToManageTheAdditionalServicesSection()
        {
            Test.Pages.OrderForm.ClickEditAdditionalServices();
            ThenTheAdditionalServiceDashboardIsPresented();
        }

        [StepDefinition(@"the Additional Service dashboard is presented")]
        public void ThenTheAdditionalServiceDashboardIsPresented()
        {
            Test.Pages.AdditionalServices.PageDisplayed();
        }

        [StepDefinition(@"the Additional Services dashboard is presented")]
        public void ThenTheAssociatedServicesDashboardIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Additional Service").Should().BeTrue();
        }

        [StepDefinition(@"the User has chosen to manage the Additional Services section")]
        public void WhenTheUserHasChosenToManageTheAdditionalServiceSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
        }

        [Then(@"there is a control to add a Additional Service")]
        public void ThenThereIsAControlToAddAAdditionalService()
        {
            Test.Pages.AdditionalServices.AddAdditionalServiceButtonDisplayed().Should().BeTrue();
        }

        [When(@"the User chooses to add a single Additional Service")]
        public void WhenTheUserChoosesToAddASingleAdditionalService()
        {
            Test.Pages.OrderForm.ClickAddSolutionButton();
        }

        [Then(@"they are presented with the Additional Service available from their chosen Supplier")]
        [Then(@"the Additional Services of the Catalogue Solutions in the order is displayed")]
        public void ThenTheyArePresentedWithTheAdditionalServiceAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Additional Service").Should().BeTrue();
        }

        [Then(@"they can select one Additional Service to add")]
        public void ThenTheyCanSelectOneAdditionalServiceToAdd()
        {
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
        }

        [Given(@"the User is presented with Additional Services available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithAdditionalServicesAvailableFromTheirChosenSupplier()
        {
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            WhenTheUserChoosesToAddASingleAdditionalService();
            ThenTheyArePresentedWithTheAdditionalServiceAvailableFromTheirChosenSupplier();
        }

        [Given(@"the User has selected an Additional Service to add")]
        public void GivenTheUserHasSelectedAnAdditionalServiceToAdd()
        {
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            WhenTheUserChoosesToAddASingleAdditionalService();
            ThenTheyArePresentedWithTheAdditionalServiceAvailableFromTheirChosenSupplier();
            Test.Pages.OrderForm.ClickRadioButton();
        }

        [Then(@"all the available prices for that Additional Service are presented")]
        public void ThenAllTheAvailablePricesForThatAdditionalServiceArePresented()
        {
            Test.Pages.AdditionalServices.PricePageTitle().Should().ContainEquivalentOf("List price");
            ThenTheyCanSelectOneAdditionalServiceToAdd();
        }

        [Given(@"the available prices for the selected Additional Service are presented")]
        public void GivenTheAvailablePricesForTheSelectedAdditionalServiceArePresented()
        {
            GivenTheUserHasSelectedAnAdditionalServiceToAdd();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Given(@"the User has selected a Additional Service price")]
        public void GivenTheUserHasSelectedAAdditionalServicePrice()
        {
            Test.Pages.AdditionalServices.PricePageTitle().Should().ContainEquivalentOf("List price");
            Test.Pages.OrderForm.ClickRadioButton();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Then(@"the Additional Service name is displayed")]
        public void ThenTheAdditionalServiceNameIsDisplayed()
        {
            Test.Pages.AdditionalServices.ServiceRecipientsTitle().Should().MatchRegex("Service Recipient for .*");
        }

        [Then(@"the Service Recipients are presented in ascending alphabetical order by Presentation Name")]
        public void ThenTheServiceRecipientsArePresentedInAscendingAlphabeticalOrderByPresentationName()
        {
            var displayedRecipients = Test.Pages.AdditionalServices.ServiceRecipientNames();

            displayedRecipients.Should().BeInAscendingOrder();
        }

        [Given(@"the User has selected a price for the Additional Service")]
        public void GivenTheUserHasSelectedAPriceForTheAdditionalService()
        {
            Test.Pages.OrderForm.ClickRadioButton();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Given("the User is on the Edit Price form")]
        public void GivenIAmOnTheEditPriceForm()
        {
            var common = new CommonSteps(Test, Context);
            common.WhenTheOrderFormForTheExistingOrderIsPresented();
            GivenTheAvailablePricesForTheSelectedAdditionalServiceArePresented();
            GivenTheUserHasSelectedAAdditionalServicePrice();
            GivenTheUserHasSelectedAServiceRecipient();
            common.WhenTheyChooseToContinue();
        }

        [Then(@"they are presented with the Additional Service edit form for flat list price")]
        public void ThenTheyArePresentedWithTheAdditionalServiceEditFormForFlatListPrice()
        {
            Test.Driver.Url.Should().Contain("additional-services/neworderitem");
        }

        [Then(@"the form contains one item")]
        public void ThenTheFormContainsOneItem()
        {
            Test.Pages.AdditionalServices.GetTableRowsCount().Should().Be(1);
        }

        [Then(@"the item contains an input for the price")]
        public void ThenTheItemContainsAnInputForThePrice()
        {
            Test.Pages.AdditionalServices.PriceInputDisplayed().Should().BeTrue();
        }

        [Then(@"the item contains a unit of order")]
        public void ThenTheItemContainsAUnitOfOrder()
        {
            Test.Pages.AdditionalServices.PriceUnitDisplayed().Should().BeTrue();
        }

        [Then(@"the item contains an input for the quantity")]
        public void ThenTheItemContainsAnInputForTheQuantity()
        {
            Test.Pages.AdditionalServices.QuantityInputDisplayed().Should().BeTrue();
        }

        [Given(@"the User has selected a Service Recipient")]
        public void GivenTheUserHasSelectedAServiceRecipient()
        {
            Test.Pages.OrderForm.ClickRadioButton();
        }

        [When(@"the quantity is above the max value")]
        public void WhenTheQuantityIsAboveTheMaxValue()
        {
            Test.Pages.AdditionalServices.SetQuantityAboveMax();
        }

        [When(@"all data is complete and valid")]
        public void WhenAllDataIsCompleteAndValid()
        {
            Test.Pages.AdditionalServices.SetQuantity();
        }

        [Then(@"the Additional Service is saved")]
        public void ThenTheAdditionalServiceIsSaved()
        {
            ThenTheAdditionalServiceDashboardIsPresented();
            Test.Pages.OrderForm.AddedOrderItemsTableIsPopulated().Should().BeTrue();
        }

        [Then(@"the section content validation status of the Additional Service is Complete")]
        public void ThenTheSectionContentValidationStatusOfTheAdditionalServiceIsComplete()
        {
            new CommonSteps(Test, Context).WhenTheUserChoosesToGoBack();
            new OrderForm(Test, Context).ThenTheContentValidationStatusOfTheSectionIsComplete("additional-services");
        }

        [Given(@"the edit Additional Service form for flat list price with variable \(patient numbers\) order type is presented")]
        public void GivenTheEditAdditionalServiceFormForFlatListPriceWithVariablePatientNumbersOrderTypeIsPresented()
        {
            new OrderForm(Test, Context).GivenTheAdditionalServicesSectionIsComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithVariablePricedPerPatient((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedAdditionalServiceOrderItem", orderItem);
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            GivenTheUserChoosesToEditTheSavedAdditionalService();
        }

        [Given(@"the edit Additional Service form for flat list price with declarative order type is presented")]
        public void GivenTheEditAdditionalServiceFormForFlatListPriceWithDeclarativeOrderTypeIsPresented()
        {
            new OrderForm(Test, Context).GivenTheAdditionalServicesSectionIsComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithDeclarative((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedAdditionalServiceOrderItem", orderItem);
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            GivenTheUserChoosesToEditTheSavedAdditionalService();
        }

        [Given(@"the edit Additional Service form for flat list price with variable \(on demand\) order type is presented")]
        public void GivenTheEditAdditionalServiceFormForFlatListPriceWithVariableOnDemandOrderTypeIsPresented()
        {
            new OrderForm(Test, Context).GivenTheAdditionalServicesSectionIsComplete();
            var orderItem = new OrderItem().GenerateAdditionalServiceWithFlatPricedVariableOnDemand((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedAdditionalServiceOrderItem", orderItem);
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            GivenTheUserChoosesToEditTheSavedAdditionalService();
        }

        [Given(@"there is one or more Additional Services added to the order")]
        public void GivenTheThereAreOneOrMoreAddionalServicesAddedtoTheOrder()
        {
            new OrderForm(Test, Context).GivenTheAdditionalServicesSectionIsComplete();

            var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithVariablePricedPerPatient((Order)Context["CreatedOrder"]);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedAdditionalServiceOrderItem", orderItem);
        }

        [StepDefinition(@"the User chooses to edit the saved Additional service")]
        public void GivenTheUserChoosesToEditTheSavedAdditionalService()
        {
            Test.Pages.OrderForm.ClickTableRowLink();
        }

        [Then(@"the pricing values will be populated with the values that was saved by the User")]
        public void ThenThePricingValuesWillBePopulatedWithTheValuesThatWasSavedByTheUser()
        {
            var quantityFromPage = Test.Pages.OrderForm.GetQuantity();
            var priceFromPage = Test.Pages.OrderForm.GetPriceInputValue();

            var orderItem = (OrderItem)Context["CreatedAdditionalServiceOrderItem"];

            quantityFromPage.Should().Be(orderItem.Quantity.ToString());
            priceFromPage.Should().Be(orderItem.Price.ToString("F")); // ToString("F") does a financial rounding on a decimal, including adding .00 if a round number
        }

        [Given(@"the supplier added to the order has an additional service with a declarative flat price")]
        public void GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithADeclarativeFlatPrice()
        {
            var supplier = SupplierInfo.SuppliersWithAssociatedServices(Test.BapiConnectionString, ProvisioningType.Declarative).First() ?? throw new NullReferenceException("Supplier not found");
            var order = (Order)Context["CreatedOrder"];
            order.SupplierId = int.Parse(supplier.SupplierId);
            order.SupplierName = supplier.Name;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the supplier added to the order has an additional service with a patient flat price")]
        public void GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithAPatientFlatPrice()
        {
            var supplier = SupplierInfo.SuppliersWithAssociatedServices(Test.BapiConnectionString, ProvisioningType.Patient).First() ?? throw new NullReferenceException("Supplier not found");
            var order = (Order)Context["CreatedOrder"];
            order.SupplierId = int.Parse(supplier.SupplierId);
            order.SupplierName = supplier.Name;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the supplier added to the order has an additional service with an on demand flat price")]
        public void GivenTheSupplierAddedToTheOrderHasAnAdditionalServiceWithAnOnDemandFlatPrice()
        {
            var supplier = SupplierInfo.SuppliersWithAssociatedServices(Test.BapiConnectionString, ProvisioningType.OnDemand).First() ?? throw new NullReferenceException("Supplier not found");
            var order = (Order)Context["CreatedOrder"];
            order.SupplierId = int.Parse(supplier.SupplierId);
            order.SupplierName = supplier.Name;
            order.Update(Test.ConnectionString);
        }

        [Given(@"there is no Additional Service in the order but the section is complete")]
        public void GivenThereIsNoAdditionalServiceInTheOrderButTheSectionIsComplete()
        {
            var of = new OrderForm(Test, Context);
            of.GivenTheAdditionalServicesSectionIsNotCompleteAndNoServicesAreAdded();
            of.GivenTheAdditionalServicesSectionIsComplete();
        }

    }
}
