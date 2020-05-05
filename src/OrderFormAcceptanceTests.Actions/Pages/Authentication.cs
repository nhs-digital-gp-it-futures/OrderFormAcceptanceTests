using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

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
            Wait.Until(d => d.FindElements(Pages.Login.Username).Count > 0);
            Driver.FindElement(Pages.Login.Username).Click();
            Driver.FindElement(Pages.Login.Username).SendKeys(username);
        }

        public void PageDisplayed()
        {
            Wait.Until(s => s.FindElements(Pages.Login.Username).Count == 1);
        }
    }
}
