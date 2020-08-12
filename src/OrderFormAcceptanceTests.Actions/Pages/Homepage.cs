using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public sealed class Homepage : PageAction
    {
        public Homepage(IWebDriver driver) : base(driver)
        {
        }

        public void PageDisplayed()
        {
            Wait.Until(s => s.FindElement(Pages.Homepage.Title).Displayed);
        }

        public void ClickLoginButton()
        {
            Wait.Until(s => s.FindElements(Pages.Homepage.LoginLogoutLink).Count > 0);
            Driver.FindElement(Pages.Homepage.LoginLogoutLink).Click();
            Wait.Until(s => s.FindElements(Pages.Homepage.LoginLogoutLink).Count == 0);
        }

        public bool LoginLogoutLinkText(string expectedValue)
        {
            Wait.Until(s => s.FindElements(Pages.Homepage.LoginLogoutLink).Count > 0);
            Wait.Until(s => s.FindElement(Pages.Homepage.LoginLogoutLink).Text.Contains(expectedValue, StringComparison.OrdinalIgnoreCase));
            return true;
        }

        public void LogOut()
        {
            if (LoginLogoutLinkText("Log out"))
            {
                Driver.FindElement(Pages.Homepage.LoginLogoutLink).Click();
            }
            else
            {
                throw new WebDriverException("Log out text incorrect");
            }
        }

        public void ClickOrderTile()
        {
            Driver.FindElement(Pages.Homepage.OrderTile).Click();
        }
    }
}
