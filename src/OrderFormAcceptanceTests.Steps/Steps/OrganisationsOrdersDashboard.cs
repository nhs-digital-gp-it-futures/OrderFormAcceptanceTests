namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Collections.Generic;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData.Models;
    using TechTalk.SpecFlow;

    [Binding]
    internal class OrganisationsOrdersDashboard : TestBase
    {
        public OrganisationsOrdersDashboard(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Then(@"the page displays who is logged in and the primary organisation name")]
        public void ThenThePageDisplaysWhoIsLoggedInAndThePrimaryOrganisationName()
        {
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
            Test.Pages.OrderForm.LoggedInDisplayNameIsDisplayed().Should().BeTrue();
        }

        [StepDefinition(@"the Organisation Orders Dashboard is displayed")]
        public void OrganisationOrdersDashboardDisplayed()
        {
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
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
            Test.Driver.Navigate().Refresh();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"there is a list of my Organisation's Orders")]
        public void ThenThereIsAListOfMyOrganisationSOrders()
        {
            var numberOfOrdersDisplayed = Test.Pages.OrganisationsOrdersDashboard.GetNumberOfOrdersDisplayed();
            (numberOfOrdersDisplayed > 0).Should().BeTrue();
            Context.Add(ContextKeys.NumberOfOrdersDisplayed, numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Call Off Agreement ID")]
        public void ThenEachItemIncludesTheCallOffAgreementId()
        {
            var numberOfOrdersDisplayed = (int)Context[ContextKeys.NumberOfOrdersDisplayed];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCallOffAgreementIds().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Order Description")]
        public void ThenEachItemIncludesTheOrderDescription()
        {
            var numberOfOrdersDisplayed = (int)Context[ContextKeys.NumberOfOrdersDisplayed];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfDescriptions().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Display Name of the User who made most recent edit")]
        public void ThenEachItemIncludesTheDisplayNameOfTheUserWhoMadeMostRecentEdit()
        {
            var numberOfOrdersDisplayed = (int)Context[ContextKeys.NumberOfOrdersDisplayed];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfLastUpdatedBys().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date of the most recent edit")]
        public void ThenEachItemIncludesTheDateOfTheMostRecentEdit()
        {
            var numberOfOrdersDisplayed = Test.Pages.OrganisationsOrdersDashboard.GetNumberOfIncompleteOrders();
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfLastUpdatedDates().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date it was created")]
        public void ThenEachItemIncludesTheDateItWasCreated()
        {
            var numberOfOrdersDisplayed = (int)Context[ContextKeys.NumberOfOrdersDisplayed];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCreatedDates().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"there is a table titled Incomplete orders")]
        public void ThenThereIsATableTitledIncompleteOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.IncompleteOrdersTableDisplayed().Should().BeTrue();
        }

        [Then(@"there is a table titled Completed orders")]
        public void ThenThereIsATableTitledCompletedOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.CompletedOrdersTableDisplayed().Should().BeTrue();
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

        [When(@"my organisation's orders dashboard is presented")]
        [StepDefinition(@"the order dashboard is presented")]
        public void WhenTheOrderDashboardIsPresented()
        {
            Test.Driver.Navigate().Refresh();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"there is a control I can use to view the completed Version of the order summary")]
        public void ThenThereIsAControlICanUseToViewTheCompletedVersionOfTheOrderSummary()
        {
            var callOffId = Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId.ToString();
            var (tagName, text, url) = Test.Pages.OrganisationsOrdersDashboard.GetOrderSummaryLink(callOffId);
            tagName.Should().BeEquivalentTo("a");
            text.Should().ContainEquivalentOf(callOffId);
            url.Should().ContainEquivalentOf("summary");
        }

        [Then(@"there isn't a last updated column in the completed orders table")]
        public void ThenThereIsNoLastUpdatedColumnInTheCompletedOrdersTable()
        {
            Test.Pages.OrganisationsOrdersDashboard.CompletedOrdersTableHasNoLastUpdateDate();
        }

        [Then(@"the date that the order was completed is displayed in the date completed column")]
        public void ThenTheDateThatTheOrderWasCompletedIsDisplayedInTheDateCompletedColumn()
        {
            var completedOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();

            foreach (var order in completedOrders)
            {
                order.Completed.Should().NotBeNull();
            }
        }

        [Then(@"the completed orders are presented in descending order by the date completed")]
        public void ThenCompletedOrdersPresentedIDescendingOrderByDateCompleted()
        {
            IList<OrderTableItem> orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();
            orders.Should().NotBeEmpty();
            orders.Should().BeInDescendingOrder(o => o.Completed);
        }

        [Then(@"there is a date completed column")]
        public void ThenThereIsADateCompletedColumn()
        {
            Test.Pages.OrganisationsOrdersDashboard.CompletedOrdersTableHasDateCompleted()
                .Should().BeTrue();
        }

        [Then(@"the orders are separated into groups")]
        public void ThenTheOrdersAreSeparatedIntoGroups()
        {
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfIncompleteOrders().Should().BeGreaterOrEqualTo(1);
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCompleteOrders().Should().BeGreaterOrEqualTo(1);
        }
    }
}
