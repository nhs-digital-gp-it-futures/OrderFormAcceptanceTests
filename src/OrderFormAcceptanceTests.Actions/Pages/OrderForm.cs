using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

namespace OrderFormAcceptanceTests.Actions.Pages
{
	public class OrderForm : PageAction
	{
		public OrderForm(IWebDriver driver) : base(driver)
		{
		}

		public string ErrorTitle()
		{
			return Driver.FindElement(Pages.Common.ErrorTitle).Text;
		}

		public void CreateNewOrder()
		{
			Driver.FindElement(Pages.OrderFormDashboard.CreateOrderButton).Click();			
		}

		public bool NewOrderFormDisplayed()
		{
			try
			{
				Wait.Until(s => s.FindElement(By.TagName("h1")).Text.Equals("New Order", StringComparison.OrdinalIgnoreCase));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void BackLinkDisplayed()
		{
			Driver.FindElements(Pages.Common.BackLink).Count.Should().Be(1);
		}

		public void DeleteOrderButtonIsDisabled()
		{
			Driver.FindElement(Pages.OrderForm.DeleteOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled").Should().BeEquivalentTo("true");
		}

		public void PreviewOrderButtonIsDisabled()
		{
			Driver.FindElement(Pages.OrderForm.PreviewOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled").Should().BeEquivalentTo("true");
		}

		public void SubmitOrderButtonIsDisabled()
		{
			Driver.FindElement(Pages.OrderForm.SubmitOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled").Should().BeEquivalentTo("true");
		}

		public void DeleteOrderButtonHasAltTest()
		{
			Driver.FindElement(Pages.OrderForm.DeleteOrderButton).GetAttribute("aria-label").Length.Should().BeGreaterThan(0);
		}

		public void PreviewOrderButtonHasAltTest()
		{
			Driver.FindElement(Pages.OrderForm.PreviewOrderButton).GetAttribute("aria-label").Length.Should().BeGreaterThan(0);
		}

		public void SubmitOrderButtonHasAltTest()
		{
			Driver.FindElement(Pages.OrderForm.SubmitOrderButton).GetAttribute("aria-label").Length.Should().BeGreaterThan(0);
		}
	}
}
