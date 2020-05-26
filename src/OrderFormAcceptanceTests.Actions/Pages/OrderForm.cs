using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Linq;

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
				Wait.Until(d => d.FindElement(Pages.Common.LoggedInDisplayName).Text.Contains("Logged in as: ", StringComparison.OrdinalIgnoreCase));
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
		public bool FooterDisplayed()
		{
			return Driver.FindElements(Pages.Common.Footer).Count == 1;
		}

		public bool HeaderDisplayed()
		{
			return Driver.FindElements(Pages.Common.Header).Count == 1;
		}

		public string ErrorTitle()
		{
			return Driver.FindElement(Pages.Common.ErrorTitle).Text;
		}

        public bool ErrorSummaryDisplayed()
		{
			return Driver.FindElements(Pages.Common.ErrorSummary).Count > 0;
		}

		public bool ErrorMessagesDisplayed()
		{
			return Driver.FindElements(Pages.Common.ErrorMessages).Count > 0;
		}

		public bool NewOrderFormDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElement(Pages.OrderForm.PageTitle).Text.Equals("New Order", StringComparison.OrdinalIgnoreCase));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public string GetCallOffId()
		{
			return Driver.FindElement(Pages.OrderForm.PageTitle).Text.Split("Order")[1].Trim();
		}

		public bool CallOffIdDisplayedInPageTitle(string callOffAgreementId)
		{
			return Driver.FindElement(Pages.OrderForm.PageTitle).Text.Contains(callOffAgreementId);
		}

		public bool TaskListDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElements(Pages.OrderForm.TaskList).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool SectionComplete(string section)
		{
			var search = Driver.FindElements(Pages.OrderForm.GenericSection(section)).Single()
				.FindElement(By.XPath("../.."))
				.FindElements(Pages.OrderForm.SectionStatus);

			if (search.Count == 0)
			{
				return false;
			}
			else
			{
				return search[0].Text.Equals("COMPLETE", StringComparison.OrdinalIgnoreCase);
			}
		}

		public void BackLinkDisplayed()
		{
			Driver.FindElements(Pages.Common.BackLink).Count.Should().Be(1);
		}

		public bool DeleteOrderButtonIsDisabled()
		{
			return Driver.FindElement(Pages.OrderForm.DeleteOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled") != null;
		}

		public bool PreviewOrderButtonIsDisabled()
		{
			return Driver.FindElement(Pages.OrderForm.PreviewOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled") != null;
		}

		public bool SubmitOrderButtonIsDisabled()
		{
			return Driver.FindElement(Pages.OrderForm.SubmitOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled") != null;
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
				Wait.Until(d => d.FindElements(Pages.OrderForm.EditDescription).Count == 1);
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

		public bool EditCallOffOrderingPartySectionDisplayed()
		{

			try
			{
				Wait.Until(d => d.FindElements(Pages.OrderForm.EditCallOffOrderingParty).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void ClickEditCallOffOrderingParty()
		{
			Driver.FindElement(Pages.OrderForm.EditCallOffOrderingParty).Click();
		}

		public bool EditSupplierSectionDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElements(Pages.OrderForm.EditSupplier).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}

		}

		public void ClickEditSupplier()
		{
			Driver.FindElement(Pages.OrderForm.EditSupplier).Click();
		}

		public bool EditNamedSectionPageDisplayed(string namedSectionPageTitle)
		{
			try
			{
				Wait.Until(d => d.FindElement(Pages.OrderForm.PageTitle).Text.Contains(namedSectionPageTitle, StringComparison.OrdinalIgnoreCase));
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

		public void ClickBackLink()
		{
			Driver.FindElement(Pages.Common.BackLink).Click();
		}

		public void EnterTextIntoTextArea(string value, int index = 0)
		{
			Driver.FindElements(Pages.Common.TextArea)[index].SendKeys(value);
		}

		public void EnterTextIntoTextField(string value, int index = 0)
		{
			Driver.FindElements(Pages.Common.TextField)[index].SendKeys(value);
		}

		public string ClickOnErrorLink()
		{
			var errorMessages = Driver.FindElements(Pages.Common.ErrorMessages).ToList();
			var index = new Random().Next(errorMessages.Count());

			var linkHref = errorMessages[index].GetAttribute("href");

			errorMessages[index].Click();

			return linkHref;
		}

		public bool OdsCodeDisplayedAndNotEditable()
		{
			try
			{
				var displayed = Driver.FindElements(Pages.OrderForm.OrganisationOdsCode).Count == 1;
				var notEditable = Driver.FindElement(Pages.OrderForm.OrganisationOdsCode).TagName == "div";
				return displayed && notEditable;
			}
			catch { return false; }
		}

		public bool OrganisationNameDisplayedAndNotEditable()
		{
			try
			{
				var displayed = Driver.FindElements(Pages.OrderForm.OrganisationName).Count == 1;
				var notEditable = Driver.FindElement(Pages.OrderForm.OrganisationName).TagName == "div";
				return displayed && notEditable;
			}
			catch { return false; }
		}
		public bool OrganisationAddressDisplayedAndNotEditable()
		{
			try
			{
				var lineDisplayed = Driver.FindElements(Pages.OrderForm.AddressLineX(1)).Count == 1;
				var lineNotEditable = Driver.FindElement(Pages.OrderForm.AddressLineX(1)).TagName == "div";
				var townDisplayed = Driver.FindElements(Pages.OrderForm.OrganisationAddressTown).Count == 1;
				var townNotEditable = Driver.FindElement(Pages.OrderForm.OrganisationAddressTown).TagName == "div";
				var countyDisplayed = Driver.FindElements(Pages.OrderForm.OrganisationAddressCounty).Count == 1;
				var countyNotEditable = Driver.FindElement(Pages.OrderForm.OrganisationAddressCounty).TagName == "div";
				var postcodeDisplayed = Driver.FindElements(Pages.OrderForm.OrganisationAddressPostcode).Count == 1;
				var postcodeNotEditable = Driver.FindElement(Pages.OrderForm.OrganisationAddressPostcode).TagName == "div";
				return lineDisplayed && lineNotEditable && townDisplayed && townNotEditable && countyDisplayed && countyNotEditable && postcodeDisplayed && postcodeNotEditable;
			}
			catch { return false; }
		}

		public string GetOdsCode()
		{
			return Driver.FindElement(Pages.OrderForm.OrganisationOdsCode).Text;
		}

		public string GetOrganisationName()
		{
			return Driver.FindElement(Pages.OrderForm.OrganisationName).Text;
		}

		public Address GetOrganisationAddress()
		{
			return new Address
			{
				Line1 = Driver.FindElement(Pages.OrderForm.AddressLineX(1)).Text,
				Line2 = Driver.FindElement(Pages.OrderForm.AddressLineX(2)).Text,
				Line3 = Driver.FindElement(Pages.OrderForm.AddressLineX(3)).Text,
				Line4 = Driver.FindElement(Pages.OrderForm.AddressLineX(4)).Text,
				Town = Driver.FindElement(Pages.OrderForm.OrganisationAddressTown).Text,
				County = Driver.FindElement(Pages.OrderForm.OrganisationAddressCounty).Text,
				Postcode = Driver.FindElement(Pages.OrderForm.OrganisationAddressPostcode).Text,
				Country = Driver.FindElement(Pages.OrderForm.OrganisationAddressCountry).Text
			};
		}

		public void EnterContact(Contact contact)
		{
			Wait.Until(d => d.FindElements(Pages.OrderForm.ContactFirstName).Count == 1);
			Driver.FindElement(Pages.OrderForm.ContactFirstName).SendKeys(contact.FirstName);
			Driver.FindElement(Pages.OrderForm.ContactLastName).SendKeys(contact.LastName);
			Driver.FindElement(Pages.OrderForm.ContactEmail).SendKeys(contact.Email);
			Driver.FindElement(Pages.OrderForm.ContactTelephone).SendKeys(contact.Phone);
		}
	}
}
