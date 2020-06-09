using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
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
        [Given(@"no Supplier is selected")]
        [Then("the Commencement Date information is not saved")]
        [Given(@"the Call Off Ordering Party is not selected")]
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

            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, order.OrganisationOdsCode);            
            order.ServiceRecipientsViewed = 1;

            order.Create(Test.ConnectionString);
            Context.Add("CreatedOrder", order);
            serviceRecipient.Create(Test.ConnectionString);
            Context.Add("CreatedServiceRecipient", serviceRecipient);
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
                
    }
}
