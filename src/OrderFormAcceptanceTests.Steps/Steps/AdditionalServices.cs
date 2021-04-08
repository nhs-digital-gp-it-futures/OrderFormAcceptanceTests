namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class AdditionalServices : TestBase
    {
        public AdditionalServices(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Given(@"the supplier has multiple Additional Services")]
        public async Task GivenTheSupplierHasMultipleAdditionalServicesAsync()
        {
            var solutionId = await SupplierInfo.GetSolutionWithMultipleAdditionalServices(Test.BapiConnectionString);

            Context.Add(ContextKeys.ChosenItemId, solutionId);

            var supplierId = solutionId.Split('-')[0];
            var supplier = await DbContext.Supplier.SingleOrDefaultAsync(s => s.Id == supplierId)
                ?? (await SupplierInfo.GetSupplierWithId(supplierId, Test.BapiConnectionString)).ToDomain();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            order = new OrderBuilder(order)
                .WithExistingSupplier(supplier)
                .WithSupplierContact(ContactHelper.Generate())
                .WithCommencementDate(DateTime.Today)
                .WithOrderingPartyContact(ContactHelper.Generate())
                .Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"the Catalogue Solution for the Additional Services has been added")]
        public async Task GivenTheCatalogueSolutionForTheAdditionalServicesHasBeenAddedAsync()
        {
            var solutionId = Context.Get<string>(ContextKeys.ChosenItemId);

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            var orderItem = await OrderItemHelper.GetOrderItemWithId(order, solutionId, Test.BapiConnectionString, DbContext);

            order.AddOrUpdateOrderItem(orderItem);

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();
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

        [StepDefinition(@"the User chooses to add a single Additional Service")]
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
            Context.Add(ContextKeys.ChosenItemId, Test.Pages.OrderForm.GetRadioButtonText()[0]);
            Test.Pages.OrderForm.ClickRadioButton();
        }

        [Given(@"the supplier has an additional service with more than one price")]
        public async Task GivenTheSupplierHasAnAdditionalServiceWithMoreThanOnePriceAsync()
        {
            var supplier = await Actions.Pages.OrderForm.SupplierHasAdditionalServiceMoreThan1Price(Test.BapiConnectionString);
            var order = new OrderBuilder(Context.Get<Order>(ContextKeys.CreatedOrder))
                .WithExistingSupplier(supplier)
                .Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();
        }

        [Then(@"all the available prices for that Additional Service are presented")]
        public void ThenAllTheAvailablePricesForThatAdditionalServiceArePresented()
        {
            Test.Pages.AdditionalServices.PricePageTitle().Should().ContainEquivalentOf("List price");
            ThenTheyCanSelectOneAdditionalServiceToAdd();
        }

        [Then(@"the User is presented with the correct page")]
        public async Task ThenTheUserIsPresentedWithTheCorrectPageAsync()
        {
            var itemId = Test.Pages.OrderForm.GetSelectedRadioButton();
            var numberOfPrices = await OrderItemHelper.GetNumberOfPricingUnitsForItemAsync(itemId, Test.BapiConnectionString);

            var pageTitle = numberOfPrices == 1 ? "Add Additional Service" : "List Price";

            Test.Pages.OrderForm.GetPageTitle().Should().ContainEquivalentOf(pageTitle);
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
            if (Test.Pages.OrderForm.TextDisplayedInPageTitle("List Price"))
            {
                Test.Pages.OrderForm.ClickRadioButton();
                new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            }
        }

        [Then(@"the Additional Service name is displayed")]
        public void ThenTheAdditionalServiceNameIsDisplayed()
        {
            Test.Pages.AdditionalServices.ServiceRecipientsTitle().Should().MatchRegex("Service Recipients for .*");
        }

        [Then(@"the Service Recipients are presented in ascending alphabetical order by Presentation Name")]
        public void ThenTheServiceRecipientsArePresentedInAscendingAlphabeticalOrderByPresentationName()
        {
            CommonSteps.AssertListOfStringsIsInAscendingOrder(Test.Pages.AdditionalServices.ServiceRecipientNames());
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
            try
            {
                GivenTheAvailablePricesForTheSelectedAdditionalServiceArePresented();
                GivenTheUserHasSelectedAAdditionalServicePrice();
            }
            catch
            {
            }

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
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Then(@"the section content validation status of the Additional Service is Complete")]
        public void ThenTheSectionContentValidationStatusOfTheAdditionalServiceIsComplete()
        {
            new CommonSteps(Test, Context).WhenTheUserChoosesToGoBack();
            new OrderForm(Test, Context).ThenTheContentValidationStatusOfTheSectionIsComplete("additional-services");
        }

        [Given(@"the edit Additional Service form for flat list price with variable \(patient numbers\) order type is presented")]
        [Given(@"the edit Additional Service form for flat list price with declarative order type is presented")]
        [Given(@"the edit Additional Service form for flat list price with variable \(on demand\) order type is presented")]
        public void GivenTheEditAdditionalServiceFormForFlatListPriceWithVariablePatientNumbersOrderTypeIsPresented()
        {
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            GivenTheUserChoosesToEditTheSavedAdditionalService();
        }

        [Given(@"there is one or more Additional Services added to the order")]
        public async Task GivenTheThereAreOneOrMoreAddionalServicesAddedtoTheOrder()
        {
            await new CommonSteps(Test, Context).GivenAnAdditionalServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder();
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

            var orderItem = (OrderItem)Context[ContextKeys.CreatedAdditionalServiceOrderItem];

            quantityFromPage.Should().Be(orderItem.OrderItemRecipients[0].Quantity.ToString());
            priceFromPage.Should().MatchRegex(@"^[0-9]*(\.[0-9]{2,3})?$");
        }

        [Given(@"there is no Additional Service in the order but the section is complete")]
        public async Task GivenThereIsNoAdditionalServiceInTheOrderButTheSectionIsComplete()
        {
            var of = new OrderForm(Test, Context);
            await of.GivenTheAdditionalServicesSectionIsNotCompleteAndNoServicesAreAdded();
            await new CommonSteps(Test, Context).SetOrderAdditionalServicesSectionToComplete();
        }
    }
}
