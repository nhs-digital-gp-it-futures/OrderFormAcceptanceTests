using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public class Dashboard : PageAction
    {
        public Dashboard(IWebDriver driver) : base(driver)
        {

        }

        public void WaitForDashboardToBeDisplayed()
        {
            Driver.WaitForJsToComplete(Wait);
            Wait.Until(d => d.FindElements(Pages.OrderFormDashboard.PageTitle).Count == 1);
        }

        public void SelectExistingOrder(string CallOffAgreementId)
        {
            Wait.Until(d => d.FindElements(Pages.OrderFormDashboard.SpecificExistingOrder(CallOffAgreementId)).Count == 1);
            Wait.Until(ElementExtensions.ElementToBeClickable(Pages.OrderFormDashboard.SpecificExistingOrder(CallOffAgreementId)));
            Driver.FindElement(Pages.OrderFormDashboard.SpecificExistingOrder(CallOffAgreementId)).Click();
        }

        public bool CreateNewOrderButtonDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.OrderFormDashboard.CreateOrderButton).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CreateNewOrder()
        {
            CreateNewOrderButtonDisplayed();
            Driver.FindElement(Pages.OrderFormDashboard.CreateOrderButton).Click();
        }

        public int GetNumberOfOrdersDisplayed()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.Orders).Count;
        }

        public int GetNumberOfCallOffAgreementIds()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.GenericExistingOrder).Count;
        }
        public int GetNumberOfDescriptions()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.GenericExistingOrderDescription).Count;
        }
        public int GetNumberOfLastUpdatedBys()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.GenericExistingOrderLastUpdatedBy).Count;
        }
        public int GetNumberOfLastUpdatedDates()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.GenericExistingOrderLastUpdatedDate).Count;
        }
        public int GetNumberOfCreatedDates()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.GenericExistingOrderCreatedDate).Count;
        }
        public bool UnsubmittedOrdersTableDisplayed()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.UnsubmittedOrdersTable).Count == 1;
        }

        public bool SubmittedOrdersTableDisplayed()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.SubmittedOrdersTable).Count == 1;
        }
        public bool NominateProxyDisplayed()
        {
            return Driver.FindElements(Pages.OrderFormDashboard.NominateProxy).Count == 1;
        }
        public bool BackLinkDisplayed()
        {
            return Driver.FindElements(Pages.Common.BackLink).Count == 1;
        }

        public void ClickBackLink()
        {
            Driver.FindElement(Pages.Common.BackLink).Click();
        }

        public bool FooterDisplayed()
        {
            return Driver.FindElements(Pages.Common.Footer).Count == 1;
        }
        public bool HeaderDisplayed()
        {
            return Driver.FindElements(Pages.Common.Header).Count == 1;
        }
        public bool BetaBannerDisplayed()
        {
            return Driver.FindElements(Pages.Common.BetaBanner).Count == 1;
        }
    }
}
