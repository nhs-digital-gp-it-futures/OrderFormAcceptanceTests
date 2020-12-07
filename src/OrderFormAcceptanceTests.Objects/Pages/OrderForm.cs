using System;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public sealed class OrderForm
    {
        public By DeleteOrderButton => CustomBy.DataTestId("delete-order-button");
        public By PreviewOrderButton => CustomBy.DataTestId("preview-order-button");
        public By CompleteOrderLink => CustomBy.DataTestId("complete-order-button", "a");
        public By CompleteOrderButton => CustomBy.DataTestId("complete-order-button", "button");
        public By CompleteOrderLabel => CustomBy.DataTestId("complete-order-button");
        public By PageTitle => By.CssSelector("[data-test-id$='-page-title']");
        public By TaskList => CustomBy.DataTestId("task-list");
        public By SectionStatus => By.CssSelector("[data-test-id$='-complete-tag']");
        public By SectionDescription => By.CssSelector("[data-test-id$='-description']");
        public By GenericSection(string sectionHrefRoute)
        {
            return By.CssSelector(string.Format("[href$='{0}']", sectionHrefRoute));
        }

        public By OrderDescription => By.CssSelector("[data-test-id$=order-description");
        public By OrganisationName => CustomBy.DataTestId("organisation-name");
        public By OrganisationOdsCode => CustomBy.DataTestId("organisation-ods-code");
        public Func<int, By> AddressLineX => (LineNumber) => By.CssSelector(string.Format("[data-test-id$=-address-{0}]", LineNumber.ToString()));
        public By AddressTown => By.CssSelector("[data-test-id$=-address-town]");
        public By AddressCounty => By.CssSelector("[data-test-id$=-address-county]");
        public By AddressPostcode => By.CssSelector("[data-test-id$=-address-postcode]");
        public By AddressCountry => By.CssSelector("[data-test-id$=-address-country]");
        public By ContactFirstName => By.Id("firstName");
        public By ContactLastName => By.Id("lastName");
        public By ContactEmail => By.Id("emailAddress");
        public By ContactTelephone => By.Id("telephoneNumber");
        public By SearchButton => CustomBy.DataTestId("search-button", "button");
        public By SupplierOptionsLabels => CustomBy.DataTestId("question-selectSupplier", "label");
        public By SupplierOptions => CustomBy.DataTestId("question-selectSupplier", "input");
        public By SupplierName => CustomBy.DataTestId("supplier-name");
        public By SearchAgainLink => CustomBy.DataTestId("search-again-link", "a");
        public By SelectDeselectAll => CustomBy.DataTestId("select-deselect-button", "button");
        public By ServiceRecipientName => By.CssSelector("[data-test-id$='organisationName'] label");
        public By ServiceRecipientOdsCode => By.CssSelector("[data-test-id$='odsCode']");
        public By AddSolution => CustomBy.DataTestId("add-orderItem-button", "a");
        public By NoSolutionsAdded => CustomBy.DataTestId("no-added-orderItems");
        public By AddedOrderItemsTable => CustomBy.DataTestId("added-orderItems");
        public By AddedOrderItemName => By.CssSelector("[data-test-id$='-catalogueItemName']");
        public By AddedSolutionServiceRecipient => By.CssSelector("[data-test-id$='-serviceRecipient']");
        public By PriceInput => By.Id("price");
        public By OrderUnit => CustomBy.DataTestId("unit-of-order");
        public By Quantity => By.Id("quantity");
        public By OrderDate => CustomBy.DataTestId("date-field-input");
        public By OrderDateDay => By.Id("deliveryDate-day");
        public By OrderDateMonth => By.Id("deliveryDate-month");
        public By OrderDateYear => By.Id("deliveryDate-year");
        public By EstimationPeriod => CustomBy.DataTestId("question-selectEstimationPeriod");
        public By PracticeListSizeInput => By.Id("quantity");
    }
}
