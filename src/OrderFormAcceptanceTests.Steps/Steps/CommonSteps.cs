namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Bogus;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class CommonSteps : TestBase
    {
        public CommonSteps(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Then(@"the Order is deleted")]
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

        [Given(@"that a buyer user has logged in")]
        public void GivenThatABuyerUserHasLoggedIn()
        {
            if (!Context.ContainsKey(ContextKeys.User))
            {
                CreateUser(UserType.Buyer);
            }

            User user = (User)Context[ContextKeys.User];

            Test.Pages.Authentication.Login(user.UserName, User.GenericTestPassword());
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
            // clear fields
            // var listOfTextAreas = Test.Driver.FindElements(By.TagName("textarea"));
            var listOfInputs = Test.Driver.FindElements(By.ClassName("nhsuk-input"));
            foreach (var element in listOfInputs)
            {
                element.Clear();
            }
        }

        [Given(@"an incomplete order exists")]
        public void GivenAnIncompleteOrderExists()
        {
            var orgAddress = Address.Generate();
            var orgContact = Contact.Generate();
            var supplierAddress = Address.Generate();
            var supplierContact = Contact.Generate();

            if (!Context.ContainsKey(ContextKeys.Organisation))
            {
                Context.Add(ContextKeys.Organisation, new Organisation().RetrieveRandomOrganisationWithNoUsers(Test.IsapiConnectionString));
            }

            var organisation = (Organisation)Context[ContextKeys.Organisation];

            var order = Order.Generate(organisation);

            order.SupplierId = 100000;
            order.SupplierName = "Really Kool Corporation";

            order.CommencementDate = new Faker().Date.Future().Date;

            orgAddress.Create(Test.OrdapiConnectionString);
            orgAddress = orgAddress.Retrieve(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedAddress, orgAddress);

            orgContact.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedContact, orgContact);

            supplierAddress.Create(Test.OrdapiConnectionString);
            supplierAddress = supplierAddress.Retrieve(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedSupplierAddress, supplierAddress);

            supplierContact.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedSupplierContact, supplierContact);

            order.OrganisationAddressId = orgAddress.AddressId;
            order.OrganisationBillingAddressId = orgAddress.AddressId;
            order.OrganisationContactId = orgContact.ContactId;
            order.SupplierAddressId = supplierAddress.AddressId;
            order.SupplierContactId = supplierContact.ContactId;

            order.Create(Test.OrdapiConnectionString);
            if (!Context.ContainsKey(ContextKeys.CreatedOrder))
            {
                Context.Add(ContextKeys.CreatedOrder, order);
            }

            Context.TryGetValue(ContextKeys.CreatedIncompleteOrders, out IList<Order> createdOrders);
            createdOrders ??= new List<Order>();
            createdOrders.Add(order);
            Context.Set(createdOrders, ContextKeys.CreatedIncompleteOrders);
        }

        [Given(@"an incomplete order exists without a commencement date")]
        public void GivenAnIncompleteOrderExistsNoCommencement()
        {
            var orgAddress = Address.Generate();
            orgAddress.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedAddress, orgAddress);
            var orgContact = Contact.Generate();
            orgContact.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedContact, orgContact);

            var supplierAddress = Address.Generate();
            supplierAddress.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedSupplierAddress, supplierAddress);
            var supplierContact = Contact.Generate();
            supplierContact.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedSupplierContact, supplierContact);

            if (!Context.ContainsKey(ContextKeys.Organisation))
            {
                Context.Add(ContextKeys.Organisation, new Organisation().RetrieveRandomOrganisationWithNoUsers(Test.IsapiConnectionString));
            }

            var organisation = (Organisation)Context[ContextKeys.Organisation];

            var order = Order.Generate(organisation);
            order.OrganisationAddressId = orgAddress.AddressId;
            order.OrganisationBillingAddressId = orgAddress.AddressId;
            order.OrganisationContactId = orgContact.ContactId;
            order.SupplierAddressId = supplierAddress.AddressId;
            order.SupplierContactId = supplierContact.ContactId;
            order.SupplierId = 100000;
            order.SupplierName = "Really Kool Corporation";

            order.Create(Test.OrdapiConnectionString);
            if (!Context.ContainsKey(ContextKeys.CreatedOrder))
            {
                Context.Add(ContextKeys.CreatedOrder, order);
            }

            Context.TryGetValue(ContextKeys.CreatedIncompleteOrders, out IList<Order> createdOrders);
            createdOrders ??= new List<Order>();
            createdOrders.Add(order);
            Context.Set(createdOrders, ContextKeys.CreatedIncompleteOrders);
        }

        [Given(@"an incomplete order with catalogue items exists")]
        public void GivenAnIncompleteOrderWithCatalogueItemsExists()
        {
            GivenAnIncompleteOrderExists();
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.CatalogueSolutionsViewed = 1;

            var serviceRecipient = ServiceRecipient.Generate(order.OrderId, order.OrganisationOdsCode);
            order.ServiceRecipientsViewed = 1;
            serviceRecipient.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedServiceRecipient, serviceRecipient);

            order.Update(Test.OrdapiConnectionString);
            var orderItem = OrderItem.GenerateOrderItemWithFlatPricedVariableOnDemand(order);
            orderItem.Create(Test.OrdapiConnectionString);
            Context.Add(ContextKeys.CreatedOrderItem, orderItem);
        }

        [Given(@"my organisation has one or more orders")]
        public void GivenOneOrMoreOrdersExist()
        {
            GivenACompleteOrderExists();
            GivenACompleteOrderExists();
            GivenAnIncompleteOrderExists();
        }

        [Given(@"a complete order exists")]
        [Given(@"an order is completed")]
        [Given(@"a User has completed an Order")]
        public void GivenACompleteOrderExists()
        {
            var orgAddress = Address.Generate();
            orgAddress.Create(Test.OrdapiConnectionString);
            var orgContact = Contact.Generate();
            orgContact.Create(Test.OrdapiConnectionString);

            var supplierAddress = Address.Generate();
            supplierAddress.Create(Test.OrdapiConnectionString);
            var supplierContact = Contact.Generate();
            supplierContact.Create(Test.OrdapiConnectionString);

            if (!Context.ContainsKey(ContextKeys.Organisation))
            {
                Context.Add(ContextKeys.Organisation, new Organisation().RetrieveRandomOrganisationWithNoUsers(Test.IsapiConnectionString));
            }

            var organisation = (Organisation)Context[ContextKeys.Organisation];

            var order = Order.Generate(organisation);
            order.OrganisationAddressId = orgAddress.AddressId;
            order.OrganisationBillingAddressId = orgAddress.AddressId;
            order.OrganisationContactId = orgContact.ContactId;
            order.SupplierAddressId = supplierAddress.AddressId;
            order.SupplierContactId = supplierContact.ContactId;
            order.SupplierId = 100000;
            order.SupplierName = "Really Kool Corporation";

            var faker = new Faker();
            order.CommencementDate = faker.Date.Future().Date;
            var dateCompleted = faker.Date.Past().Date;
            order.DateCompleted = dateCompleted;
            order.LastUpdated = dateCompleted;

            var serviceRecipient = ServiceRecipient.Generate(order.OrderId, order.OrganisationOdsCode);

            order.CatalogueSolutionsViewed = 1;
            order.ServiceRecipientsViewed = 1;
            order.AdditionalServicesViewed = 1;
            order.AssociatedServicesViewed = 1;
            order.FundingSourceOnlyGMS = 1;

            const int completed = 1;
            order.OrderStatusId = completed;

            order.Create(Test.OrdapiConnectionString);
            if (!Context.ContainsKey(ContextKeys.CreatedOrder))
            {
                Context.Add(ContextKeys.CreatedOrder, order);
            }

            var orderItem = OrderItem.GenerateOrderItemWithFlatPricedVariableOnDemand(order);
            orderItem.LastUpdated = dateCompleted;
            orderItem.Create(Test.OrdapiConnectionString);
            serviceRecipient.Create(Test.OrdapiConnectionString);

            Context.TryGetValue(ContextKeys.CreatedCompletedOrders, out IList<Order> createdOrders);
            createdOrders ??= new List<Order>();
            createdOrders.Add(order);
            Context.Set(createdOrders, ContextKeys.CreatedCompletedOrders);
        }

        [StepDefinition(@"the Order Form for the existing order is presented")]
        public void WhenTheOrderFormForTheExistingOrderIsPresented()
        {
            Test.Driver.Navigate().Refresh();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrganisationsOrdersDashboard.SelectExistingOrder(((Order)Context[ContextKeys.CreatedOrder]).OrderId);
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
        [Then(@"they are presented with the Service Recipients saved in the Order")]
        [Then(@"they can select one Associated Service to add")]
        public void ThenTheyCanSelectOneRadioButton()
        {
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
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
        public void ThenOnlyThePublishedAdditionalServicesAreAvailableForSelection(string itemType)
        {
            var itemTypeId = ConvertType(itemType);

            var publishedItems = SupplierInfo.GetPublishedCatalogueItems(Test.BapiConnectionString, ((Order)Context[ContextKeys.CreatedOrder]).SupplierId.Value.ToString(), itemTypeId);

            var displayedItems = Test.Pages.OrderForm.GetRadioButtonText();

            displayedItems.Should().BeEquivalentTo(publishedItems);
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

        public void CreateUser(UserType userType)
        {
            if (!Context.ContainsKey(ContextKeys.Organisation))
            {
                Context.Add(ContextKeys.Organisation, new Organisation().RetrieveRandomOrganisationWithNoUsers(Test.IsapiConnectionString));
            }

            var organisation = (Organisation)Context[ContextKeys.Organisation];

            var user = new User() { UserType = userType }.GenerateRandomUser(organisation.OrganisationId);
            user.Create(Test.IsapiConnectionString);

            Context.Add(ContextKeys.User, user);
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
