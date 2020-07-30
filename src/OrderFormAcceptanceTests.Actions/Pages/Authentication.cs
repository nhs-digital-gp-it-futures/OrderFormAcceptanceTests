using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public class Authentication : PageAction
    {
        public Authentication(IWebDriver driver) : base(driver)
        {
        }

        public void Login(User user)
        {
            EnterUsername(user.Username);
            EnterPassword(user.Password);
            Driver.FindElement(Pages.Login.LoginButton).Click();
        }

        private void EnterPassword(string password)
        {
            Driver.FindElement(Pages.Login.Password).Click();
            Driver.FindElement(Pages.Login.Password).SendKeys(password);
        }

        private void EnterUsername(string username)
        {
            Driver.WaitForJsToComplete(Wait);
            Wait.Until(d => d.FindElements(Pages.Login.Username).Count == 1);
            Wait.Until(d => d.FindElement(Pages.Login.Username).GetAttribute("value") == "");
            Wait.Until(ElementExtensions.ElementToBeClickable(Pages.Login.Username));
            Driver.FindElement(Pages.Login.Username).Click();
            Driver.EnterTextViaJs(Wait, Pages.Login.Username, username);
            Wait.Until(d => d.FindElement(Pages.Login.Username).GetAttribute("value") != "");
        }

        public void PageDisplayed()
        {
            Wait.Until(s => s.FindElements(Pages.Login.Username).Count == 1);
        }
    }
}
