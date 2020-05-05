
using OrderFormAcceptanceTests.Actions.Pages;

namespace OrderFormAcceptanceTests.Actions.Collections
{
	public sealed class PageActionCollection
	{
		public Authentication Authentication { get; set; }
		public Pages.Homepage Homepage { get; set; }
		public OrderForm OrderForm { get; set; }
	}
}
