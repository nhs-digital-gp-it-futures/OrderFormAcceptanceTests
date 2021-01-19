﻿using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public class Authentication : PageAction
    {
        public Authentication(IWebDriver driver) : base(driver)
        {
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            Driver.FindElement(Objects.Pages.Login.LoginButton).Click();
        }

        private void EnterPassword(string password)
        {
            Driver.FindElement(Objects.Pages.Login.Password).Click();
            Driver.FindElement(Objects.Pages.Login.Password).SendKeys(password);
        }

        private void EnterUsername(string username)
        {
            Driver.WaitForJsToComplete(Wait);
            Wait.Until(d => d.FindElements(Objects.Pages.Login.Username).Count == 1);
            var usernameInput = Driver.FindElement(Objects.Pages.Login.Username);
            usernameInput.Click();

            // Workaround for seleniums poor Clear() method
            usernameInput.SendKeys(Keys.Control + "a");
            usernameInput.SendKeys(Keys.Delete);

            Driver.EnterTextViaJs(Wait, Objects.Pages.Login.Username, username);
            Wait.Until(d => d.FindElement(Objects.Pages.Login.Username).GetAttribute("value") != "");
        }

        public void PageDisplayed()
        {
            Wait.Until(s => s.FindElements(Objects.Pages.Login.Username).Count == 1);
        }
    }
}
