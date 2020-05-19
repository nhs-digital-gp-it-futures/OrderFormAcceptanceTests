using OrderFormAcceptanceTests.Objects.Pages;

namespace OrderFormAcceptanceTests.Objects.Collections
{
	public sealed class PageCollection
	{
		public Login Login { get; set; }
		public Homepage Homepage { get; set; }
		public Common Common { get; set; }
		public OrganisationsOrdersDashboard OrganisationsOrdersDashboard { get; set; }
		public OrderForm OrderForm { get; set; }
	}
}
