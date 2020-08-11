﻿using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    internal class OrganisationsOrdersDashboard : TestBase
    {
        public OrganisationsOrdersDashboard(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the Organisation Orders Dashboard is displayed")]
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
            var numberOfOrdersDisplayed = Test.Pages.OrganisationsOrdersDashboard.GetNumberOfOrdersDisplayed();
            (numberOfOrdersDisplayed > 0).Should().BeTrue();
            Context.Add("NumberOfOrdersDisplayed", numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Call Off Agreement ID")]
        public void ThenEachItemIncludesTheCallOffAgreementId()
        {
            var numberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCallOffAgreementIds().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Order Description")]
        public void ThenEachItemIncludesTheOrderDescription()
        {
            var numberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfDescriptions().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the Display Name of the User who made most recent edit")]
        public void ThenEachItemIncludesTheDisplayNameOfTheUserWhoMadeMostRecentEdit()
        {
            var numberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfLastUpdatedBys().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date of the most recent edit")]
        public void ThenEachItemIncludesTheDateOfTheMostRecentEdit()
        {
            var numberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfLastUpdatedDates().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"each item includes the date it was created")]
        public void ThenEachItemIncludesTheDateItWasCreated()
        {
            var numberOfOrdersDisplayed = (int)Context["NumberOfOrdersDisplayed"];
            Test.Pages.OrganisationsOrdersDashboard.GetNumberOfCreatedDates().Should().Be(numberOfOrdersDisplayed);
        }

        [Then(@"there is a table titled Incomplete orders")]
        public void ThenThereIsATableTitledIncompleteOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.IncompleteOrdersTableDisplayed().Should().BeTrue();
        }

        [Then(@"there is a table titled Submitted orders")]
        public void ThenThereIsATableTitledCompletedOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.SubmittedOrdersTableDisplayed().Should().BeTrue();
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
            var order = (Order)Context["CreatedOrder"];
            (Test.Driver.FindElements(By.LinkText(order.OrderId)).Count == 1).Should().BeTrue();
        }

        [StepDefinition(@"the item is displayed as an Incomplete Order")]
        public void ThenTheItemIsDisplayedAsAnIncompleteOrder()
        {
            var createdOrder = (Order)Context["CreatedOrder"];
            var orders = Test.Pages.OrganisationsOrdersDashboard.GetListOfIncompleteOrders();
            var found = false;

            foreach (Order order in orders)
            {
                if (order.OrderId == createdOrder.OrderId)
                {
                    found = true;
                    Context.Add("OrderFromUI", order);
                    break;
                }
            }
            found.Should().BeTrue();
        }

        [StepDefinition(@"the item includes the Call Off Agreement ID")]
        public void WhenTheItemIncludesTheCallOffAgreementId()
        {
            var createdOrder = (Order)Context["CreatedOrder"];
            var orderFromUi = (Order)Context["OrderFromUI"];
            orderFromUi.OrderId.Should().BeEquivalentTo(createdOrder.OrderId);
        }

        [Then(@"the item includes the Order Description")]
        public void ThenTheItemIncludesTheOrderDescription()
        {
            var createdOrder = (Order)Context["CreatedOrder"];
            var orderFromUi = (Order)Context["OrderFromUI"];
            orderFromUi.Description.Should().BeEquivalentTo(createdOrder.Description);
        }

        [Then(@"the item includes the Display Name of the User who made most recent edit")]
        public void ThenTheItemIncludesTheDisplayNameOfTheUserWhoMadeMostRecentEdit()
        {
            var createdOrder = (Order)Context["CreatedOrder"];
            var orderFromUi = (Order)Context["OrderFromUI"];
            orderFromUi.LastUpdatedByName.Should().BeEquivalentTo(createdOrder.LastUpdatedByName);
        }

        [Then(@"the item includes the date of the most recent edit")]
        public void ThenTheItemIncludesTheDateOfTheMostRecentEdit()
        {
            var createdOrder = (Order)Context["CreatedOrder"];
            var orderFromUi = (Order)Context["OrderFromUI"];
            orderFromUi.LastUpdated.Should().BeSameDateAs(createdOrder.LastUpdated);
        }
        [Then(@"the item includes the date it was created")]
        public void ThenTheItemIncludesTheDateItWasCreated()
        {
            var createdOrder = (Order)Context["CreatedOrder"];
            var orderFromUi = (Order)Context["OrderFromUI"];
            orderFromUi.Created.Should().BeSameDateAs(createdOrder.Created);
        }

    }
}
