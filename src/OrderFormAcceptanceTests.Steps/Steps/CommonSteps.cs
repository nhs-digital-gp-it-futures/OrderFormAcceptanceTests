using Bogus;
using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class CommonSteps : TestBase
    {
        public CommonSteps(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Given(@"that a buyer user has logged in")]
        public void GivenThatABuyerUserHasLoggedIn()
        {
            Test.Pages.Homepage.ClickLoginButton();
            var user = (User)EnvironmentVariables.User(UserType.Buyer);
            Test.Pages.Authentication.Login(user);
        }

        [Given(@"the User has chosen to manage a new Order Form")]
        public void GivenTheUserHasChosenToManageANewOrderForm()
        {
            GivenThatABuyerUserHasLoggedIn();
            Test.Pages.Homepage.ClickOrderTile();
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
            //clear fields
            //var listOfTextAreas = Test.Driver.FindElements(By.TagName("textarea"));
            var listOfInputs = Test.Driver.FindElements(By.ClassName("nhsuk-input"));
            foreach (var element in listOfInputs)
            {
                element.Clear();
            }
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
        public void DoNothing()
        {
            //do nothing
        }

        [Given(@"an incomplete order exists")]
        public void GivenAnIncompleteOrderExists()
        {
            var orgAddress = new Address().Generate();
            orgAddress.Create(Test.ConnectionString);
            Context.Add("CreatedAddress", orgAddress);
            var orgContact = new Contact().Generate();
            orgContact.Create(Test.ConnectionString);
            Context.Add("CreatedContact", orgContact);

            var supplierAddress = new Address().Generate();
            supplierAddress.Create(Test.ConnectionString);
            Context.Add("CreatedSupplierAddress", supplierAddress);
            var supplierContact = new Contact().Generate();
            supplierContact.Create(Test.ConnectionString);
            Context.Add("CreatedSupplierContact", supplierContact);

            var order = new Order().Generate();
            order.OrganisationAddressId = orgAddress.AddressId;
            order.OrganisationBillingAddressId = orgAddress.AddressId;
            order.OrganisationContactId = orgContact.ContactId;
            order.SupplierAddressId = supplierAddress.AddressId;
            order.SupplierContactId = supplierContact.ContactId;
            order.SupplierId = 100000;
            order.SupplierName = "Really Kool Corporation";

            order.CommencementDate = new Faker().Date.Future().Date;

            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, order.OrganisationOdsCode);
            order.ServiceRecipientsViewed = 1;

            order.Create(Test.ConnectionString);
            Context.Add("CreatedOrder", order);
            serviceRecipient.Create(Test.ConnectionString);
            Context.Add("CreatedServiceRecipient", serviceRecipient);

            Context.TryGetValue(ContextKeys.CreatedIncompleteOrders, out IList<Order> createdOrders);
            createdOrders ??= new List<Order>();
            createdOrders.Add(order);
            Context.Set(createdOrders, ContextKeys.CreatedIncompleteOrders);
        }

        [Given(@"an incomplete order with catalogue items exists")]
        public void GivenAnIncompleteOrderWithCatalogueItemsExists()
        {
            GivenAnIncompleteOrderExists();
            var order = (Order)Context["CreatedOrder"];
            order.CatalogueSolutionsViewed = 1;
            order.Update(Test.ConnectionString);
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand(order);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
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
        public void GivenACompleteOrderExists()
        {
            var orgAddress = new Address().Generate();
            orgAddress.Create(Test.ConnectionString);
            var orgContact = new Contact().Generate();
            orgContact.Create(Test.ConnectionString);

            var supplierAddress = new Address().Generate();
            supplierAddress.Create(Test.ConnectionString);
            var supplierContact = new Contact().Generate();
            supplierContact.Create(Test.ConnectionString);

            var order = new Order().Generate();
            order.OrganisationAddressId = orgAddress.AddressId;
            order.OrganisationBillingAddressId = orgAddress.AddressId;
            order.OrganisationContactId = orgContact.ContactId;
            order.SupplierAddressId = supplierAddress.AddressId;
            order.SupplierContactId = supplierContact.ContactId;
            order.SupplierId = 100000;
            order.SupplierName = "Really Kool Corporation";

            order.CommencementDate = new Faker().Date.Future().Date;
            var dateCompleted = new Faker().Date.Past().Date;
            order.DateCompleted = dateCompleted;
            order.LastUpdated = dateCompleted;

            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, order.OrganisationOdsCode);

            order.CatalogueSolutionsViewed = 1;
            order.ServiceRecipientsViewed = 1;
            order.AdditionalServicesViewed = 1;
            order.AssociatedServicesViewed = 1;
            order.FundingSourceOnlyGMS = 1;

            const int completed = 1;
            order.OrderStatusId = completed;

            order.Create(Test.ConnectionString);

            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand(order);
            orderItem.LastUpdated = dateCompleted;
            orderItem.Create(Test.ConnectionString);
            serviceRecipient.Create(Test.ConnectionString);

            Context.TryGetValue(ContextKeys.CreatedCompletedOrders, out IList<Order> createdOrders);
            createdOrders ??= new List<Order>();
            createdOrders.Add(order);
            Context.Set(createdOrders, ContextKeys.CreatedCompletedOrders);
        }

        [StepDefinition(@"the Order Form for the existing order is presented")]
        public void WhenTheOrderFormForTheExistingOrderIsPresented()
        {
            GivenThatABuyerUserHasLoggedIn();
            Test.Pages.Homepage.ClickOrderTile();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrganisationsOrdersDashboard.SelectExistingOrder(((Order)Context["CreatedOrder"]).OrderId);
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

        [Then(@"they can select a price for the Associated Service")]
        [Then(@"they can select one Catalogue Solution to add")]
        [Then(@"they can select a price for the Catalogue Solution")]
        [Then(@"they are presented with the Service Recipients saved in the Order")]
        [Then(@"they can select one Associated Service to add")]
        public void ThenTheyCanSelectOneRadioButton()
        {
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
        }

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

        public void ContinueAndWaitForRadioButtons()
        {
            WhenTheyChooseToContinue();
            ThenTheyCanSelectOneRadioButton();
        }

        public void AssertListOfStringsIsInAscendingOrder(IEnumerable<string> stringList)
        {
            var hexList = stringList
                .Select(s => Encoding.UTF8.GetBytes(s)) // Convert to byte[]
                .Select(h => BitConverter.ToString(h)) // convert byte[] to hex string
                .Select(r => r.Replace("-", "")) // remove any '-' characters
                .ToList();
            hexList.Should().BeInAscendingOrder();
        }
    }
}