using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Steps.Utils;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class Dashboard : TestBase
    {
        public Dashboard(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the page displays who is logged in and the primary organisation name")]
        public void ThenThePageDisplaysWhoIsLoggedInAndThePrimaryOrganisationName()
        {
            Test.Pages.Dashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrderForm.LoggedInDisplayNameIsDisplayed().Should().BeTrue();
        }

        [Then(@"the new order page displays the logged in display name and organisation name")]
        public void ThenTheNewOrderPageDisplaysTheLoggedInDisplayNameAndOrganisationName()
        {
            Test.Pages.Dashboard.CreateNewOrder();
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.LoggedInDisplayNameIsDisplayed().Should().BeTrue();
            Test.Driver.Navigate().Back();
            Test.Pages.Dashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"the new order page displays the standard Public browse footer")]
        public void ThenTheNewOrderPageDisplaysTheStandardPublicBrowseFooter()
        {
            Test.Pages.Dashboard.CreateNewOrder();
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.FooterDisplayed().Should().BeTrue();
            Test.Driver.Navigate().Back();
            Test.Pages.Dashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"the new order page displays the standard Public browse header")]
        public void ThenTheNewOrderPageDisplaysTheStandardPublicBrowseHeader()
        {
            Test.Pages.Dashboard.CreateNewOrder();
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.HeaderDisplayed().Should().BeTrue();
            Test.Driver.Navigate().Back();
            Test.Pages.Dashboard.WaitForDashboardToBeDisplayed();
        }

        [When(@"the User is presented with the Organisation's Orders dashboard")]
        public void WhenTheUserIsPresentedWithTheOrganisationSOrdersDashboard()
        {
            new CommonSteps(Test, Context).GivenThatABuyerUserHasLoggedIn();
            Test.Pages.Homepage.ClickOrderTile();
            Test.Pages.Dashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"there is a list of my Organisation's Orders")]
        public void ThenThereIsAListOfMyOrganisationSOrders()
        {
            var NumberOfOrdersDisplayed = Test.Pages.Dashboard.GetNumberOfOrdersDisplayed();
            (NumberOfOrdersDisplayed > 0).Should().BeTrue();
            Context.Add("NumberOfOrdersDisplayed", NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Call Off Agreement ID")]
        public void ThenEachItemIncludesTheCallOffAgreementID()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.Dashboard.GetNumberOfCallOffAgreementIds().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Order Description")]
        public void ThenEachItemIncludesTheOrderDescription()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.Dashboard.GetNumberOfDescriptions().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Display Name of the User who made most recent edit")]
        public void ThenEachItemIncludesTheDisplayNameOfTheUserWhoMadeMostRecentEdit()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.Dashboard.GetNumberOfLastUpdatedBys().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date of the most recent edit")]
        public void ThenEachItemIncludesTheDateOfTheMostRecentEdit()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.Dashboard.GetNumberOfLastUpdatedDates().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date it was created")]
        public void ThenEachItemIncludesTheDateItWasCreated()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.Dashboard.GetNumberOfCreatedDates().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"there is a table titled Unsubmitted orders")]
        public void ThenThereIsATableTitledUnsubmittedOrders()
        {
            Test.Pages.Dashboard.UnsubmittedOrdersTableDisplayed().Should().BeTrue();
        }

        [Then(@"there is a table titled Submitted orders")]
        public void ThenThereIsATableTitledSubmittedOrders()
        {
            Test.Pages.Dashboard.SubmittedOrdersTableDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to nominate an organisation to buy on my behalf")]
        public void ThenThereIsAControlToNominateAnOrganisationToBuyOnMyBehalf()
        {
            Test.Pages.Dashboard.NominateProxyDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to go back to the homepage")]
        public void ThenThereIsAControlToGoBackToTheHomepage()
        {
            Test.Pages.Dashboard.BackLinkDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to create a new order")]
        public void ThenThereIsAControlToCreateANewOrder()
        {
            Test.Pages.Dashboard.CreateNewOrderButtonDisplayed().Should().BeTrue();
        }

        [Then(@"the Organisation's Orders dashboard contains the standard Public browse footer")]
        public void ThenTheOrganisationSOrdersDashboardContainsTheStandardPublicBrowseFooter()
        {
            Test.Pages.Dashboard.FooterDisplayed().Should().BeTrue();
        }

        [Then(@"the Organisation's Orders dashboard contains the standard Public browse header including the Beta banner")]
        public void ThenTheOrganisationSOrdersDashboardContainsTheStandardPublicBrowseHeaderIncludingTheBetaBanner()
        {
            Test.Pages.Dashboard.HeaderDisplayed().Should().BeTrue();
            Test.Pages.Dashboard.BetaBannerDisplayed().Should().BeTrue();
        }

        [When(@"the User choose to go back to the homepage")]
        public void WhenTheUserChooseToGoBackToTheHomepage()
        {
            Test.Pages.Dashboard.ClickBackLink();
        }

    }
}
