namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Domain.Users;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    public class Login_Authorisation : TestBase
    {
        public Login_Authorisation(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Given(@"that a User is not authenticated and the user logs in using the 'login' function")]
        public void GivenThatAUserIsNotAuthenticatedAndTheUserLogsInUsingTheFunction()
        {
            Test.Pages.Homepage.ClickLoginButton();
        }

        [Given(@"that a User is not authenticated and the user selects the order form tile")]
        public void GivenThatAUserIsNotAuthenticatedAndTheUserSelectsTheOrderFormTile()
        {
            Test.Pages.Homepage.ClickOrderTile();
        }

        [Given(@"the User is prompted to login")]
        public void GivenTheUserIsPromptedToLogin()
        {
            Test.Pages.Authentication.PageDisplayed();
        }

        [When(@"the User is a Buyer User")]
        public async Task WhenTheUserIsABuyerUser()
        {
            await new CommonSteps(Test, Context).CreateUser(UserType.Authority);
        }

        [When(@"the User is not a Buyer User")]
        public async Task WhenTheUserIsNotABuyerUser()
        {
            await new CommonSteps(Test, Context).CreateUser(UserType.Authority);
        }

        [Then(@"the User will be logged in")]
        public void ThenTheBuyerWillBeLoggedIn()
        {
            var user = (User)Context[ContextKeys.User];
            Test.Pages.Authentication.Login(user.UserName, UsersHelper.GenericTestPassword());
        }

        [Then(@"the Buyer will be able to access the Order Form feature without having to authenticate again")]
        public void ThenTheBuyerWillBeAbleToAccessTheOrderFormFeatureWithoutHavingToAuthenticateAgain()
        {
            Test.Pages.Homepage.ClickOrderTile();
        }

        [Then(@"will be taken to the Order Form Feature")]
        public void ThenWillBeTakenToTheOrderFormFeature()
        {
            Test.Driver.Url.Should().Contain("/order/");
        }

        [Then(@"the User will be informed they cannot access that feature")]
        public void ThenTheUserWillBeInformedTheyCannotAccessThatFeature()
        {
            Test.Pages.OrderForm.ErrorTitle().Should().ContainEquivalentOf("not logged in");
        }

        [Then(@"the Public Browse homepage is presented")]
        public void ThenThePublicBrowseHomepageIsPresented()
        {
            Test.Pages.Homepage.PageDisplayed();
        }
    }
}
