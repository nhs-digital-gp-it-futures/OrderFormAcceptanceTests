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
		public By GenericSection(string sectionHrefRoute) {
			return By.CssSelector(string.Format("[href$='{0}']", sectionHrefRoute));
		}

		public By OrganisationName => CustomBy.DataTestId("organisation-name");
		public By OrganisationOdsCode => CustomBy.DataTestId("organisation-ods-code");
		public Func<int, By> AddressLineX => (LineNumber) => CustomBy.PartialDataTestId("organisation-address-{0}", LineNumber.ToString());
		public By OrganisationAddressTown => CustomBy.DataTestId("organisation-address-town");
		public By OrganisationAddressCounty => CustomBy.DataTestId("organisation-address-county");
		public By OrganisationAddressPostcode => CustomBy.DataTestId("organisation-address-postcode");
		public By OrganisationAddressCountry => CustomBy.DataTestId("organisation-address-country");

        
    }
}
