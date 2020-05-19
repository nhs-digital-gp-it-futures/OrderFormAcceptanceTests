using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public class OrganisationsOrdersDashboard : PageAction
    {
        public OrganisationsOrdersDashboard(IWebDriver driver) : base(driver)
        {

        }

        public void WaitForDashboardToBeDisplayed()
        {
            Driver.WaitForJsToComplete(Wait);
            Wait.Until(d => d.FindElements(Pages.OrganisationsOrdersDashboard.PageTitle).Count == 1);
        }

        public void SelectExistingOrder(string CallOffAgreementId)
        {
            Wait.Until(d => d.FindElements(Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(CallOffAgreementId)).Count == 1);
            Wait.Until(ElementExtensions.ElementToBeClickable(Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(CallOffAgreementId)));
            Driver.FindElement(Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(CallOffAgreementId)).Click();
        }

        public bool CreateNewOrderButtonDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.OrganisationsOrdersDashboard.CreateOrderButton).Count == 1);
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
            Driver.FindElement(Pages.OrganisationsOrdersDashboard.CreateOrderButton).Click();
        }

        public int GetNumberOfOrdersDisplayed()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.Orders).Count;
        }

        public int GetNumberOfCallOffAgreementIds()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.GenericExistingOrder).Count;
        }
        public int GetNumberOfDescriptions()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.GenericExistingOrderDescription).Count;
        }
        public int GetNumberOfLastUpdatedBys()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedBy).Count;
        }
        public int GetNumberOfLastUpdatedDates()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedDate).Count;
        }
        public int GetNumberOfCreatedDates()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.GenericExistingOrderCreatedDate).Count;
        }
        public bool UnsubmittedOrdersTableDisplayed()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.UnsubmittedOrdersTable).Count == 1;
        }

        public bool SubmittedOrdersTableDisplayed()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.SubmittedOrdersTable).Count == 1;
        }
        public bool NominateProxyDisplayed()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.NominateProxy).Count == 1;
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
