namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bogus;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using OrderFormAcceptanceTests.TestData.Utils;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class AssociatedServices : TestBase
    {
        public AssociatedServices(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Given(@"the supplier has multiple Associated Services")]
        public async Task GivenTheSupplierHasMultipleAssociatedServicesAsync()
        {
            var supplierId = await SupplierInfo.GetSupplierWithMultipleAssociatedServices(Test.BapiConnectionString);
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

        [Then(@"the User is able to manage the Associated Services section")]
        public void ThenTheUserIsAbleToManageTheAssociatedServicesSection()
        {
            Test.Pages.OrderForm.ClickEditAssociatedServices();
            ThenTheAssociatedServicesDashboardIsPresented();
        }

        [StepDefinition(@"the User has chosen to manage the Associated Service section")]
        public void WhenTheUserHasChosenToManageTheAssociatedServiceSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAssociatedServicesSection();
        }

        [StepDefinition(@"the Associated Services dashboard is presented")]
        public void ThenTheAssociatedServicesDashboardIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Associated Services").Should().BeTrue();
        }

        [Then(@"there is a control to add an Associated Service")]
        public void ThenThereIsAControlToAddAnAssociatedService()
        {
            Test.Pages.OrderForm.AddAssociatedServiceButtonDisplayed().Should().BeTrue();
        }

        [Then(@"they are presented with the Associated Services available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheAssociatedServicesAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Associated Service").Should().BeTrue();
        }

        [Given(@"the User is presented with Associated Services available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithAssociatedServicesAvailableFromTheirChosenSupplier()
        {
            WhenTheUserHasChosenToManageTheAssociatedServiceSection();
            new CommonSteps(Test, Context).WhenTheUserChoosesToAddAOrderItem();
            ThenTheyArePresentedWithTheAssociatedServicesAvailableFromTheirChosenSupplier();
        }

        [Given(@"the User is presented with the prices for the selected Associated Service")]
        public void GivenTheUserIsPresentedWithThePricesForTheSelectedAssociatedService()
        {
            GivenTheUserIsPresentedWithAssociatedServicesAvailableFromTheirChosenSupplier();
            var itemId = Test.Pages.OrderForm.ClickRadioButton(0);
            Context.Add(ContextKeys.ChosenItemId, itemId);
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
        }

        [Then(@"the User is informed they have to select an Associated Service")]
        public void ThenTheUserIsInformedTheyHaveToSelectAnAssociatedService()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectAssociatedService");
        }

        [Then(@"the User is informed they have to select a Associated Service price")]
        public void ThenTheUserIsInformedTheyHaveToSelectAAssociatedServicePrice()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectAssociatedServicePrice");
        }

        [Given(@"the User selects an Associated Service to add")]
        public void GivenTheUserSelectsAnAssociatedServiceToAdd()
        {
            var itemId = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add(ContextKeys.ChosenItemId, itemId);
        }

        [Given(@"the User selects the flat variable price type")]
        public void GivenTheUserSelectsTheFlatVariablePriceType()
        {
            Test.Pages.OrderForm.ClickRadioButton(1);
        }

        [Given(@"the User selects the flat declarative price type")]
        public void GivenTheUserSelectsTheFlatDeclarativePriceType()
        {
            Test.Pages.OrderForm.ClickRadioButton(0);
        }

        [Then(@"all the available prices for that Associated Service are presented")]
        public async Task ThenAllTheAvailablePricesForThatAssociatedServiceArePresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("List price").Should().BeTrue();
            var itemId = (string)Context[ContextKeys.ChosenItemId];
            var query = "Select count(*) FROM [dbo].[CataloguePrice] where CatalogueItemId=@itemId";
            var expectedNumberOfPrices = (await SqlExecutor.ExecuteAsync<int>(Test.BapiConnectionString, query, new { itemId })).Single();
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().Be(expectedNumberOfPrices);
        }

        [Then(@"the name of the selected Associated Service is displayed on the Associated Service edit form")]
        public async Task ThenTheNameOfTheSelectedAssociatedServiceIsDisplayedOnTheAssociatedServiceEditForm()
        {
            var itemId = (string)Context[ContextKeys.ChosenItemId];
            var query = "Select Name FROM [dbo].[CatalogueItem] where CatalogueItemId=@itemId";
            var expectedSolutionName = (await SqlExecutor.ExecuteAsync<string>(Test.BapiConnectionString, query, new { itemId })).Single();
            Test.Pages.OrderForm.TextDisplayedInPageTitle(expectedSolutionName).Should().BeTrue();
        }

        [Given(@"the User is presented with the Associated Service edit form for a variable flat price")]
        public void GivenTheUserIsPresentedWithTheAssociatedServiceEditFormForAVariableFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedAssociatedService();
            GivenTheUserSelectsTheFlatVariablePriceType();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            new CatalogueSolutions(Test, Context).ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the User is presented with the Associated Service edit form for a declarative flat price")]
        public void GivenTheUserIsPresentedWithTheAssociatedServiceEditFormForADeclarativeFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedAssociatedService();
            GivenTheUserSelectsTheFlatDeclarativePriceType();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            new CatalogueSolutions(Test, Context).ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"fills in the Associated Service edit form with valid data")]
        public void GivenFillsInTheAssociatedServiceEditFormWithValidData()
        {
            if (Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed() == 2)
            {
                Test.Pages.OrderForm.ClickRadioButton(1);
            }

            var f = new Faker();
            Test.Pages.OrderForm.EnterQuantity(f.Random.Number(min: 1, max: 99999).ToString());
            var defaultPrice = decimal.Parse(Test.Pages.OrderForm.GetPriceInputValue());
            Test.Pages.OrderForm.EnterPriceInputValue(f.Finance.Amount(max: defaultPrice).ToString());
        }

        [Given(@"an Associated Service with a flat price variable \(On-demand\) order type with the quantity period per year is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var orderItem = await OrderItemHelper.CreateOrderItem(
                order,
                CatalogueItemType.AssociatedService,
                CataloguePriceType.Flat,
                ProvisioningType.OnDemand,
                DbContext,
                Test.BapiConnectionString);

            var recipients = new List<OrderItemRecipient>();

            var recipient = new ServiceRecipient(order.OrderingParty.OdsCode, order.OrderingParty.Name);

            var orderItemRecipient = new OrderItemRecipient
            {
                Recipient = recipient,
                DeliveryDate = DateTime.UtcNow,
                Quantity = new Random().Next(1, 101),
            };

            recipients.Add(orderItemRecipient);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            order.AddOrUpdateOrderItem(orderItem);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an Associated Service has been saved to the order")]
        [Given(@"an Associated Service with a flat price declarative order type is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceDeclarativeOrderTypeIsSavedToTheOrder()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var orderItem = await OrderItemHelper.CreateOrderItem(
                order,
                CatalogueItemType.AssociatedService,
                CataloguePriceType.Flat,
                ProvisioningType.Declarative,
                DbContext,
                Test.BapiConnectionString);

            var recipients = new List<OrderItemRecipient>();

            var recipient = new ServiceRecipient(order.OrderingParty.OdsCode, order.OrderingParty.Name);

            var orderItemRecipient = new OrderItemRecipient
            {
                Recipient = recipient,
                DeliveryDate = DateTime.UtcNow,
                Quantity = new Random().Next(1, 101),
            };

            recipients.Add(orderItemRecipient);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            order.AddOrUpdateOrderItem(orderItem);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"the User amends the existing Associated Service details")]
        public void GivenTheUserAmendsTheExistingAssociatedServiceDetails()
        {
            var catalogueSolutionSteps = new CatalogueSolutions(Test, Context);
            WhenTheUserHasChosenToManageTheAssociatedServiceSection();
            catalogueSolutionSteps.ThenTheOrderItemsArePresented();
            Test.Pages.OrderForm.ClickAddedCatalogueItem();
            catalogueSolutionSteps.ThenTheyArePresentedWithTheOrderItemPriceEditForm();

            if (Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed() == 2)
            {
                var estimatedPeriod = Test.Pages.OrderForm.ClickRadioButton();
                Context.Add(ContextKeys.AmendedEstimatedPeriod, estimatedPeriod);
            }

            var f = new Faker();
            var quantity = f.Random.Number(min: 1).ToString();
            var price = f.Random.Number(min: 1).ToString();
            Test.Pages.OrderForm.EnterQuantity(quantity);
            Test.Pages.OrderForm.EnterPriceInputValue(price);

            Context.Add(ContextKeys.AmendedQuantity, quantity);
            Context.Add(ContextKeys.AmendedPrice, price);
            new OrderForm(Test, Context).WhenTheUserChoosesToSave();
            catalogueSolutionSteps.ThenTheOrderItemsArePresented();
        }

        [Then(@"the User is informed that there are no Associated Services to select")]
        public void ThenThereIsContentIndicatingThereIsNoAdditionalServicesToSelect()
        {
            Test.Pages.OrderForm.ErrorTitle().Should().BeEquivalentTo("No Associated Services found");
        }

        [Then(@"there is content indicating there is no Additional Service added")]
        public void ThenThereIsContentIndicatingThereIsNoOrderItemeAdded()
        {
            Test.Pages.AdditionalServices.NoAddedOrderItemsDisplayed().Should().BeTrue();
        }

        [Given(@"there is no Associated Service in the order but the section is complete")]
        public async Task GivenThereIsNoAssociatedServiceInTheOrderButTheSectionIsComplete()
        {
            await new CommonSteps(Test, Context).SetOrderAssociatedServicesSectionToComplete();
        }

        [Given(@"the supplier added to the order has an associated service with a declarative flat price")]
        public async Task GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceDeclarativeAsync()
        {
            var supplier = (await SupplierInfo.SupplierLookup(Test.BapiConnectionString, CatalogueItemType.AssociatedService, ProvisioningType.Declarative)).First()
                .ToDomain();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            order.Supplier = supplier;

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();
        }

        [Given(@"the supplier added to the order has an associated service with an on-demand flat price")]
        public async Task GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceOnDemandAsync()
        {
            var supplier = (await SupplierInfo.SupplierLookup(Test.BapiConnectionString, CatalogueItemType.AssociatedService, ProvisioningType.OnDemand)).First()
                .ToDomain();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            order.Supplier = supplier;

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();
        }

        [Given(@"the Select Associated Service form is presented")]
        public async Task GivenTheSelectAssociatedServiceFormIsPresentedAsync()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];

            order.OrderingPartyContact = ContactHelper.Generate();

            var supplier = await DbContext.Supplier.SingleOrDefaultAsync(s => s.Id == "10000")
                ?? (await SupplierInfo.GetSupplierWithId("10000", Test.BapiConnectionString)).ToDomain();

            var orderBuilder = new OrderBuilder(order)
                .WithExistingSupplier(supplier)
                .WithSupplierContact(ContactHelper.Generate());

            order = orderBuilder.Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);

            WhenTheUserHasChosenToManageTheAssociatedServiceSection();
            new CommonSteps(Test, Context).WhenTheUserChoosesToAddAOrderItem();
        }

        [When(@"the User selects an Associated Service previously saved in the Order")]
        public async Task WhenTheUserSelectsAnAssociatedServicePreviouslySavedInTheOrderAsync()
        {
            Test.Pages.OrderForm.ClickAddSolutionButton();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemInDB = (await DbContext.Order.FindAsync(order.Id))
                .OrderItems.First(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.AssociatedService);

            var associatedRadioButton = Test.Pages.OrderForm.GetRadioButtonText().IndexOf(orderItemInDB.CatalogueItem.Name);

            Test.Pages.OrderForm.ClickRadioButton(associatedRadioButton);
        }

        [When(@"the edit Associated Service form is presented")]
        public void WhenTheEditAssociatedServiceFormIsPresented()
        {
            Test.Pages.OrderForm.ClickContinueButton();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("information for").Should().BeTrue();
        }

        [Then(@"the previously saved data is Displayed")]
        public async Task ThenThePreviouslySavedDataIsDisplayedAsync()
        {
            var priceFromPage = decimal.Parse(Test.Pages.OrderForm.GetPriceInputValue());

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemsInDb = (await DbContext.Order.FindAsync(order.Id))
                .OrderItems.Single(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.AssociatedService);
            var unitOrder = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);

            orderItemsInDb.Price.Value.Should().Be(priceFromPage);
            Test.Pages.OrderForm.GetAssociatedServicesPricingUnit(unitOrder).Should().BeTrue();
        }

        [Given(@"the Edit Associated Service form is displayed")]
        public void GivenTheEditAssociatedServiceFormIsDisplayed()
        {
            Test.Pages.OrderForm.ClickAddedCatalogueItem();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("information for").Should().BeTrue();
        }

        [Given(@"User enters a price greater than the list price selected")]
        public async Task GivenUserEntersAPriceGreaterThanTheListPriceSelectedAsync()
        {
            // (1) get price of solution in the DB
            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemsInDb = (await DbContext.Order.FindAsync(order.Id))
                .OrderItems.Single(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.AssociatedService);

            // (2) increase the value of the db price
            var dbPrice = orderItemsInDb.Price.Value;
            var exceededValue = dbPrice + 0.1m;

            // var exceededValue = decimal.Add(dbPrice, 0.1m);

            // (3) clear the default value in the price field and enter a value higher than the item price
            Test.Pages.OrderForm.EnterPriceInputValue(exceededValue.ToString());

            Test.Pages.OrderForm.ClickSaveButton();
        }

        [When(@"they choose to save")]
        public void WhenTheyChooseToSave()
        {
            Test.Pages.OrderForm.ClickSaveButton();
        }

        [Then(@"they are informed")]
        public void ThenTheyAreInformed()
        {
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
        }

        [Given(@"the User enters a price less than or equal to the list price selected")]
        public async Task GivenTheUserEntersAPriceLessThanOrEqualToTheListPriceSelectedAsync()
        {
            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemsInDb = (await DbContext.Order.FindAsync(order.Id))
                .OrderItems.Single(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.AssociatedService);

            var dbPrice = orderItemsInDb.Price.Value;

            Test.Pages.OrderForm.EnterPriceInputValue(dbPrice.ToString());

            Test.Pages.OrderForm.ClickSaveButton();
        }

        [Then(@"the price is valid")]
        public void ThenThePriceIsValid()
        {
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeFalse();
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeTrue();
        }
    }
}
