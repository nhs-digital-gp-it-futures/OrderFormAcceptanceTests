namespace OrderFormAcceptanceTests.Objects.Pages
{
    using System;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class OrderForm
    {
        public static By DeleteOrderButton => CustomBy.DataTestId("delete-order-button");

        public static By PreviewOrderButton => CustomBy.DataTestId("preview-order-button");

        public static By CompleteOrderLink => CustomBy.DataTestId("complete-order-button");

        public static By CompleteOrderButton => CustomBy.DataTestId("complete-order-button", "button");

        public static By CompleteOrderLabel => CustomBy.DataTestId("complete-order-button");

        public static By PageTitle => By.CssSelector("[data-test-id$='-title']");

        public static By TaskList => CustomBy.DataTestId("task-list");

        public static By SectionStatus => By.CssSelector("[data-test-id$='-complete-tag']");

        public static By SectionDescription => By.CssSelector("[data-test-id$='-description']");

        public static By OrderDescription => By.CssSelector("[data-test-id$=order-description");

        public static By OrganisationName => CustomBy.DataTestId("organisation-name");

        public static By OrganisationOdsCode => CustomBy.DataTestId("organisation-ods-code");

        public static Func<int, By> AddressLine => (lineNumber) => By.CssSelector(string.Format("[data-test-id$=-address-{0}]", lineNumber.ToString()));

        public static By AddressTown => By.CssSelector("[data-test-id$=-address-town]");

        public static By AddressCounty => By.CssSelector("[data-test-id$=-address-county]");

        public static By AddressPostcode => By.CssSelector("[data-test-id$=-address-postcode]");

        public static By AddressCountry => By.CssSelector("[data-test-id$=-address-country]");

        public static By ContactFirstName => By.Id("firstName");

        public static By ContactLastName => By.Id("lastName");

        public static By ContactEmail => By.Id("emailAddress");

        public static By ContactTelephone => By.Id("telephoneNumber");

        public static By SearchButton => CustomBy.DataTestId("search-button", "button");

        public static By SupplierOptionsLabels => CustomBy.DataTestId("question-selectSupplier", "label");

        public static By SupplierOptions => CustomBy.DataTestId("question-selectSupplier", "input");

        public static By SupplierName => CustomBy.DataTestId("supplier-name");

        public static By SearchAgainLink => CustomBy.DataTestId("search-again-link", "a");

        public static By SelectDeselectAll => CustomBy.DataTestId("select-deselect-button", "button");

        public static By ServiceRecipientName => By.CssSelector("[data-test-id$='organisationName'] label");

        public static By ServiceRecipientOdsCode => By.CssSelector("[data-test-id$='odsCode']");

        public static By AddSolution => CustomBy.DataTestId("add-orderItem-button", "a");

        public static By NoSolutionsAdded => CustomBy.DataTestId("no-added-orderItems");

        public static By AddedOrderItemsTable => CustomBy.DataTestId("added-orderItems");

        public static By AddedOrderItemName => By.CssSelector("[data-test-id$='-catalogueItemName']");

        public static By AddedSolutionServiceRecipient => By.CssSelector("[data-test-id$='-recipient']");

        public static By PriceInput => By.Id("price");

        public static By OrderUnit => By.CssSelector("label[for=price]");

        public static By Quantity => By.Id("quantity");

        public static By OrderDate => CustomBy.DataTestId("date-field-input");

        public static By OrderDateDay => By.Id("deliveryDate-day");

        public static By OrderDateMonth => By.Id("deliveryDate-month");

        public static By OrderDateYear => By.Id("deliveryDate-year");

        public static By EstimationPeriod => CustomBy.DataTestId("question-selectEstimationPeriod");

        public static By PracticeListSizeInput => By.Id("quantity");

        public static Func<string, By> GenericSection => (sectionHrefRoute) => By.CssSelector(string.Format("[href$='{0}']", sectionHrefRoute));

        public static By EditServiceRecipientsButton => CustomBy.DataTestId("edit-button", "a");

        public static By CancelDeleteLink => By.PartialLinkText("Cancel delete");

        public static By DeleteSolutionConfirmation => CustomBy.DataTestId("delete-catalogue-confirmation-page-title");

        public static By OrderUnitAssociatedService => CustomBy.DataTestId("unit-of-order");
    }
}
