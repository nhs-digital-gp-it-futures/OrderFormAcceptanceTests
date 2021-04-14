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
            Test.Pages.OrderForm.EnterQuantity(f.Random.Number(min: 1).ToString());
            Test.Pages.OrderForm.EnterPriceInputValue(f.Finance.Amount().ToString());
        }

        [Given(@"an Associated Service with a flat price variable \(On-demand\) order type with the quantity period per year is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            SetOrderAssociatedServicesSectionToComplete();
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

        [Given(@"an Associated Service with a flat price declarative order type is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceDeclarativeOrderTypeIsSavedToTheOrder()
        {
            SetOrderAssociatedServicesSectionToComplete();

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

        [StepDefinition(@"the Associated Service is saved in the DB")]
        public void GivenTheAssociatedServiceIsSavedInTheDB()
        {
            // var order = (Order)Context[ContextKeys.CreatedOrder];
            // var orderItem = OrderItem.RetrieveByOrderId(Test.OrdapiConnectionString, order.Id, 3).First();
            // Context.Add(ContextKeys.CreatedOrderItem, orderItem);
            // orderItem.Should().NotBeNull();
        }

        [Given(@"there is no Associated Service in the order but the section is complete")]
        public async Task GivenThereIsNoAssociatedServiceInTheOrderButTheSectionIsComplete()
        {
            await new CommonSteps(Test, Context).SetOrderAssociatedServicesSectionToComplete();
        }

        [Given(@"the supplier added to the order has an associated service with a declarative flat price")]
        public void GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceDeclarative()
        {
            /*var supplier = GetSupplierDetails(ProvisioningType.Declarative);
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.SupplierId = int.Parse(supplier.SupplierId);
            order.SupplierName = supplier.Name;
            order.Update(Test.OrdapiConnectionString);*/
        }

        [Given(@"the supplier added to the order has an associated service with an on-demand flat price")]
        public void GivenTheSupplierAddedToTheOrderHasAnAssociatedServiceOnDemand()
        {
            // var supplier = GetSupplierDetails(Domain.ProvisioningType.OnDemand);
            // var order = (Order)Context[ContextKeys.CreatedOrder];
            // order.SupplierId = int.Parse(supplier.SupplierId);
            // order.SupplierName = supplier.Name;
            // order.Update(Test.OrdapiConnectionString);
        }

        public void SetOrderAssociatedServicesSectionToComplete()
        {
            // var order = (Order)Context[ContextKeys.CreatedOrder];
            // order.AssociatedServicesViewed = 1;
            // order.Update(Test.OrdapiConnectionString);
        }
    }
}
