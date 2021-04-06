namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Domain.Users;
    using OrderFormAcceptanceTests.Persistence.Data;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using OrderFormAcceptanceTests.TestData.Information;
    using OrderFormAcceptanceTests.TestData.Models;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class CommonSteps : TestBase
    {
        public CommonSteps(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Given(@"the User has not completed the Order")]
        [Given(@"no Funding Source option is selected")]
        [StepDefinition(@"the Associated Service is saved")]
        [Then(@"the Associated Service is not saved")]
        [Given(@"no Associated Service price is selected")]
        [Then(@".* section is not saved")]
        [Then(@"the Catalogue Solution is not saved")]
        [StepDefinition(@"the Catalogue Solution is saved")]
        [Given(@"no Supplier is selected")]
        [Then("the Commencement Date information is not saved")]
        [Given(@"the Call Off Ordering Party is not selected")]
        [Given(@"the User chooses not to add a Catalogue Solution")]
        [Given(@"no Catalogue Solution is selected")]
        [Given(@"no Catalogue Solution price is selected")]
        [StepDefinition(@"no Service Recipient is selected")]
        [Given(@"there are no Catalogue Solution items in the order")]
        [Given(@"no Additional Service is selected")]
        [StepDefinition(@"there is no Additional Service added to the order")]
        [Then("the Additional Service price is not saved")]
        [When(@"there is no Associated Service added to the order")]
        [Given(@"the User chooses not to add an Associated Service")]
        [Given(@"no Associated Service is selected")]
        [Then("the Additional Service is not saved")]
        [Given(@"the Funding Source section is not complete")]
        [Given(@"there are no Service Recipients in the order")]
        [Given(@"the Call Off Ordering Party section is not complete")]
        [Given(@"the Supplier section is not complete")]
        [Given(@"the Commencement Date section is not complete")]
        [Given(@"the Service Recipients section is not complete")]
        [Given(@"the Catalogue Solutions section is not complete")]
        public static void DoNothing()
        {
            // do nothing
        }

        public static void AssertListOfStringsIsInAscendingOrder(IEnumerable<string> stringList)
        {
            var hexList = stringList
                .Select(s => Encoding.UTF8.GetBytes(s)) // Convert to byte[]
                .Select(h => BitConverter.ToString(h)) // convert byte[] to hex string
                .Select(r => r.Replace("-", string.Empty)) // remove any '-' characters
                .ToList();
            hexList.Should().BeInAscendingOrder();
        }

        [Then(@"the Order is deleted")]
        public async Task ThenTheOrderIsDeleted()
        {
            var orderId = Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId;

            var orderInDb = await DbContext.Order.SingleOrDefaultAsync(o => o.CallOffId == orderId);

            orderInDb.Should().BeNull();
        }

        [Given(@"that a buyer user has logged in")]
        public async Task GivenThatABuyerUserHasLoggedIn()
        {
            if (!Context.ContainsKey(ContextKeys.User))
            {
                await CreateUser(UserType.Buyer);
            }

            User user = (User)Context[ContextKeys.User];

            Test.Pages.Authentication.Login(user.UserName, UsersHelper.GenericTestPassword());
        }

        [Given(@"the User has chosen to manage a new Order Form")]
        public void GivenTheUserHasChosenToManageANewOrderForm()
        {
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrder();
        }

        [Then(@"the new Order is presented")]
        [When(@"the Order Form is presented")]
        public void ThenTheNewOrderIsPresented()
        {
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
        }

        [Given(@"mandatory data are missing")]
        [When(@"the User has not entered a Supplier search criterion")]
        public void GivenMandatoryDataAreMissing()
        {
            var listOfInputs = Test.Driver.FindElements(By.ClassName("nhsuk-input"));
            foreach (var element in listOfInputs)
            {
                element.Clear();
            }
        }

        [Given(@"an incomplete order exists")]
        public async Task GivenAnIncompleteOrderExists()
        {
            var context = (OrderingDbContext)Context[ContextKeys.DbContext];
            var user = (User)Context[ContextKeys.User];
            var createModel = new CreateOrderModel { Description = RandomInformation.RandomInformationText(), OrganisationId = user.PrimaryOrganisationId };
            var order = await OrderHelpers.CreateOrderAsync(createModel, context, user, Test.BapiConnectionString, Test.IsapiConnectionString);

            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"the Call Off Ordering Party section is complete")]
        public async Task GivenTheCallOffOrderingPartySectionIsComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderBuilder = new OrderBuilder(order)
                .WithOrderingPartyContact(ContactHelper.Generate());

            order = orderBuilder.Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"a supplier has been selected")]
        public async Task GivenASupplierHasBeenSelected()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];

            var supplier = await DbContext.Supplier.SingleOrDefaultAsync(s => s.Id == "100000")
                ?? (await SupplierInfo.GetSupplierWithId("100000", Test.BapiConnectionString)).ToDomain();

            var orderBuilder = new OrderBuilder(order)
                .WithExistingSupplier(supplier)
                .WithSupplierContact(ContactHelper.Generate());

            order = orderBuilder.Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an incomplete order with catalogue items exists")]
        public async Task GivenAnIncompleteOrderWithCatalogueItemsExists()
        {
            await GivenAnIncompleteOrderExists();
            var order = (Order)Context[ContextKeys.CreatedOrder];

            order.OrderingPartyContact = ContactHelper.Generate();
            order.CommencementDate = DateTime.Today;

            var supplier = await DbContext.Supplier.SingleOrDefaultAsync(s => s.Id == "100000")
                ?? (await SupplierInfo.GetSupplierWithId("100000", Test.BapiConnectionString)).ToDomain();

            var orderBuilder = new OrderBuilder(order)
                .WithExistingSupplier(supplier)
                .WithSupplierContact(ContactHelper.Generate());

            order = orderBuilder.Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();

            await GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder(100);

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"my organisation has one or more orders")]
        public async Task GivenOneOrMoreOrdersExist()
        {
            await GivenACompleteOrderExists();
        }

        [Given(@"a complete order exists")]
        [Given(@"an order is completed")]
        [Given(@"a User has completed an Order")]
        public async Task GivenACompleteOrderExists()
        {
            var completeOrder = new CompleteOrder(Test, Context);

            await completeOrder.GivenTheOrderIsCompleteEnoughSoThatTheCompleteOrderButtonIsEnabled("yes");
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);

            var result = order.Complete();

            if (!result)
            {
                throw new DbUpdateException($"Order {order.CallOffId} not completed");
            }

            await DbContext.SaveChangesAsync();

            (await DbContext.Order.SingleAsync(o => o.CallOffId == order.CallOffId)).Completed.Should().NotBeNull();

            Test.Driver.Navigate().Refresh();
        }

        [StepDefinition(@"the Order Form for the existing order is presented")]
        public void WhenTheOrderFormForTheExistingOrderIsPresented()
        {
            Test.Driver.Navigate().Refresh();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrganisationsOrdersDashboard.SelectExistingOrder(((Order)Context[ContextKeys.CreatedOrder]).CallOffId.ToString(), Test.Url);
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeTrue();
        }

        [StepDefinition(@"the User chooses to go back")]
        public void WhenTheUserChoosesToGoBack()
        {
            Test.Pages.OrderForm.ClickBackLink();
        }

        [When(@"they choose to continue")]
        [StepDefinition(@"the User chooses to continue")]
        public void WhenTheyChooseToContinue()
        {
            Test.Pages.OrderForm.ClickContinueButton();
        }

        [Then(@"there is content indicating there is no Catalogue Solution added")]
        [Then(@"there is content indicating there is no Associated Service added")]
        public void ThenThereIsContentIndicatingThereIsNoCatalogueSolutionAdded()
        {
            Test.Pages.OrderForm.NoSolutionsAddedDisplayed().Should().BeTrue();
        }

        [When(@"the User chooses to add a single Catalogue Solution")]
        [When(@"the User has chosen to Add a single Associated Service")]
        public void WhenTheUserChoosesToAddAOrderItem()
        {
            Test.Pages.OrderForm.ClickAddSolutionButton();
        }

        [Then(@"there is a control to continue")]
        public void ThenThereIsAControlToContinue()
        {
            Test.Pages.OrderForm.ContinueButtonDisplayed().Should().BeTrue();
        }

        [Then(@"there is no Continue button")]
        public void ThenThereIsNoButton()
        {
            Test.Pages.OrderForm.ContinueButtonDisplayed().Should().BeFalse();
        }

        [Then(@"they can select one or more Service Recipients for the Catalogue Solution")]
        [Then(@"the User is able to select the Call Off Ordering Party")]
        [Then(@"they are presented with the Service Recipients for the Order")]
        public void ThenTheyCanSelectOneOrMoreCheckbox()
        {
            Test.Pages.OrderForm.NumberOfCheckboxesDisplayed().Should().BeGreaterThan(0);
        }

        [Then(@"they can select a price for the Associated Service")]
        [Then(@"they can select one Catalogue Solution to add")]
        [Then(@"they can select a price for the Catalogue Solution")]
        [Then(@"they can select one Associated Service to add")]
        public void ThenTheyCanSelectOneRadioButton()
        {
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
        }

        [Then(@"they are presented with the Service Recipients saved in the Order")]
        public void ThenTheServiceRecipientsAreDisplayed()
        {
            Test.Pages.OrderForm.NumberOfCheckboxesDisplayed().Should().BeGreaterThan(0);
        }

        [Then(@"the User's selected catalogue solution is selected")]
        [Then(@"the User's selected price is selected")]
        [Then(@"the User's selected Associated Service is selected")]
        [Then(@"the User's selected Additional Service is selected")]
        public void ThenARadioOptiionIsSelected()
        {
            Test.Pages.OrderForm.IsRadioButtonSelected().Should().BeTrue();
        }

        [Then("a new tab will open")]
        public void ThenANewTabWillOpen()
        {
            Test.Pages.OrderForm.ASecondTabIsOpen().Should().BeTrue();
        }

        [Then("the tab will contain the printable version of the Order Summary")]
        public void ThenTheTabWillContainThePrintableVersionOfTheOrderSummary()
        {
            Test.Pages.OrderForm.FindPrintableSummary().Should().BeTrue();
        }

        [Then("the Print Dialog within the Browser will appear automatically")]
        public void ThenThePrintPreviewDialogueWithinTheBrowserWillAppearAutomatically()
        {
            Test.Pages.OrderForm.FindPrintPreviewWindow().Should().BeTrue();
        }

        [Then(@"only the published (.*) are available for selection")]
        public async Task ThenOnlyThePublishedAdditionalServicesAreAvailableForSelection(string itemType)
        {
            var itemTypeId = ConvertType(itemType);

            var publishedItems = await SupplierInfo.GetPublishedCatalogueItems(Test.BapiConnectionString, Context.Get<Order>(ContextKeys.CreatedOrder).Supplier.Id, itemTypeId);

            var displayedItems = Test.Pages.OrderForm.GetRadioButtonText();

            displayedItems.Should().BeEquivalentTo(publishedItems.Select(s => s.Name));
        }

        [Given(@"a Catalogue Solution is added to the order")]
        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per year is saved to the order")]
        public async Task GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.Solution, CataloguePriceType.Flat, ProvisioningType.OnDemand, DbContext, Test.BapiConnectionString);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"a catalogue solution with a flat price variable \(On-demand\) order type with the quantity period per month is saved to the order")]
        public async Task GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.Solution, CataloguePriceType.Flat, ProvisioningType.OnDemand, DbContext, Test.BapiConnectionString, TimeUnit.PerMonth);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"a catalogue solution with a flat price variable \(Per-Patient\) order type is saved to the order")]
        public async Task GivenACatalogueSolutionWithAFlatPriceVariablePer_PatientOrderTypeIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.Solution, CataloguePriceType.Flat, ProvisioningType.Patient, DbContext, Test.BapiConnectionString);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"a catalogue solution with a flat price variable \(Declarative\) order type is saved to the order (.*)")]
        public async Task GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder(int numRecipients = 1)
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.Solution, CataloguePriceType.Flat, ProvisioningType.Declarative, DbContext, Test.BapiConnectionString);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl, numRecipients);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an Additional Service is added to the order")]
        [Given(@"an additional service with a flat price variable Declarative order type is saved to the order")]
        public async Task GivenAnAdditionalServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(
                order,
                CatalogueItemType.AdditionalService,
                CataloguePriceType.Flat,
                ProvisioningType.Declarative,
                DbContext,
                Test.BapiConnectionString);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an additional service with a flat price variable On Demand order type with the quantity period per year is saved to the order")]
        public async Task GivenAnAdditionalServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(
                order,
                CatalogueItemType.AdditionalService,
                CataloguePriceType.Flat,
                ProvisioningType.OnDemand,
                DbContext,
                Test.BapiConnectionString);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an additional service with a flat price variable On-Demand order type with the quantity period per month is saved to the order")]
        public async Task GivenAnAdditionalServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.AdditionalService, CataloguePriceType.Flat, ProvisioningType.Declarative, DbContext, Test.BapiConnectionString, TimeUnit.PerMonth);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an additional service with a flat price variable Patient order type is saved to the order")]
        public async Task GivenAnAdditionalServiceWithAFlatPriceVariablePatientOrderTypeIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.AdditionalService, CataloguePriceType.Flat, ProvisioningType.Patient, DbContext, Test.BapiConnectionString);

            var recipients = await ServiceRecipientHelper.Generate(order.OrderingParty.OdsCode, Test.OdsUrl);

            await OrderItemHelper.AddRecipientToOrderItem(orderItem, recipients, DbContext);

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an associated service with a flat price variable \(Declarative\) order type is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.AssociatedService, CataloguePriceType.Flat, ProvisioningType.Declarative, DbContext, Test.BapiConnectionString);

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

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an associated service with a flat price variable \(On-Demand\) order type with the quantity period per year is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerYearIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(
                order,
                CatalogueItemType.AssociatedService,
                CataloguePriceType.Flat,
                ProvisioningType.OnDemand,
                DbContext,
                Test.BapiConnectionString,
                TimeUnit.PerYear);

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

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"an associated service with a flat price variable \(On-Demand\) order type with the quantity period per month is saved to the order")]
        public async Task GivenAnAssociatedServiceWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder()
        {
            await SetOrderCatalogueSectionToComplete();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            var orderItem = await OrderItemHelper.CreateOrderItem(order, CatalogueItemType.AssociatedService, CataloguePriceType.Flat, ProvisioningType.OnDemand, DbContext, Test.BapiConnectionString, TimeUnit.PerMonth);

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

            var selectedRecipients = new List<SelectedServiceRecipient>();
            selectedRecipients.AddRange(recipients.Select(r => new SelectedServiceRecipient { Recipient = r.Recipient }));

            order.AddOrUpdateOrderItem(orderItem);
            order.SetSelectedServiceRecipients(selectedRecipients);

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"the Supplier and Call Off Ordering Party sections are complete")]
        public async Task GivenTheSupplierAndCallOffOrderingPartySectionsAreComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];

            order.OrderingPartyContact = ContactHelper.Generate();

            var supplier = await DbContext.Supplier.SingleOrDefaultAsync(s => s.Id == "100000")
                ?? (await SupplierInfo.GetSupplierWithId("100000", Test.BapiConnectionString)).ToDomain();

            var orderBuilder = new OrderBuilder(order)
                .WithExistingSupplier(supplier)
                .WithSupplierContact(ContactHelper.Generate());

            order = orderBuilder.Build();

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();

            Context.Remove(ContextKeys.CreatedOrder);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"the Commencement Date is complete")]
        public async Task GivenTheCommencementDateIsComplete()
        {
            await SetCommencementDate();
        }

        [When(@"the Order is in the '(.*)' table")]
        public void WhenTheOrderIsInTheTable(string table)
        {
            List<OrderTableItem> orders;

            if (table.Contains("incomplete", StringComparison.InvariantCultureIgnoreCase))
            {
                orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfIncompleteOrders();
            }
            else
            {
                orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();
            }

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            bool result = false;
            foreach (var tableOrder in orders)
            {
                result = tableOrder.Id == order.CallOffId.ToString();
            }

            result.Should().BeTrue();
        }

        [Then(@"there is an indication that the Order has not been processed automatically")]
        public void ThenThereIsAnIndicationThatTheOrderHasNotBeenProcessedAutomatically()
        {
            var completedOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            completedOrders.Single(o => o.Id == order.CallOffId.ToString()).AutomaticallyProcessed.Should().BeFalse();
        }

        [Then(@"there is an indication that the Order has been processed automatically")]
        public void ThenThereIsAnIndicationThatTheOrderHasBeenProcessedAutomatically()
        {
            var completedOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            completedOrders.Single(o => o.Id == order.CallOffId.ToString()).AutomaticallyProcessed.Should().BeTrue();
        }

        [Then(@"the completed order summary contains the date the Order was completed")]
        public void ThenTheCompletedOrderSummaryContainsTheDateTheOrderWasCompleted()
        {
            var completedOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            completedOrders.All(o => o.Completed.HasValue).Should().BeTrue();
        }

        [Then(@"the Order is not completed")]
        public async Task ThenTheOrderIsNotCompleted()
        {
            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            (await DbContext.Order.FindAsync(order.Id)).Completed.Should().BeNull();
        }

        [Then(@"the status of the Order does not change to deleted")]
        public async Task ThenTheStatusOfTheOrderDoesNotChangeToDeleted()
        {
            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            (await DbContext.Order.FindAsync(order.Id)).IsDeleted.Should().BeFalse();
        }

        [Then(@"the Order is not on the Organisation's Orders Dashboard")]
        public void ThenTheOrderIsNotOnTheOrganisationSOrdersDashboard()
        {
            WhenTheUserChoosesToGoBack();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);

            var allOrders = new List<OrderTableItem>();
            allOrders.AddRange(Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders());
            allOrders.AddRange(Test.Pages.OrganisationsOrdersDashboard.GetListOfIncompleteOrders());

            allOrders.Select(s => s.Id).Should().NotContain(order.CallOffId.ToString());
        }

        public void ContinueAndWaitForCheckboxes()
        {
            WhenTheyChooseToContinue();
            ThenTheyCanSelectOneOrMoreCheckbox();
        }

        public void ContinueAndWaitForRadioButtons()
        {
            WhenTheyChooseToContinue();
            ThenTheyCanSelectOneRadioButton();
        }

        public async Task CreateUser(UserType userType)
        {
            Organisation organisation = await Organisation.GetByODSCode("27D", Test.IsapiConnectionString);
            var user = UsersHelper.GenerateRandomUser(organisation.OrganisationId, new User() { UserType = userType });
            await UsersHelper.Create(Test.IsapiConnectionString, user);

            Context.Add(ContextKeys.User, user);
        }

        public async Task SetOrderCatalogueSectionToComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.Progress.CatalogueSolutionsViewed = true;

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();
        }

        public async Task SetOrderAdditionalServicesSectionToComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.Progress.AdditionalServicesViewed = true;

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();
        }

        public async Task SetOrderAssociatedServicesSectionToComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];

            var dbOrder = await DbContext.Order.FindAsync(order.Id);
            dbOrder.Progress.AssociatedServicesViewed = true;

            DbContext.Update(dbOrder);

            await DbContext.SaveChangesAsync();
        }

        public async Task SetCommencementDate(DateTime? date = null)
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.CommencementDate = date ?? DateTime.Today;

            DbContext.Update(order);

            await DbContext.SaveChangesAsync();
        }

        private static CatalogueItemType ConvertType(string itemType)
        {
            return itemType.ToLower() switch
            {
                "catalogue solution" => CatalogueItemType.Solution,
                "additional services" => CatalogueItemType.AdditionalService,
                "associated services" => CatalogueItemType.AssociatedService,
                _ => throw new ArgumentOutOfRangeException(nameof(itemType), "Item type not recognised"),
            };
        }
    }
}
