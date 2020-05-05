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

        [Given(@"that a User is not authenticated and the user  logs in using the '(.*)' function")]
        public void GivenThatAUserIsNotAuthenticatedAndTheUserLogsInUsingTheFunction(string p0)
        {
            Context.Pending();
        }
        
        [Given(@"that a User is not authenticated and the user selects the order form tile")]
        public void GivenThatAUserIsNotAuthenticatedAndTheUserSelectsTheOrderFormTile()
        {
            Context.Pending();
        }
        
        [Given(@"the User is prompted to login")]
        public void GivenTheUserIsPromptedToLogin()
        {
            Context.Pending();
        }
        
        [When(@"the User is a Buyer User")]
        public void WhenTheUserIsABuyerUser()
        {
            Context.Pending();
        }
        
        [When(@"the User is not a Buyer User")]
        public void WhenTheUserIsNotABuyerUser()
        {
            Context.Pending();
        }
        
        [Then(@"the Buyer will be logged in")]
        public void ThenTheBuyerWillBeLoggedIn()
        {
            Context.Pending();
        }
        
        [Then(@"the Buyer will be able to access the Order Form feature without having to authenticate again")]
        public void ThenTheBuyerWillBeAbleToAccessTheOrderFormFeatureWithoutHavingToAuthenticateAgain()
        {
            Context.Pending();
        }
        
        [Then(@"will be taken to the Order Form Feature")]
        public void ThenWillBeTakenToTheOrderFormFeature()
        {
            Context.Pending();
        }
        
        [Then(@"the User will be informed they cannot access that feature")]
        public void ThenTheUserWillBeInformedTheyCannotAccessThatFeature()
        {
            Context.Pending();
        }
    }
}
