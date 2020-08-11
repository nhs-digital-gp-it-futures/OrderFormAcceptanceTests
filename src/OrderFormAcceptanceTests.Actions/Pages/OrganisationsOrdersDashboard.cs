using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Collections.Generic;
using OrderFormAcceptanceTests.Objects.Pages;

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
            Wait.Until(d => CreateNewOrderButtonDisplayed());
        }

        public void SelectExistingOrder(string callOffAgreementId)
        {
            Wait.Until(d => d.FindElements(Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(callOffAgreementId)).Count == 1);
            Wait.Until(ElementExtensions.ElementToBeClickable(Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(callOffAgreementId)));
            Driver.FindElement(Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(callOffAgreementId)).Click();
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

        public bool IncompleteOrdersTableDisplayed()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.IncompleteOrdersTable).Count == 1;
        }

        public bool SubmittedOrdersTableDisplayed()
        {
            return Driver.FindElements(Pages.OrganisationsOrdersDashboard.CompletedOrdersTable).Count == 1;
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

        public List<Order> GetListOfIncompleteOrders()
        {
            var listOfIncompleteOrders = new List<Order>();

            var table = Driver.FindElement(Pages.OrganisationsOrdersDashboard.IncompleteOrdersTable);
            var tableRows = table.FindElements(Pages.Common.TableRows);

            foreach (var row in tableRows)
            {
                var id = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrder).Text;
                var description = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderDescription).Text;
                var lastUpdateDisplayName = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedBy).Text;
                var lastUpdatedDate = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedDate).Text;
                var createdDate = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderCreatedDate).Text;
                var currentRowOrder = new Order
                {
                    OrderId = id,
                    Description = description,
                    LastUpdatedByName = lastUpdateDisplayName,
                    LastUpdated = Convert.ToDateTime(lastUpdatedDate),
                    Created = Convert.ToDateTime(createdDate)
                };
                listOfIncompleteOrders.Add(currentRowOrder);
            }

            return listOfIncompleteOrders;
        }

        public List<Order> GetListOfCompletedOrders()
        {
            List<Order> listOfSubmittedOrders = new List<Order>();

            var table = Driver.FindElement(Pages.OrganisationsOrdersDashboard.CompletedOrdersTable);
            var tableRows = table.FindElements(Pages.Common.TableRows);

            foreach (var row in tableRows)
            {
                var id = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrder).Text;
                var description = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderDescription).Text;
                var lastUpdateDisplayName = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedBy).Text;
                var createdDate = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderCreatedDate).Text;
                var automaticallyProcessed = row.FindElement(Pages.OrganisationsOrdersDashboard.GenericExistingOrderAutomaticallyProcessed).Text;
                
                var currentRowOrder = new Order
                {
                    OrderId = id,
                    Description = description,
                    LastUpdatedByName = lastUpdateDisplayName,
                    Created = Convert.ToDateTime(createdDate),
                    FundingSourceOnlyGMS = automaticallyProcessed== "Yes" ? 1 : 0
                };
                listOfSubmittedOrders.Add(currentRowOrder);
            }

            return listOfSubmittedOrders;
        }
    }
}
