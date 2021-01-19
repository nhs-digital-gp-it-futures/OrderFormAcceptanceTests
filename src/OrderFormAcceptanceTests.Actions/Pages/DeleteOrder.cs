﻿using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public sealed class DeleteOrder : PageAction
    {
        public DeleteOrder(IWebDriver driver) : base(driver)
        {

        }

        public void ClickDeleteButtonYes()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.DeleteOrder.DeleteButtonYes).Count == 1);
            Driver.FindElement(Objects.Pages.DeleteOrder.DeleteButtonYes).Click();
        }

        public void ClickDeleteButtonNo()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.DeleteOrder.DeleteButtonNo).Count == 1);
            Driver.FindElement(Objects.Pages.DeleteOrder.DeleteButtonNo).Click();
        }
    }
}
