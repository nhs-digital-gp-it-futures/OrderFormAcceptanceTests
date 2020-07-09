using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
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

        [Then(@".* section is not saved")]
        [Then(@"the Catalogue Solution is not saved")]
        [StepDefinition(@"the Catalogue Solution is saved")]
        [Given(@"no Supplier is selected")]
        [Then("the Commencement Date information is not saved")]
        [Given(@"the Call Off Ordering Party is not selected")]
        [Given(@"the User chooses not to add a Catalogue Solution")]
        [Given(@"no Catalogue Solution is selected")]
        [Given(@"no Catalogue Solution price is selected")]
        [Given(@"no Service Recipient is selected")]
        [Given(@"there are no Catalogue Solution items in the order")]
        [StepDefinition(@"there is no Additional Service added to the order")]
        public void DoNothing()
        {
            //do nothing

        }

        [Given(@"an unsubmitted order exists")]
        public void GivenAnUnsubmittedOrderExists()
        {            
            var orgAddress = new TestData.Address().Generate();
            orgAddress.Create(Test.ConnectionString);
            Context.Add("CreatedAddress", orgAddress);
            var orgContact = new Contact().Generate();
            orgContact.Create(Test.ConnectionString);
            Context.Add("CreatedContact", orgContact);

            var supplierAddress = new TestData.Address().Generate();
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

            order.CommencementDate = new Faker().Date.Future();
            order.CommencementDate = new DateTime(order.CommencementDate.Value.Year, order.CommencementDate.Value.Month, order.CommencementDate.Value.Day);

            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, order.OrganisationOdsCode);            
            order.ServiceRecipientsViewed = 1;

            order.Create(Test.ConnectionString);
            Context.Add("CreatedOrder", order);
            serviceRecipient.Create(Test.ConnectionString);
            Context.Add("CreatedServiceRecipient", serviceRecipient);            
        }

        [Given(@"an unsubmited order with catalogue items exists")]
        public void GivenAnUnsubmittedOrderWithCatalogueItemsExists()
		{
            GivenAnUnsubmittedOrderExists();
            var order = (Order)Context["CreatedOrder"];
            order.CatalogueSolutionsViewed = 1;
            order.Update(Test.ConnectionString);
            var orderItem = new OrderItem().GenerateOrderItemWithFlatPricedVariableOnDemand(order);
            orderItem.Create(Test.ConnectionString);
            Context.Add("CreatedOrderItem", orderItem);
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

        [When(@"the User chooses to go back")]
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
    }
}
