﻿using FluentAssertions;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
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
            Test.Pages.OrderForm.CreateNewOrder();
        }

        [Then(@"the new Order is presented")]
        [When(@"the Order Form is presented")]
        public void ThenTheNewOrderIsPresented()
        {
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
        }

        [Given(@"mandatory data are missing")]
        [Then(@".* section is not saved")]
        public void GivenMandatoryDataAreMissing()
        {
            //do nothing
        }

    }
}
