namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using TechTalk.SpecFlow;

    [Binding]
    internal class OrganisationsOrdersDashboard : TestBase
    {
        public OrganisationsOrdersDashboard(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [StepDefinition(@"the Organisation Orders Dashboard is displayed")]
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

        [Then(@"the saved Order is displayed as an item on the Organisation's Orders dashboard")]
        public void ThenTheSavedOrderIsDisplayedAsAnItemOnTheOrganisationSOrdersDashboard()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            (Test.Driver.FindElements(By.LinkText(order.OrderId)).Count == 1).Should().BeTrue();
        }

        [StepDefinition(@"the item is displayed as an Incomplete Order")]
        public void ThenTheItemIsDisplayedAsAnIncompleteOrder()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfIncompleteOrders();
            var found = false;

            foreach (Order order in orders)
            {
                if (order.OrderId == createdOrder.OrderId)
                {
                    found = true;
                    Context.Add(ContextKeys.OrderFromUI, order);
                    break;
                }
            }

            found.Should().BeTrue();
        }

        [StepDefinition(@"the Order is in the 'Completed Orders' table")]
        public void ThenTheItemIsDisplayedAsACompletedOrder()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();
            bool found = false;
            foreach (Order order in orders)
            {
                if (order.OrderId == createdOrder.OrderId)
                {
                    found = true;
                    Context.Add(ContextKeys.OrderFromUI, order);
                    break;
                }
            }

            found.Should().BeTrue();
        }

        [StepDefinition(@"there is an indication that the Order has been processed automatically")]
        public void WhenTheItemHasBeenAutomaticallyProcessed()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.OrderId.Should().BeEquivalentTo(createdOrder.OrderId);
            orderFromUi.FundingSourceOnlyGMS.Should().Be(1);
        }

        [StepDefinition(@"there is an indication that the Order has not been processed automatically")]
        public void WhenTheItemHasNotBeenAutomaticallyProcessed()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.OrderId.Should().BeEquivalentTo(createdOrder.OrderId);
            var notProcessed = orderFromUi.FundingSourceOnlyGMS == null || orderFromUi.FundingSourceOnlyGMS == 0;
            notProcessed.Should().BeTrue();
        }

        [StepDefinition(@"the item includes the Call Off Agreement ID")]
        public void WhenTheItemIncludesTheCallOffAgreementId()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.OrderId.Should().BeEquivalentTo(createdOrder.OrderId);
        }

        [Then(@"the item includes the Order Description")]
        public void ThenTheItemIncludesTheOrderDescription()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.Description.Should().BeEquivalentTo(createdOrder.Description);
        }

        [Then(@"the item includes the Display Name of the User who made most recent edit")]
        public void ThenTheItemIncludesTheDisplayNameOfTheUserWhoMadeMostRecentEdit()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.LastUpdatedByName.Should().BeEquivalentTo(createdOrder.LastUpdatedByName);
        }

        [Then(@"the item includes the date of the most recent edit")]
        public void ThenTheItemIncludesTheDateOfTheMostRecentEdit()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.LastUpdated.Should().BeSameDateAs(createdOrder.LastUpdated);
        }

        [Then(@"the item includes the date it was created")]
        public void ThenTheItemIncludesTheDateItWasCreated()
        {
            var createdOrder = (Order)Context[ContextKeys.CreatedOrder];
            var orderFromUi = (Order)Context[ContextKeys.OrderFromUI];
            orderFromUi.Created.Should().BeSameDateAs(createdOrder.Created);
        }

        [When(@"my organisation's orders dashboard is presented")]
        [StepDefinition(@"the order dashboard is presented")]
        public void WhenTheOrderDashboardIsPresented()
        {
            Test.Driver.Navigate().Refresh();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [When(@"it is included in the completed orders table of the organisation's orders dashboard")]
        public void WhenItIsIncludedInTheCompletedOrdersTableOfTheOrganisationsOrdersDashboard()
        {
            WhenTheOrderDashboardIsPresented();

            var order = Context.Get<IList<Order>>(ContextKeys.CreatedCompletedOrders).First();
            var orderLink = Test.Pages.OrganisationsOrdersDashboard.GetOrderSummaryLink(order.OrderId);

            orderLink.Should().NotBeNull();
        }

        [Then(@"there isn't a last updated column in the completed orders table")]
        public void ThenThereIsNoLastUpdatedColumnInTheCompletedOrdersTable()
        {
            Test.Pages.OrganisationsOrdersDashboard.CompletedOrdersTableHasNoLastUpdateDate();
        }

        [Then(@"the completed orders are presented in descending order by the date completed")]
        public void ThenCompletedOrdersPresentedIDescendingOrderByDateCompleted()
        {
            IList<Order> orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();
            orders.Should().NotBeEmpty();
            orders.Should().BeInDescendingOrder(o => o.DateCompleted);
        }

        [Then(@"there is a control I can use to view the completed Version of the order summary")]
        public void ThenThereIsAControlICanUseToViewTheCompletedVersionOfTheOrderSummary()
        {
            var orders = Context.Get<IList<Order>>(ContextKeys.CreatedCompletedOrders);

            foreach (var order in orders)
            {
                var orderId = order.OrderId;
                var (tagName, text, url) = Test.Pages.OrganisationsOrdersDashboard.GetOrderSummaryLink(orderId);

                tagName.Should().Be("a");
                text.Should().Be(orderId);
                url.Should().EndWith($"/order/organisation/{orderId}/summary");
            }
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

        [Then(@"the first group includes orders which are not completed")]
        public void ThenTheFirstGroupIncludesOrdersWhichAreNotCompleted()
        {
            Test.Pages.OrganisationsOrdersDashboard.IncompleteOrdersPrecedesCompletedOrders()
                .Should().BeTrue();

            IEnumerable<string> createdOrders = Context
                .Get<IList<Order>>(ContextKeys.CreatedIncompleteOrders)
                .Select(o => o.OrderId);

            IEnumerable<string> incompleteOrders = Test.Pages.OrganisationsOrdersDashboard
                .GetListOfIncompleteOrders()
                .Select(o => o.OrderId);

            incompleteOrders.Should().IntersectWith(createdOrders);
        }

        [Then(@"the second group includes orders which are completed")]
        public void ThenTheSecondGroupIncludesOrdersWhichAreCompleted()
        {
            Test.Pages.OrganisationsOrdersDashboard.IncompleteOrdersPrecedesCompletedOrders()
                .Should().BeTrue();

            IEnumerable<string> createdOrders = Context
                .Get<IList<Order>>(ContextKeys.CreatedCompletedOrders)
                .Select(o => o.OrderId);

            IEnumerable<string> completedOrders = Test.Pages.OrganisationsOrdersDashboard
                .GetListOfCompletedOrders()
                .Select(o => o.OrderId);

            completedOrders.Should().IntersectWith(createdOrders);
        }

        [Then(@"the date that the order was completed is displayed in the date completed column")]
        public void ThenTheDateThatTheOrderWasCompletedIsDisplayedInTheDateCompletedColumn()
        {
            var createdOrder = Context.Get<IList<Order>>(ContextKeys.CreatedCompletedOrders).First();

            IList<Order> completedOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfCompletedOrders();
            var completedOrder = completedOrders.FirstOrDefault(o => o.OrderId == createdOrder.OrderId);

            completedOrder.Should().NotBeNull();
            completedOrder.DateCompleted.Should().Be(createdOrder.DateCompleted);
        }
    }
}
