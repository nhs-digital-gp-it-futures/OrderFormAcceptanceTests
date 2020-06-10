using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public bool EditCommencementDateSectionDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElements(Pages.OrderForm.EditCommencementDate).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void ClickEditCommencementDate()
		{
			Driver.FindElement(Pages.OrderForm.EditCommencementDate).Click();
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

		public bool CallOffOrderingPartyEnabled()
		{
			return Driver.FindElement(Pages.OrderForm.EditCallOffOrderingParty).TagName == "a";
		}

		public bool OrderDescriptionEnabled()
		{
			return Driver.FindElement(Pages.OrderForm.EditDescription).TagName == "a";
		}

		public bool SupplierInformationEnabled()
		{
			return Driver.FindElement(Pages.OrderForm.EditSupplier).TagName == "a";
		}

		public void ClickEditCallOffOrderingParty()
		{
			Driver.FindElement(Pages.OrderForm.EditCallOffOrderingParty).Click();
		}

		public bool CommencementDateEnabled()
		{
			return Driver.FindElement(Pages.OrderForm.EditCommencementDate).TagName == "a";
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

		public bool EditServiceRecipientsSectionDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElements(Pages.OrderForm.EditServiceRecipients).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void ClickEditServiceRecipients()
		{
			Driver.FindElement(Pages.OrderForm.EditServiceRecipients).Click();
		}

		public bool EditCatalogueSolutionsSectionDisplayed()
		{
			try
			{
				Wait.Until(d => d.FindElements(Pages.OrderForm.EditCatalogueSolutions).Count == 1);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool EditCatalogueSolutionsSectionIsEnabled()
		{
			return Driver.FindElement(Pages.OrderForm.EditCatalogueSolutions).TagName == "a";
		}

		public void ClickEditCatalogueSolutions()
		{
			Driver.FindElement(Pages.OrderForm.EditCatalogueSolutions).Click();
		}

		public void ClickSaveButton()
		{
			Driver.FindElement(Pages.Common.SaveButton).Click();
		}

		public bool ContinueButtonDisplayed()
		{
			return Driver.FindElements(Pages.Common.ContinueButton).Count ==  1;
		}

		public void ClickContinueButton()
		{
			Driver.FindElement(Pages.Common.ContinueButton).Click();
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
			Wait.Until(d => d.FindElements(Pages.Common.TextField).Count > index);
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

		public string GetOrderDescription()
		{
			Wait.Until(d => d.FindElements(Pages.OrderForm.OrderDescription).Count == 1);
			return Driver.FindElement(Pages.OrderForm.OrderDescription).Text;
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
		public bool AddressDisplayedAndNotEditable()
		{
			try
			{
				var lineDisplayed = Driver.FindElements(Pages.OrderForm.AddressLineX(1)).Count == 1;
				var lineNotEditable = Driver.FindElement(Pages.OrderForm.AddressLineX(1)).TagName == "div";
				var townDisplayed = Driver.FindElements(Pages.OrderForm.AddressTown).Count == 1;
				var townNotEditable = Driver.FindElement(Pages.OrderForm.AddressTown).TagName == "div";
				var countyDisplayed = Driver.FindElements(Pages.OrderForm.AddressCounty).Count == 1;
				var countyNotEditable = Driver.FindElement(Pages.OrderForm.AddressCounty).TagName == "div";
				var postcodeDisplayed = Driver.FindElements(Pages.OrderForm.AddressPostcode).Count == 1;
				var postcodeNotEditable = Driver.FindElement(Pages.OrderForm.AddressPostcode).TagName == "div";
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

		public Address GetAddress()
		{
			return new Address
			{
				Line1 = Driver.FindElement(Pages.OrderForm.AddressLineX(1)).Text,
				Line2 = Driver.FindElement(Pages.OrderForm.AddressLineX(2)).Text,
				Line3 = Driver.FindElement(Pages.OrderForm.AddressLineX(3)).Text,
				Line4 = Driver.FindElement(Pages.OrderForm.AddressLineX(4)).Text,
				Town = Driver.FindElement(Pages.OrderForm.AddressTown).Text,
				County = Driver.FindElement(Pages.OrderForm.AddressCounty).Text,
				Postcode = Driver.FindElement(Pages.OrderForm.AddressPostcode).Text,
				Country = Driver.FindElement(Pages.OrderForm.AddressCountry).Text
			};
		}

		public void EnterContact(Contact contact)
		{
			Wait.Until(d => d.FindElements(Pages.OrderForm.ContactFirstName).Count == 1);
			Driver.FindElement(Pages.OrderForm.ContactFirstName).Clear();
			Driver.FindElement(Pages.OrderForm.ContactFirstName).SendKeys(contact.FirstName);
			Driver.FindElement(Pages.OrderForm.ContactLastName).Clear();
			Driver.FindElement(Pages.OrderForm.ContactLastName).SendKeys(contact.LastName);
			Driver.FindElement(Pages.OrderForm.ContactEmail).Clear();
			Driver.FindElement(Pages.OrderForm.ContactEmail).SendKeys(contact.Email);
			Driver.FindElement(Pages.OrderForm.ContactTelephone).Clear();
			Driver.FindElement(Pages.OrderForm.ContactTelephone).SendKeys(contact.Phone);
		}

		public Contact GetContact()
		{			
			Wait.Until(d => d.FindElements(Pages.OrderForm.ContactFirstName).Count == 1);
			return new Contact()
			{
				FirstName = Driver.FindElement(Pages.OrderForm.ContactFirstName).GetAttribute("value"),
				LastName = Driver.FindElement(Pages.OrderForm.ContactLastName).GetAttribute("value"),
				Email = Driver.FindElement(Pages.OrderForm.ContactEmail).GetAttribute("value"),
				Phone = Driver.FindElement(Pages.OrderForm.ContactTelephone).GetAttribute("value")
			};
		}

		public void ClickSearchButton()
		{
			Wait.Until(d => d.FindElements(Pages.OrderForm.SearchButton).Count == 1);
			Driver.FindElement(Pages.OrderForm.SearchButton).Click();
		}

		public ReadOnlyCollection<IWebElement> ListOfSuppliers()
		{
			return Driver.FindElements(Pages.OrderForm.SupplierOptions);
		}

		public void SelectSupplier()
		{
			SelectSupplier(null);
		}

		public void SelectSupplier(int? index)
		{
			var suppliers = ListOfSuppliers();
			if (index == null)
			{
				index = new Random().Next(suppliers.Count());
			}
			suppliers[(int)index].Click();
		}

		public bool SupplierNameIsDisplayed()
		{
			return Driver.FindElements(Pages.OrderForm.SupplierName).Count == 1;
		}

		public string GetSupplierName()
		{
			return Driver.FindElement(Pages.OrderForm.SupplierName).Text;
		}

		public bool SearchAgainLinkIsDisplayed()
		{
			return Driver.FindElements(Pages.OrderForm.SearchAgainLink).Count == 1;
		}

		public void ClickSearchAgainLink()
		{
			Driver.FindElement(Pages.OrderForm.SearchAgainLink).Click();
		}

		public void ClickSelectDeselectAll()
		{
			Wait.Until(d => d.FindElements(Pages.OrderForm.SelectDeselectAll).Count == 1);
			Driver.FindElement(Pages.OrderForm.SelectDeselectAll).Click();			
		}

		public string GetSelectDeselectAllText()
		{
			return Driver.FindElement(Pages.OrderForm.SelectDeselectAll).Text;
		}

		public int NumberOfCheckboxesDisplayed()
		{
			return Driver.FindElements(Pages.Common.Checkbox).Count;
		}

		public void ClickCheckbox(int index = 0)
		{
			Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
			Driver.FindElements(Pages.Common.Checkbox)[index].Click();
		}

		public bool IsCheckboxChecked(int index = 0)
		{
			Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
			return Driver.FindElements(Pages.Common.Checkbox)[index].GetAttribute("checked") != null;
		}

		public bool ServiceRecipientsNameAndOdsDisplayed()
		{
			return Driver.FindElements(Pages.OrderForm.ServiceRecipientName).Count > 0 && Driver.FindElements(Pages.OrderForm.ServiceRecipientOdsCode).Count > 0;
		}

		public bool AddSolutionButtonDisplayed()
		{
			return Driver.FindElements(Pages.OrderForm.AddSolution).Count == 1;
		}

		public void ClickAddSolutionButton()
		{
			Wait.Until(d => AddSolutionButtonDisplayed());
			Driver.FindElement(Pages.OrderForm.AddSolution).Click();
		}

		public bool NoSolutionsAddedDisplayed()
		{
			return Driver.FindElements(Pages.OrderForm.NoSolutionsAdded).Count == 1;
		}
	}
}
