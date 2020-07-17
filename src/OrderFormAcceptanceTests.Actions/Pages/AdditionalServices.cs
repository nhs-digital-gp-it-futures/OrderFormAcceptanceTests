using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

namespace OrderFormAcceptanceTests.Actions.Pages
{
	public sealed class AdditionalServices : PageAction
	{
		public AdditionalServices(IWebDriver driver) : base(driver)
		{
		}

		public bool AddAdditionalServiceButtonDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElements(Pages.AdditionalServices.AddAdditionalServices).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void PageDisplayed()
		{
			Wait.Until(s => s.FindElement(Pages.OrderForm.PageTitle).Text.Contains("Additional service", StringComparison.OrdinalIgnoreCase));
		}

		public bool NoAddedOrderItemsDisplayed()
		{
			return Driver.FindElements(Pages.AdditionalServices.NoAddedOrderItemsMessage).Count == 1;
		}

		public string PricePageTitle()
		{
			return Driver.FindElement(Pages.AdditionalServices.PricePageTitle).Text;
		}
	}
}
