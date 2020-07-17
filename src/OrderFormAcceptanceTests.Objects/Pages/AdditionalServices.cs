using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public sealed class AdditionalServices
	{
		public By AddAdditionalServices => CustomBy.DataTestId("add-orderItem-button");

		public By NoAddedOrderItemsMessage => CustomBy.DataTestId("no-added-orderItems");

		public By PricePageTitle => CustomBy.DataTestId("additional-service-price-page-title");

		public By ServiceRecipientsTitle => CustomBy.DataTestId("additional-service-recipient-page-title");
	}
}
