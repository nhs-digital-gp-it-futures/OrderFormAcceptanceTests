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

		public bool LoggedInDisplayNameIsDisplayed()
		{
			try
			{
				Wait.Until(s => s.FindElement(Pages.Common.LoggedInDisplayName).Text.Contains("Logged in as: ", StringComparison.OrdinalIgnoreCase));
				string loggedInText = Driver.FindElement(Pages.Common.LoggedInDisplayName).Text;
				string displayName = loggedInText.Split(":")[1].Split("for")[0].Trim();
				string organisationName = loggedInText.Split("for")[1].Trim();
				return displayName != "" && organisationName != "";
			}
			catch
			{
				return false;
			}
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

		public bool EditDescriptionSectionDisplayed()
		{
			
			try
			{
				Wait.Until(s => s.FindElements(Pages.OrderForm.EditDescription).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void ClickEditDescription()
		{
			Driver.FindElement(Pages.OrderForm.EditDescription).Click();
		}

		public bool EditNamedSectionPageDisplayed(string namedSectionPageTitle)
		{
			try
			{
				Wait.Until(s => s.FindElement(By.TagName("h1")).Text.Equals(namedSectionPageTitle, StringComparison.OrdinalIgnoreCase));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void ClickSaveButton()
		{
			Driver.FindElement(Pages.Common.SaveButton).Click();
		}

		public void EnterTextIntoTextArea(string value, int index = 0)
		{
			Driver.FindElements(Pages.OrderForm.TextArea)[index].SendKeys(value);
		}
	}
}
