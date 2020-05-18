using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

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
		public By EditDescription => CustomBy.DataTestId("item-0-description");
		public By GenericSection(string section) { 
			return CustomBy.PartialDataTestId("item-0-{0}", section);
		}
		public By TextArea => By.TagName("textarea");
	}
}
