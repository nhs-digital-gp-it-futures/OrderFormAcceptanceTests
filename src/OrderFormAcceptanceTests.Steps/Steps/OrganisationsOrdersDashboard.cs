using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Steps.Utils;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class OrganisationsOrdersDashboard : TestBase
    {
        public OrganisationsOrdersDashboard(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the page displays who is logged in and the primary organisation name")]
        public void ThenThePageDisplaysWhoIsLoggedInAndThePrimaryOrganisationName()
        {
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrderForm.LoggedInDisplayNameIsDisplayed().Should().BeTrue();
        }

        [Then(@"the new order page displays the logged in display name and organisation name")]
        public void ThenTheNewOrderPageDisplaysTheLoggedInDisplayNameAndOrganisationName()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrder();
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.LoggedInDisplayNameIsDisplayed().Should().BeTrue();
            Test.Driver.Navigate().Back();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"the new order page displays the standard Public browse footer")]
        public void ThenTheNewOrderPageDisplaysTheStandardPublicBrowseFooter()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrder();
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.FooterDisplayed().Should().BeTrue();
            Test.Driver.Navigate().Back();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"the new order page displays the standard Public browse header")]
        public void ThenTheNewOrderPageDisplaysTheStandardPublicBrowseHeader()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrder();
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.HeaderDisplayed().Should().BeTrue();
            Test.Driver.Navigate().Back();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [When(@"the User is presented with the Organisation's Orders dashboard")]
        public void WhenTheUserIsPresentedWithTheOrganisationSOrdersDashboard()
        {
            new CommonSteps(Test, Context).GivenThatABuyerUserHasLoggedIn();
            Test.Pages.Homepage.ClickOrderTile();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"there is a list of my Organisation's Orders")]
        public void ThenThereIsAListOfMyOrganisationSOrders()
        {
            var NumberOfOrdersDisplayed = Test.Pages.OrganisationsOrdersDashboard.GetNumberOfOrdersDisplayed();
            (NumberOfOrdersDisplayed > 0).Should().BeTrue();
            Context.Add("NumberOfOrdersDisplayed", NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Call Off Agreement ID")]
        public void ThenEachItemIncludesTheCallOffAgreementID()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCallOffAgreementIds().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Order Description")]
        public void ThenEachItemIncludesTheOrderDescription()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfDescriptions().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Display Name of the User who made most recent edit")]
        public void ThenEachItemIncludesTheDisplayNameOfTheUserWhoMadeMostRecentEdit()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfLastUpdatedBys().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date of the most recent edit")]
        public void ThenEachItemIncludesTheDateOfTheMostRecentEdit()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfLastUpdatedDates().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date it was created")]
        public void ThenEachItemIncludesTheDateItWasCreated()
        {
            var NumberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCreatedDates().Should().Be(NumberOfOrdersDisplayed);
        }

        [Then(@"there is a table titled Unsubmitted orders")]
        public void ThenThereIsATableTitledUnsubmittedOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.UnsubmittedOrdersTableDisplayed().Should().BeTrue();
        }

        [Then(@"there is a table titled Submitted orders")]
        public void ThenThereIsATableTitledSubmittedOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.SubmittedOrdersTableDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to nominate an organisation to buy on my behalf")]
        public void ThenThereIsAControlToNominateAnOrganisationToBuyOnMyBehalf()
        {
            Test.Pages.OrganisationsOrdersDashboard.NominateProxyDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to go back to the homepage")]
        public void ThenThereIsAControlToGoBackToTheHomepage()
        {
            Test.Pages.OrganisationsOrdersDashboard.BackLinkDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to create a new order")]
        public void ThenThereIsAControlToCreateANewOrder()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrderButtonDisplayed().Should().BeTrue();
        }

        [Then(@"the Organisation's Orders dashboard contains the standard Public browse footer")]
        public void ThenTheOrganisationSOrdersDashboardContainsTheStandardPublicBrowseFooter()
        {
            Test.Pages.OrganisationsOrdersDashboard.FooterDisplayed().Should().BeTrue();
        }

        [Then(@"the Organisation's Orders dashboard contains the standard Public browse header including the Beta banner")]
        public void ThenTheOrganisationSOrdersDashboardContainsTheStandardPublicBrowseHeaderIncludingTheBetaBanner()
        {
            Test.Pages.OrganisationsOrdersDashboard.HeaderDisplayed().Should().BeTrue();
            Test.Pages.OrganisationsOrdersDashboard.BetaBannerDisplayed().Should().BeTrue();
        }

        [When(@"the User choose to go back to the homepage")]
        public void WhenTheUserChooseToGoBackToTheHomepage()
        {
            Test.Pages.OrganisationsOrdersDashboard.ClickBackLink();
        }

    }
}
