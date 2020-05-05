using FluentAssertions;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public class Login_Authorisation : TestBase
    {
        public Login_Authorisation(UITest test, ScenarioContext context) : base(test, context)
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
        public void WhenTheUserIsABuyerUser()
        {
            Context.Add("User", EnvironmentVariables.User(UserType.Buyer));
        }
        
        [When(@"the User is not a Buyer User")]
        public void WhenTheUserIsNotABuyerUser()
        {
            Context.Add("User", EnvironmentVariables.User(UserType.Authority));            
        }
        
        [Then(@"the Buyer will be logged in")]
        public void ThenTheBuyerWillBeLoggedIn()
        {
            Test.Pages.Authentication.Login((User)Context["User"]);
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
            Test.Pages.OrderForm.ErrorTitle().Should().BeEquivalentTo("You're not authorised to view this page");
        }
    }
}
