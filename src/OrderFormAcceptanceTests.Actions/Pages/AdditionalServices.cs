using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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

		public string ServiceRecipientsTitle()
		{
			return Driver.FindElement(Pages.AdditionalServices.ServiceRecipientsTitle).Text;
		}

		public List<string> ServiceRecipientNames()
		{
			return Driver.FindElements(Pages.Common.RadioButtonLabel).Select(s => s.Text).ToList();
		}

		public object GetTableRowsCount()
		{
			return Driver.FindElements(Pages.Common.TableRows).Count;
		}

		public bool PriceInputDisplayed()
		{
			return Driver.ElementVisible(Pages.AdditionalServices.PriceInput);
		}

		public bool PriceUnitDisplayed()
		{
			return Driver.ElementVisible(Pages.AdditionalServices.PriceUnit);
		}

		public bool QuantityInputDisplayed()
		{
			return Driver.ElementVisible(Pages.AdditionalServices.QuantityInput);
		}

		public void SetQuantityAboveMax()
		{
			SetQuantity("2147483648");
		}

		public void SetQuantity(string value = "1000")
		{
			Driver.FindElement(Pages.AdditionalServices.QuantityInput).SendKeys(value);
		}

		public int AdditionalServicesAddedTableRowsCount()
		{
			return Driver.FindElements(Pages.Common.TableRows).Count;
		}
	}
}
