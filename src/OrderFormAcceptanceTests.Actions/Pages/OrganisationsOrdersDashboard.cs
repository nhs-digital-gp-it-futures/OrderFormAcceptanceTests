namespace OrderFormAcceptanceTests.Actions.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Flurl;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Actions.Utils;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Models;

    public class OrganisationsOrdersDashboard : PageAction
    {
        public OrganisationsOrdersDashboard(IWebDriver driver)
            : base(driver)
        {
        }

        public void WaitForDashboardToBeDisplayed()
        {
            Wait.ForJsToComplete();
            Wait.Until(d => CreateNewOrderButtonDisplayed());
        }

        public void SelectExistingOrder(string callOffAgreementId, string baseUrl)
        {
            var url = baseUrl
                .AppendPathSegment("organisation")
                .AppendPathSegment(callOffAgreementId).ToString();

            Driver.Navigate().GoToUrl(url);
            Wait.Until(s => s.FindElement(Objects.Pages.OrderForm.TaskList).Displayed);
        }

        public bool CreateNewOrderButtonDisplayed()
        {
            try
            {
                Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.CreateOrderButton);
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
            Driver.FindElement(Objects.Pages.OrganisationsOrdersDashboard.CreateOrderButton).Click();
        }

        public (string TagName, string Text, string Url) GetOrderSummaryLink(string callOffAgreementId)
        {
            var link = Driver.FindElement(Objects.Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(callOffAgreementId));
            return (link.TagName, link.Text, link.GetAttribute("href"));
        }

        public int GetNumberOfOrdersDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.Orders).Count;
        }

        public int GetNumberOfCallOffAgreementIds()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrder).Count;
        }

        public int GetNumberOfDescriptions()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderDescription).Count;
        }

        public int GetNumberOfLastUpdatedBys()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedBy).Count;
        }

        public int GetNumberOfLastUpdatedDates()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedDate).Count;
        }

        public int GetNumberOfCreatedDates()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderCreatedDate).Count;
        }

        public bool IncompleteOrdersTableDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.IncompleteOrdersTable).Count == 1;
        }

        public bool CompletedOrdersTableDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrganisationsOrdersDashboard.CompletedOrdersTable).Count == 1;
        }

        public bool CompletedOrdersTableHasNoLastUpdateDate()
        {
            return Driver
                .FindElement(Objects.Pages.OrganisationsOrdersDashboard.CompletedOrdersTable)
                .FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedDate)
                .Count == 0;
        }

        public bool CompletedOrdersTableHasDateCompleted()
        {
            IReadOnlyCollection<IWebElement> columnHeadings = Driver
                .FindElement(Objects.Pages.OrganisationsOrdersDashboard.CompletedOrdersTable)
                .FindElements(Objects.Pages.OrganisationsOrdersDashboard.GenericColumnHeadingData);

            return columnHeadings.Count(c => c.Text.Equals("Date completed", StringComparison.OrdinalIgnoreCase)) == 1;
        }

        public bool BackLinkDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.BackLink).Count == 1;
        }

        public void ClickBackLink()
        {
            Driver.FindElement(Objects.Pages.Common.BackLink).Click();
        }

        public bool FooterDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.Footer).Count == 1;
        }

        public bool HeaderDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.Header).Count == 1;
        }

        public bool BetaBannerDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.BetaBanner).Count == 1;
        }

        public int GetNumberOfIncompleteOrders()
        {
            return GetNumberOfTableRows(Objects.Pages.OrganisationsOrdersDashboard.IncompleteOrdersTable);
        }

        public int GetNumberOfCompleteOrders()
        {
            return GetNumberOfTableRows(Objects.Pages.OrganisationsOrdersDashboard.CompletedOrdersTable);
        }

        public bool IncompleteOrdersPrecedesCompletedOrders()
        {
            return Driver.FindElement(Objects.Pages.OrganisationsOrdersDashboard.IncompleteOrdersBeforeCompletedOrders).Displayed;
        }

        public List<OrderTableItem> GetListOfIncompleteOrders()
        {
            var listOfIncompleteOrders = new List<OrderTableItem>();

            var table = Driver.FindElement(Objects.Pages.OrganisationsOrdersDashboard.IncompleteOrdersTable);
            var tableRows = table.FindElements(Objects.Pages.Common.TableRows);

            foreach (var row in tableRows)
            {
                var item = new OrderTableItem
                {
                    Id = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrder).Text,
                    Description = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderDescription).Text,
                    Created = DateTime.Parse(row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderCreatedDate).Text),
                    LastUpdatedBy = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedBy).Text,
                    LastUpdated = DateTime.Parse(row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedDate).Text),
                };

                listOfIncompleteOrders.Add(item);
            }

            return listOfIncompleteOrders;
        }

        public List<OrderTableItem> GetListOfCompletedOrders()
        {
            var listOfCompletedOrders = new List<OrderTableItem>();

            var table = Driver.FindElement(Objects.Pages.OrganisationsOrdersDashboard.CompletedOrdersTable);
            var tableRows = table.FindElements(Objects.Pages.Common.TableRows);

            foreach (var row in tableRows)
            {
                var item = new OrderTableItem
                {
                    Id = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrder).Text,
                    Description = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderDescription).Text,
                    Created = DateTime.Parse(row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderCreatedDate).Text),
                    LastUpdatedBy = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderLastUpdatedBy).Text,
                    Completed = DateTime.Parse(row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderCompletedDate).Text),
                    AutomaticallyProcessed = row.FindElement(Objects.Pages.OrganisationsOrdersDashboard.GenericExistingOrderAutomaticallyProcessed).Text.Equals("yes", StringComparison.OrdinalIgnoreCase),
                };

                listOfCompletedOrders.Add(item);
            }

            return listOfCompletedOrders;
        }

        public void SelectCompletedOrder(string orderId)
        {
            Driver.FindElement(Objects.Pages.OrganisationsOrdersDashboard.SpecificExistingOrder(orderId)).Click();
        }

        private int GetNumberOfTableRows(By tableSelector)
        {
            var table = Driver.FindElement(tableSelector);
            IReadOnlyCollection<IWebElement> tableRows = table.FindElements(Objects.Pages.Common.TableRows);

            return tableRows.Count;
        }
    }
}
