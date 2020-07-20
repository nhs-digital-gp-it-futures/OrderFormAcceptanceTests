using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;
using System;
using System.Runtime.CompilerServices;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public sealed class OrderForm
	{
		public By DeleteOrderButton => CustomBy.DataTestId("delete-order-button");
		public By PreviewOrderButton => CustomBy.DataTestId("preview-order-button");
		public By SubmitOrderButton => CustomBy.DataTestId("submit-order-button");
		public By PageTitle => By.CssSelector("[data-test-id$='-page-title']");
		public By TaskList => CustomBy.DataTestId("task-list");
		public By SectionStatus => By.CssSelector("[data-test-id$='-complete-tag']");
		public By EditDescription => CustomBy.DataTestId("task-0-item-0-description");
		public By EditCallOffOrderingParty => CustomBy.DataTestId("task-1-item-0-description");
		public By EditSupplier => CustomBy.DataTestId("task-1-item-1-description");
		public By EditCommencementDate => CustomBy.DataTestId("task-2-item-0-description");
		public By EditServiceRecipients => CustomBy.DataTestId("task-3-item-0-description");
		public By EditCatalogueSolutions => CustomBy.DataTestId("task-4-item-0-description");
		public By EditAdditionalServices => CustomBy.DataTestId("task-5-item-0-description");
		public By EditAssociatedServices => CustomBy.DataTestId("task-6-item-0-description");
		public By GenericSection(string sectionHrefRoute) {
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
		public By SupplierOptions => CustomBy.DataTestId("question-selectSupplier", "input");
		public By SupplierName => CustomBy.DataTestId("supplier-name");
		public By SearchAgainLink => CustomBy.DataTestId("search-again-link", "a");
		public By SelectDeselectAll => CustomBy.DataTestId("select-deselect-button", "button");
		public By ServiceRecipientName => By.CssSelector("[data-test-id^='organisation-name'] label");
		public By ServiceRecipientOdsCode => By.CssSelector(".bc-c-service-recipients-table__ods");
		public By AddSolution => CustomBy.DataTestId("add-orderItem-button", "a");
		public By NoSolutionsAdded => CustomBy.DataTestId("no-added-orderItems");
		public By AddedSolutionsTable => CustomBy.DataTestId("added-orderItems");
		public By AddedSolutionName => By.CssSelector("[data-test-id$='-solutionName']");
		public By AddedSolutionServiceRecipient => By.CssSelector("[data-test-id$='-serviceRecipient']");
		public By PriceInput => By.Id("price");
		public By OrderUnit => CustomBy.DataTestId("unit-of-order");
		public By Quantity => By.Id("quantity");
		public By OrderDate => CustomBy.DataTestId("date-field-input");
		public By OrderDateDay => By.Id("deliveryDate-day");
		public By OrderDateMonth => By.Id("deliveryDate-month");
		public By OrderDateYear => By.Id("deliveryDate-year");
		public By EstimationPeriod => CustomBy.DataTestId("question-selectEstimationPeriod");

		public By DateOrderSummaryCreated => CustomBy.DataTestId("date-summary-created");
		public By CallOffOrderingPartyPreview => CustomBy.DataTestId("call-off-party");
		public By SupplierPreview => CustomBy.DataTestId("supplier");
		public By CommencementDate => CustomBy.DataTestId("commencement-date");
		public By OneOffCostsTable => CustomBy.DataTestId("one-off-cost-table");
		public By RecurringCostsTable => CustomBy.DataTestId("recurring-cost-table");
		public Func<int, By> TableRowX => (index) => By.CssSelector(string.Format("[data-test-id=table-row-{0}]", index.ToString()));

		public By ItemRecipientName = CustomBy.DataTestId("recipient-name");
		public By ItemId => CustomBy.DataTestId("item-id");
		public By ItemName => CustomBy.DataTestId("item-name");
		public By ItemPrice => CustomBy.DataTestId("price-unit");
		public By ItemQuantity => CustomBy.DataTestId("quantity");
		public By ItemPlannedDate => CustomBy.DataTestId("planned-date");
		public By ItemCost => CustomBy.DataTestId("item-cost");
		public By TotalOneOffCost => CustomBy.DataTestId("total-cost-value");
		public By TotalAnnualCost => CustomBy.DataTestId("total-year-cost-value");
		public By TotalMonthlyCost => CustomBy.DataTestId("total-monthly-cost-value");
		public By TotalOwnershipCost => CustomBy.DataTestId("total-ownership-cost-value");

		
	}
}