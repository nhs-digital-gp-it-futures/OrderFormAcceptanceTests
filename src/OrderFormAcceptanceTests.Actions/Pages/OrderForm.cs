using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.TestData;
using OrderFormAcceptanceTests.TestData.Information;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrderFormAcceptanceTests.Objects.Utils;

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

        public void ClickEditAdditionalServices()
        {
            SelectSectionByNameContains("Additional Services").Click();
        }

        public bool TextDisplayedInPageTitle(string expectedValue)
        {
            return Driver.FindElement(Pages.OrderForm.PageTitle).Text.Contains(expectedValue, StringComparison.OrdinalIgnoreCase);
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

        public bool DeleteSolutionButtonIsDisabled()
        {
            return Driver.FindElement(Pages.Common.DeleteSolutionButton).GetAttribute("aria-disabled") != null;
        }

        public bool PreviewOrderButtonIsDisabled()
        {
            return Driver.FindElement(Pages.OrderForm.PreviewOrderButton).FindElement(By.TagName("a")).GetAttribute("aria-disabled") != null;
        }

        public void SelectSupplierWithContactDetails(string bapiConnString)
        {
            var suppliers = ListOfSupplierNames();
            var supplierNames = suppliers.Select(s => s.Text);

            // Randomise ordering of list so a random supplier is chosen each time
            Random rng = new Random();
            var supplierListShuffle = supplierNames
                .Select(x => new { value = x, order = rng.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.value)
                .ToList();

            string selectedSupplier = string.Empty;

            foreach (var supplier in supplierListShuffle)
            {
                if (SupplierInfo.SupplierHasContactInfo(bapiConnString, supplier))
                {
                    selectedSupplier = supplier;
                    break;
                }
            }

            if (string.IsNullOrEmpty(selectedSupplier)) throw new ArgumentNullException(nameof(selectedSupplier));

            suppliers.Single(s => s.Text == selectedSupplier).Click();
        }

        private ReadOnlyCollection<IWebElement> ListOfSupplierNames()
        {
            return Driver.FindElements(Pages.OrderForm.SupplierOptionsLabels);
        }

        public bool CompleteOrderButtonIsDisabled()
        {
            return Driver.FindElement(Pages.OrderForm.CompleteOrderLink).GetAttribute("aria-disabled") != null;
        }

        public void DeleteOrderButtonHasAltTest()
        {
            Driver.FindElement(Pages.OrderForm.DeleteOrderButton).GetAttribute("aria-label").Length.Should().BeGreaterThan(0);
        }

        public bool IsRadioButtonSelected(int index = 0)
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > index);
            return Driver.FindElements(Pages.Common.RadioButton)[index].GetProperty("checked") != null;
        }

        public bool ASecondTabIsOpen()
        {
            return Driver.WindowHandles.Count > 1;
        }

        public bool FindPrintableSummary()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            return Driver.FindElements(Pages.PrintOrderSummary.PrintableOrderSummary).Count > 0;
        }

        public bool FindPrintPreviewWindow()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            return Driver.FindElements(Pages.PrintOrderSummary.PrintPreview).Count > 0;
        }

        public void PreviewOrderButtonHasAltTest()
        {
            Driver.FindElement(Pages.OrderForm.PreviewOrderButton).GetAttribute("aria-label").Length.Should().BeGreaterThan(0);
        }

        public void CompleteOrderButtonHasAltTest()
        {
            Driver.FindElement(Pages.OrderForm.CompleteOrderLabel).GetAttribute("aria-label").Length.Should().BeGreaterThan(0);
        }

        public bool EditCommencementDateSectionDisplayed()
        {
            try
            {
                Wait.Until(d => SectionIsDisplayed("commencement date"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickEditCommencementDate()
        {
            SelectSectionByNameContains("commencement date").Click();
        }

        public bool EditDescriptionSectionDisplayed()
        {

            try
            {
                Wait.Until(d => SectionIsDisplayed("Description"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickEditDescription()
        {
            SelectSectionByNameContains("Description").Click();
        }

        public bool EditCallOffOrderingPartySectionDisplayed()
        {

            try
            {
                Wait.Until(d => SectionIsDisplayed("Call-Off Ordering"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CallOffOrderingPartyEnabled()
        {
            return SelectSectionByNameContains("Call-Off Ordering").TagName == "a";
        }

        public bool OrderDescriptionEnabled()
        {
            return SelectSectionByNameContains("Description").TagName == "a";
        }

        public bool SupplierInformationEnabled()
        {
            return SelectSectionByNameContains("Supplier information").TagName == "a";
        }

        public void ClickEditCallOffOrderingParty()
        {
            SelectSectionByNameContains("Call-Off Ordering").Click();
        }

        public bool CommencementDateEnabled()
        {
            return SelectSectionByNameContains("commencement date").TagName == "a";
        }

        public bool EditSupplierSectionDisplayed()
        {
            try
            {
                Wait.Until(d => SectionIsDisplayed("Supplier information"));
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void ClickEditSupplier()
        {
            SelectSectionByNameContains("Supplier information").Click();
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

        public bool EditCatalogueSolutionsSectionDisplayed()
        {
            try
            {
                Wait.Until(d => SectionIsDisplayed("Catalogue solution"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditCatalogueSolutionsSectionIsEnabled()
        {
            return SelectSectionByNameContains("Catalogue solution").TagName == "a";
        }

        public void AssertThatEditCatalogueSolutionsSectionIsNotComplete()
        {
            var labelFound = SelectSectionByNameContains("Catalogue solution")
                .FindElement(By.XPath("../.."))
                .FindElements(Pages.OrderForm.SectionStatus).Count;

            labelFound.Should().Be(0);
        }

        public void ClickEditCatalogueSolutions()
        {
            SelectSectionByNameContains("Catalogue solution").Click();
        }

        public bool SaveButtonDisplayed()
        {
            return Driver.FindElements(Pages.Common.SaveButton).Count == 1;
        }

        public void ClickSaveButton()
        {
            Driver.FindElement(Pages.Common.SaveButton).Click();
        }

        public bool ContinueButtonDisplayed()
        {
            return Driver.FindElements(Pages.Common.ContinueButton).Count == 1;
        }

        public void ClickContinueButton()
        {
            Driver.FindElement(Pages.Common.ContinueButton).Click();
        }

        public void ClickBackLink()
        {
            Driver.FindElement(Pages.Common.BackLink).Click();
        }

        public void ClickCompleteOrderLink()
        {
            Driver.FindElement(Pages.OrderForm.CompleteOrderLink).Click();
        }

        public void ClickDeleteButton()
        {
            Driver.FindElement(Pages.Common.DeleteButton).Click();
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

            if (index.HasValue)
            {
                suppliers[index.Value].Click();
            }
            else
            {
                RandomInformation.GetRandomItem(suppliers).Click();
            }
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

        public string ClickCheckbox(int index = 0)
        {
            Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
            var element = Driver.FindElements(Pages.Common.Checkbox)[index];
            element.Click();
            return element.GetAttribute("value");
        }

        public string ClickCheckboxReturnName(int index = 0)
        {
            Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
            var element = Driver.FindElements(Pages.Common.Checkbox)[index];
            element.Click();
            return element.GetAttribute("name");
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

        public bool AddedOrderItemsTableIsPopulated()
        {
            return Driver.FindElement(Pages.OrderForm.AddedOrderItemsTable)
                .FindElements(Pages.Common.TableRowX(0))
                .Count > 0;
        }

        public bool AddedOrderItemNameIsDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.AddedOrderItemName).Count == 1;
        }

        public bool AddedOrderItemNamesAreLinks()
        {
            var names = Driver.FindElements(Pages.OrderForm.AddedOrderItemName);
            var countOfNames = names.Count;
            var countOfLinks = names.Select(n => n.GetAttribute("href")).Count();
            return countOfNames == countOfLinks;
        }

        public void ClickAddedCatalogueItem(int index = 0)
        {
            Driver.FindElements(Pages.OrderForm.AddedOrderItemName)[index].Click();
        }

        public void ClickTableRowLink(int index = 0)
        {
            Driver.FindElements(Pages.Common.TableRows)[0].FindElement(By.TagName("a")).Click();
        }

        public string GetAddedSolutionServiceRecipient()
        {
            return Driver.FindElement(Pages.OrderForm.AddedSolutionServiceRecipient).Text;
        }

        public int NumberOfRadioButtonsDisplayed()
        {
            return Driver.FindElements(Pages.Common.RadioButton).Count;
        }

        public string ClickRadioButton(int index = 0)
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > index);
            var element = Driver.FindElements(Pages.Common.RadioButton)[index];
            element.Click();
            return element.GetAttribute("value");
        }

        public string ClickRadioButtonWithText(string text)
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > 0);
            var elementIndex = Driver.FindElements(Pages.Common.RadioButtonLabel)
                .IndexOf(Driver.FindElements(Pages.Common.RadioButtonLabel)
                    .Single(s => s.Text.Contains(text, StringComparison.OrdinalIgnoreCase)));

            return ClickRadioButton(elementIndex);
        }

        public string GetSelectedRadioButton()
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > 0);
            var value = Driver.FindElements(Pages.Common.RadioButton).Where(e => e.GetProperty("checked") == "True").Select(s => s.GetAttribute("value")).Single();
            return value;
        }

        public List<string> GetRadioButtonText()
        {
            return Driver.FindElements(Pages.Common.RadioButtonLabel).Select(s => s.Text).ToList();
        }

        public bool PriceInputIsDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.PriceInput).Count == 1;
        }

        public string GetPriceInputValue()
        {
            return Driver.FindElement(Pages.OrderForm.PriceInput).GetAttribute("value");
        }

        public void EnterPriceInputValue(string value)
        {
            Wait.Until(d => d.FindElements(Pages.OrderForm.PriceInput).Count == 1);
            Driver.FindElement(Pages.OrderForm.PriceInput).Clear();
            Driver.FindElement(Pages.OrderForm.PriceInput).SendKeys(value);
        }

        public bool OrderUnitIsDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.OrderUnit).Count == 1;
        }

        public bool QuantityInputIsDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.Quantity).Count == 1;
        }

        public void EnterQuantity(string value)
        {
            Wait.Until(d => d.FindElements(Pages.OrderForm.Quantity).Count == 1);
            Driver.FindElement(Pages.OrderForm.Quantity).Clear();
            Driver.FindElement(Pages.OrderForm.Quantity).SendKeys(value);
        }

        public string GetQuantity()
        {
            Wait.Until(d => d.FindElements(Pages.OrderForm.Quantity).Count == 1);
            return Driver.FindElement(Pages.OrderForm.Quantity).GetAttribute("value");
        }

        public bool ProposedDateInputIsDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.OrderDate).Count == 1;
        }

        public void EnterProposedDate(DateTime value)
        {
            EnterProposedDate(value.Year.ToString(), value.Month.ToString(), value.Day.ToString());
        }

        public void EnterProposedDate(string year, string month, string day)
        {
            Wait.Until(d => d.FindElements(Pages.OrderForm.OrderDateDay).Count == 1);
            Driver.FindElement(Pages.OrderForm.OrderDateDay).Clear();
            Driver.FindElement(Pages.OrderForm.OrderDateMonth).Clear();
            Driver.FindElement(Pages.OrderForm.OrderDateYear).Clear();
            Driver.EnterTextViaJs(Wait, Pages.OrderForm.OrderDateDay, day);
            Driver.EnterTextViaJs(Wait, Pages.OrderForm.OrderDateMonth, month);
            Driver.EnterTextViaJs(Wait, Pages.OrderForm.OrderDateYear, year);
        }

        public string GetProposedDate()
        {
            Wait.Until(d => d.FindElements(Pages.OrderForm.OrderDateDay).Count == 1);
            var day = Driver.FindElement(Pages.OrderForm.OrderDateDay).GetAttribute("value");
            var month = Driver.FindElement(Pages.OrderForm.OrderDateMonth).GetAttribute("value");
            var year = Driver.FindElement(Pages.OrderForm.OrderDateYear).GetAttribute("value");
            return string.Join(" ", day, month, year);
        }

        public bool EstimationPeriodIsDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.EstimationPeriod).Count == 1;
        }

        public bool EditAdditionalServicesSectionDisplayed()
        {
            try
            {
                Wait.Until(s => SectionIsDisplayed("Additional Services"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditAdditionalServicesSectionIsEnabled()
        {
            return SelectSectionByNameContains("Additional Services").TagName == "a";
        }

        public bool EditAssociatedServicesSectionDisplayed()
        {
            try
            {
                Wait.Until(d => SectionIsDisplayed("Associated Services"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickEditAssociatedServices()
        {
            SelectSectionByNameContains("Associated Services").Click();
        }

        public bool EditAssociatedServicesSectionIsEnabled()
        {
            return SelectSectionByNameContains("Associated Services").TagName == "a";
        }

        public void ClickPreviewOrderButton()
        {
            Wait.Until(d => d.FindElements(Pages.OrderForm.PreviewOrderButton).Count == 1);

            Driver.FindElement(Pages.OrderForm.PreviewOrderButton)
                .FindElement(By.TagName("a"))
                .Click();
        }

        public bool AddAssociatedServiceButtonDisplayed()
        {
            return Driver.FindElements(Pages.OrderForm.AddSolution).Count == 1;
        }

        public bool EditFundingSourceSectionDisplayed()
        {
            try
            {
                Wait.Until(d => SectionIsDisplayed("Explain how you're paying for this order"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickEditFundingSource()
        {
            SelectSectionByNameContains("Explain how you're paying for this order").Click();
        }

        public bool EditFundingSourceSectionIsEnabled()
        {
            return SelectSectionByNameContains("Explain how you're paying for this order").TagName == "a";
        }

        public IWebElement SelectSectionByNameContains(string text)
        {
            var titles = Driver.FindElements(Pages.OrderForm.SectionDescription);
            var selectedSection = titles.Single(s => s.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            return selectedSection;
        }

        public bool SectionIsDisplayed(string text)
        {
            var titles = Driver.FindElements(Pages.OrderForm.SectionDescription);
            var selectedSection = titles.Where(s=>s.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            return selectedSection.Count() > 0;
        }
    }
}
