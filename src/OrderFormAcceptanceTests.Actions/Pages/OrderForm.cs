namespace OrderFormAcceptanceTests.Actions.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Actions.Utils;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Information;

    public class OrderForm : PageAction
    {
        public OrderForm(IWebDriver driver)
            : base(driver)
        {
        }

        public static async Task<Supplier> SupplierHasAdditionalServiceMoreThan1Price(string bapiConnectionString)
        {
            return await SupplierInfo.GetSupplierWithCatalogueItemWithMoreThan1Price(CatalogueItemType.AdditionalService, bapiConnectionString);
        }

        public bool LoggedInDisplayNameIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.Common.LoggedInDisplayName).Text.Contains("Logged in as: ", StringComparison.OrdinalIgnoreCase));
                string loggedInText = Driver.FindElement(Objects.Pages.Common.LoggedInDisplayName).Text;
                string displayName = loggedInText.Split(":")[1].Split("for")[0].Trim();
                string organisationName = loggedInText.Split("for")[1].Trim();
                return displayName != string.Empty && organisationName != string.Empty;
            }
            catch
            {
                return false;
            }
        }

        public bool FooterDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.Footer).Count == 1;
        }

        public bool HeaderDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.Header).Count == 1;
        }

        public string ErrorTitle()
        {
            return Driver.FindElement(Objects.Pages.Common.ErrorTitle).Text;
        }

        public bool ErrorSummaryDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.ErrorSummary).Count > 0;
        }

        public bool ErrorMessagesDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.ErrorMessages).Count > 0;
        }

        public bool NewOrderFormDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.OrderForm.PageTitle).Text.Equals("New Order", StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetCallOffId()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.PageTitle).Text.Split("Order")[1].Trim();
        }

        public string GetCallOffIdSelectSolution()
        {
            var splitTitle = Driver.FindElement(Objects.Pages.OrderForm.PageTitle).Text.Split(" ");
            return splitTitle[^1];
        }

        public void ClickEditAdditionalServices()
        {
            SelectSectionByNameContains("Additional Services").Click();
        }

        public bool TextDisplayedInPageTitle(string expectedValue)
        {
            return Driver.FindElement(Objects.Pages.OrderForm.PageTitle).Text.Contains(expectedValue, StringComparison.OrdinalIgnoreCase);
        }

        public bool TaskListDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.TaskList).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SectionComplete(string section)
        {
            var search = Driver.FindElements(Objects.Pages.OrderForm.GenericSection(section)).Single()
                .FindElement(By.XPath("../.."))
                .FindElements(Objects.Pages.OrderForm.SectionStatus);

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
            Driver.FindElements(Objects.Pages.Common.BackLink).Count.Should().Be(1);
        }

        public bool DeleteOrderButtonIsDisabled()
        {
            try
            {
                Driver.FindElement(Objects.Pages.OrderForm.DeleteOrderButton).FindElement(By.TagName("span"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSolutionButtonIsDisabled()
        {
            try
            {
                Driver.FindElement(Objects.Pages.Common.DeleteSolutionButton).FindElement(By.TagName("span"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PreviewOrderButtonIsDisabled()
        {
            try
            {
                Driver.FindElement(Objects.Pages.OrderForm.PreviewOrderButton).FindElement(By.TagName("span"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetPageTitle()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.PageTitle).Text;
        }

        public bool CompleteOrderButtonIsDisabled()
        {
            try
            {
                Driver.FindElement(Objects.Pages.OrderForm.CompleteOrderLink).FindElement(By.TagName("span"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsRadioButtonSelected(int index = 0)
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > index);
            return Driver.FindElements(Objects.Pages.Common.RadioButton)[index].GetProperty("checked") != null;
        }

        public bool ASecondTabIsOpen()
        {
            return Driver.WindowHandles.Count > 1;
        }

        public bool FindPrintableSummary()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            return Driver.FindElements(Objects.Pages.PrintOrderSummary.PrintableOrderSummary).Count > 0;
        }

        public bool FindPrintPreviewWindow()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            return Driver.FindElements(Objects.Pages.PrintOrderSummary.PrintPreview).Count > 0;
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
                Wait.Until(d => d.FindElement(Objects.Pages.OrderForm.PageTitle).Text.Contains(namedSectionPageTitle, StringComparison.OrdinalIgnoreCase));
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
                .FindElements(Objects.Pages.OrderForm.SectionStatus).Count;

            labelFound.Should().Be(0);
        }

        public void ClickEditCatalogueSolutions()
        {
            SelectSectionByNameContains("Catalogue solution").Click();
        }

        public bool SaveButtonDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.SaveButton).Count == 1;
        }

        public void ClickSaveButton()
        {
            Driver.FindElement(Objects.Pages.Common.SaveButton).Click();
        }

        public bool ContinueButtonDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.ContinueButton).Count == 1;
        }

        public void ClickContinueButton()
        {
            Driver.FindElement(Objects.Pages.Common.ContinueButton).Click();
        }

        public void ClickBackLink()
        {
            Driver.FindElement(Objects.Pages.Common.BackLink).Click();
        }

        public void ClickCompleteOrderLink()
        {
            Driver.FindElement(Objects.Pages.OrderForm.CompleteOrderLink).FindElement(By.TagName("a")).Click();
        }

        public void ClickDeleteButton()
        {
            Driver.FindElement(Objects.Pages.Common.DeleteButton).Click();
        }

        public void EnterTextIntoTextArea(string value, int index = 0)
        {
            Driver.FindElements(Objects.Pages.Common.TextArea)[index].SendKeys(value);
        }

        public void EnterTextIntoTextField(string value, int index = 0)
        {
            Wait.Until(d => d.FindElements(Objects.Pages.Common.TextField).Count > index);
            Driver.FindElements(Objects.Pages.Common.TextField)[index].SendKeys(value);
        }

        public string ClickOnErrorLink()
        {
            var errorMessages = Driver.FindElements(Objects.Pages.Common.ErrorMessages).ToList();
            var index = new Random().Next(errorMessages.Count);

            var linkHref = errorMessages[index].GetAttribute("href");

            errorMessages[index].Click();

            return linkHref;
        }

        public string GetOrderDescription()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.OrderDescription).Count == 1);
            return Driver.FindElement(Objects.Pages.OrderForm.OrderDescription).Text;
        }

        public bool OdsCodeDisplayedAndNotEditable()
        {
            try
            {
                var displayed = Driver.FindElements(Objects.Pages.OrderForm.OrganisationOdsCode).Count == 1;
                var notEditable = Driver.FindElement(Objects.Pages.OrderForm.OrganisationOdsCode).TagName == "div";
                return displayed && notEditable;
            }
            catch
            {
                return false;
            }
        }

        public bool OrganisationNameDisplayedAndNotEditable()
        {
            try
            {
                var displayed = Driver.FindElements(Objects.Pages.OrderForm.OrganisationName).Count == 1;
                var notEditable = Driver.FindElement(Objects.Pages.OrderForm.OrganisationName).TagName == "div";
                return displayed && notEditable;
            }
            catch
            {
                return false;
            }
        }

        public bool AddressDisplayedAndNotEditable()
        {
            try
            {
                var lineDisplayed = Driver.FindElements(Objects.Pages.OrderForm.AddressLine(1)).Count == 1;
                var lineNotEditable = Driver.FindElement(Objects.Pages.OrderForm.AddressLine(1)).TagName == "div";
                var townDisplayed = Driver.FindElements(Objects.Pages.OrderForm.AddressTown).Count == 1;
                var townNotEditable = Driver.FindElement(Objects.Pages.OrderForm.AddressTown).TagName == "div";
                var countyDisplayed = Driver.FindElements(Objects.Pages.OrderForm.AddressCounty).Count == 1;
                var countyNotEditable = Driver.FindElement(Objects.Pages.OrderForm.AddressCounty).TagName == "div";
                var postcodeDisplayed = Driver.FindElements(Objects.Pages.OrderForm.AddressPostcode).Count == 1;
                var postcodeNotEditable = Driver.FindElement(Objects.Pages.OrderForm.AddressPostcode).TagName == "div";
                return lineDisplayed && lineNotEditable && townDisplayed && townNotEditable && countyDisplayed && countyNotEditable && postcodeDisplayed && postcodeNotEditable;
            }
            catch
            {
                return false;
            }
        }

        public string GetOdsCode()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.OrganisationOdsCode).Text;
        }

        public string GetOrganisationName()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.OrganisationName).Text;
        }

        public bool PracticeListSizeInputIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.PracticeListSizeInput).Count > 0;
        }

        public Address GetAddress()
        {
            return new Address
            {
                Line1 = Driver.FindElement(Objects.Pages.OrderForm.AddressLine(1)).Text,
                Line2 = Driver.FindElement(Objects.Pages.OrderForm.AddressLine(2)).Text,
                Line3 = Driver.FindElement(Objects.Pages.OrderForm.AddressLine(3)).Text,
                Line4 = Driver.FindElement(Objects.Pages.OrderForm.AddressLine(4)).Text,
                Town = Driver.FindElement(Objects.Pages.OrderForm.AddressTown).Text,
                County = Driver.FindElement(Objects.Pages.OrderForm.AddressCounty).Text,
                Postcode = Driver.FindElement(Objects.Pages.OrderForm.AddressPostcode).Text,
                Country = Driver.FindElement(Objects.Pages.OrderForm.AddressCountry).Text,
            };
        }

        public void EnterContact(Contact contact)
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.ContactFirstName).Count == 1);
            Driver.FindElement(Objects.Pages.OrderForm.ContactFirstName).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.ContactFirstName).SendKeys(contact.FirstName);
            Driver.FindElement(Objects.Pages.OrderForm.ContactLastName).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.ContactLastName).SendKeys(contact.LastName);
            Driver.FindElement(Objects.Pages.OrderForm.ContactEmail).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.ContactEmail).SendKeys(contact.Email);
            Driver.FindElement(Objects.Pages.OrderForm.ContactTelephone).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.ContactTelephone).SendKeys(contact.Phone);
        }

        public Domain.Contact GetContact()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.ContactFirstName).Count == 1);
            return new Domain.Contact()
            {
                FirstName = Driver.FindElement(Objects.Pages.OrderForm.ContactFirstName).GetAttribute("value"),
                LastName = Driver.FindElement(Objects.Pages.OrderForm.ContactLastName).GetAttribute("value"),
                Email = Driver.FindElement(Objects.Pages.OrderForm.ContactEmail).GetAttribute("value"),
                Phone = Driver.FindElement(Objects.Pages.OrderForm.ContactTelephone).GetAttribute("value"),
            };
        }

        public void ClickSearchButton()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.SearchButton).Count == 1);
            Driver.FindElement(Objects.Pages.OrderForm.SearchButton).Click();
        }

        public ReadOnlyCollection<IWebElement> ListOfSuppliers()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.SupplierOptions);
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

        public bool IsPlannedDeliveryDateDisplayed()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.PageTitle).Text.Contains("planned delivery", StringComparison.OrdinalIgnoreCase);
        }

        public bool SupplierNameIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.SupplierName).Count == 1;
        }

        public string GetSupplierName()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.SupplierName).Text;
        }

        public bool SearchAgainLinkIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.SearchAgainLink).Count == 1;
        }

        public void ClickSearchAgainLink()
        {
            Driver.FindElement(Objects.Pages.OrderForm.SearchAgainLink).Click();
        }

        public void ClickSelectDeselectAll()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.SelectDeselectAll).Count == 1);
            Driver.FindElement(Objects.Pages.OrderForm.SelectDeselectAll).Click();
        }

        public string GetSelectDeselectAllText()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.SelectDeselectAll).Text;
        }

        public int NumberOfCheckboxesDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.Checkbox).Count;
        }

        public string ClickCheckbox(int index = 0)
        {
            Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
            var element = Driver.FindElements(Objects.Pages.Common.Checkbox)[index];
            element.Click();
            return element.GetAttribute("value");
        }

        public string ClickCheckboxReturnName(int index = 0)
        {
            Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
            var element = Driver.FindElements(Objects.Pages.Common.Checkbox)[index];
            element.Click();
            Wait.Until(d => bool.Parse(d.FindElements(Objects.Pages.Common.Checkbox)[index].GetProperty("checked")));
            return element.GetAttribute("name");
        }

        public bool IsCheckboxChecked(int index = 0)
        {
            Wait.Until(d => NumberOfCheckboxesDisplayed() > index);
            return Driver.FindElements(Objects.Pages.Common.Checkbox)[index].GetAttribute("checked") != null;
        }

        public bool ServiceRecipientsNameAndOdsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.ServiceRecipientName).Count > 0 && Driver.FindElements(Objects.Pages.OrderForm.ServiceRecipientOdsCode).Count > 0;
        }

        public bool AddSolutionButtonDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.AddSolution).Count == 1;
        }

        public void ClickAddSolutionButton()
        {
            Wait.Until(d => AddSolutionButtonDisplayed());
            Driver.FindElement(Objects.Pages.OrderForm.AddSolution).Click();
        }

        public bool NoSolutionsAddedDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.NoSolutionsAdded).Count == 1;
        }

        public bool AddedOrderItemsTableIsPopulated()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.AddedOrderItemsTable)
                .FindElements(Objects.Pages.Common.TableRowX(0))
                .Count > 0;
        }

        public bool AddedOrderItemNameIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.AddedOrderItemName).Count == 1;
        }

        public bool AddedOrderItemNamesAreLinks()
        {
            var names = Driver.FindElements(Objects.Pages.OrderForm.AddedOrderItemName);
            var countOfNames = names.Count;
            var countOfLinks = names.Select(n => n.GetAttribute("href")).Count();
            return countOfNames == countOfLinks;
        }

        public void ClickAddedCatalogueItem(int index = 0)
        {
            Driver.FindElements(Objects.Pages.OrderForm.AddedOrderItemName)[index].Click();
        }

        public void ClickTableRowLink(int index = 0)
        {
            Driver.FindElements(Objects.Pages.Common.TableRows)[index].FindElement(By.TagName("a")).Click();
        }

        public string GetAddedSolutionServiceRecipient()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.AddedSolutionServiceRecipient).Text;
        }

        public int GetNumberOfAddedRecipients()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.AddedSolutionServiceRecipient).Count;
        }

        public int NumberOfRadioButtonsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.Common.RadioButton).Count;
        }

        public string ClickRadioButton(int index = 0)
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > index);
            var element = Driver.FindElements(Objects.Pages.Common.RadioButton)[index];
            element.Click();
            Wait.Until(s => bool.Parse(s.FindElements(Objects.Pages.Common.RadioButton)[index].GetProperty("checked")));
            return element.GetAttribute("value");
        }

        public string ClickRadioButtonWithText(string text)
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > 0);
            var elementIndex = Driver.FindElements(Objects.Pages.Common.RadioButtonLabel)
                .IndexOf(Driver.FindElements(Objects.Pages.Common.RadioButtonLabel)
                    .Single(s => s.Text.Contains(text, StringComparison.OrdinalIgnoreCase)));

            return ClickRadioButton(elementIndex);
        }

        public string GetSelectedRadioButton()
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > 0);
            var value = Driver.FindElements(Objects.Pages.Common.RadioButton).Where(e => bool.Parse(e.GetProperty("checked"))).Select(s => s.GetAttribute("value")).Single();
            return value;
        }

        public IEnumerable<IWebElement> GetRadioButtonValues()
        {
            Wait.Until(d => NumberOfRadioButtonsDisplayed() > 0);
            var values = Driver.FindElements(Objects.Pages.Common.RadioButton);
            return values;
        }

        public List<string> GetRadioButtonText()
        {
            return Driver.FindElements(Objects.Pages.Common.RadioButtonLabel).Select(s => s.Text).ToList();
        }

        public bool PriceInputIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.PriceInput).Count == 1;
        }

        public string GetPriceInputValue()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.PriceInput).GetAttribute("value");
        }

        public void EnterPriceInputValue(string value)
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.PriceInput).Count == 1);
            Driver.FindElement(Objects.Pages.OrderForm.PriceInput).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.PriceInput).SendKeys(value);
        }

        public bool OrderUnitIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.OrderUnit).Count == 1;
        }

        public bool QuantityInputIsDisplayed(int expected = 1)
        {
            return Driver.FindElements(Objects.Pages.OrderForm.Quantity).Count == expected;
        }

        public void EnterQuantity(string value, string quantityLabel = "quantity")
        {
            Wait.Until(d => d.FindElements(By.Id(quantityLabel)).Count > 0);
            var quantities = Driver.FindElements(By.Id(quantityLabel));
            foreach (var quantity in quantities)
            {
                quantity.Clear();
                quantity.SendKeys(value);
            }
        }

        public string GetQuantity()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.Quantity).Count == 1);
            return Driver.FindElement(Objects.Pages.OrderForm.Quantity).GetAttribute("value");
        }

        public bool ProposedDateInputIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.OrderDate).Count == 1;
        }

        public void EnterProposedDate(DateTime value)
        {
            EnterProposedDate(value.Year.ToString(), value.Month.ToString(), value.Day.ToString());
        }

        public void EnterProposedDate(string year, string month, string day)
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.OrderDateDay).Count == 1);
            Driver.FindElement(Objects.Pages.OrderForm.OrderDateDay).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.OrderDateMonth).Clear();
            Driver.FindElement(Objects.Pages.OrderForm.OrderDateYear).Clear();
            Driver.EnterTextViaJs(Wait, Objects.Pages.OrderForm.OrderDateDay, day);
            Driver.EnterTextViaJs(Wait, Objects.Pages.OrderForm.OrderDateMonth, month);
            Driver.EnterTextViaJs(Wait, Objects.Pages.OrderForm.OrderDateYear, year);
        }

        public string GetProposedDate()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.OrderDateDay).Count == 1);
            var day = Driver.FindElement(Objects.Pages.OrderForm.OrderDateDay).GetAttribute("value");
            var month = Driver.FindElement(Objects.Pages.OrderForm.OrderDateMonth).GetAttribute("value");
            var year = Driver.FindElement(Objects.Pages.OrderForm.OrderDateYear).GetAttribute("value");
            return string.Join(" ", day, month, year);
        }

        public bool EstimationPeriodIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.EstimationPeriod).Count == 1;
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
            Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.PreviewOrderButton).Count == 1);

            Driver.FindElement(Objects.Pages.OrderForm.PreviewOrderButton)
                .FindElement(By.TagName("a"))
                .Click();
        }

        public bool AddAssociatedServiceButtonDisplayed()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.AddSolution).Count == 1;
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
            var titles = Driver.FindElements(Objects.Pages.OrderForm.SectionDescription);
            var selectedSection = titles.Single(s => s.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            return selectedSection;
        }

        public bool SectionIsDisplayed(string text)
        {
            var titles = Driver.FindElements(Objects.Pages.OrderForm.SectionDescription);
            var selectedSection = titles.Where(s => s.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            return selectedSection.Any();
        }

        public void ClickDeleteCatalogueSolutionButton()
        {
            Driver.FindElement(Objects.Pages.Common.DeleteSolutionButton).Click();
        }

        public IEnumerable<string> GetAddedCatalogueItems()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.AddedOrderItemName).Select(s => s.Text);
        }

        public void ClickEditServiceRecipientsButton()
        {
            Driver.FindElement(Objects.Pages.OrderForm.EditServiceRecipientsButton).Click();
            Wait.Until(s => TextDisplayedInPageTitle("Service Recipients"));
        }

        public void ClickUnselectedCheckbox()
        {
            var checkedCheckboxes = Driver.FindElements(Objects.Pages.Common.Checkbox).Where(s => bool.Parse(s.GetProperty("checked"))).Count();
            var checkboxes = Driver.FindElements(Objects.Pages.Common.Checkbox);
            foreach (var checkbox in checkboxes)
            {
                if (!bool.Parse(checkbox.GetProperty("checked")))
                {
                    checkbox.Click();

                    if (bool.Parse(checkbox.GetProperty("checked")))
                    {
                        Wait.Until(d => d.FindElements(Objects.Pages.Common.Checkbox).Where(s => bool.Parse(s.GetProperty("checked"))).Count() == checkedCheckboxes + 1);
                        break;
                    }
                }
            }
        }

        public void ClickCancelDelete()
        {
            Driver.FindElement(Objects.Pages.OrderForm.CancelDeleteLink).Click();
        }

        public string DeleteConfirmationOrderDescription()
        {
            return Driver.FindElement(Objects.Pages.Common.DeleteConfirmationOrderDescription).Text;
        }

        public async Task SelectSupplierWithContactDetails(string connectionString)
        {
            var suppliers = ListOfSupplierNames();
            var supplierNames = suppliers.Select(s => s.Text);

            Random rng = new();
            var supplierListShuffle = supplierNames
                .Select(x => new { value = x, order = rng.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.value)
                .ToList();

            string selectedSupplier = string.Empty;

            foreach (var supplier in supplierListShuffle)
            {
                var supplierResult = await SupplierInfo.GetSupplierWithContactDetails(connectionString, supplier);
                if (supplierResult)
                {
                    selectedSupplier = supplier;
                    break;
                }
            }

            if (string.IsNullOrEmpty(selectedSupplier))
            {
                throw new NullReferenceException(nameof(selectedSupplier));
            }

            suppliers.Single(s => s.Text == selectedSupplier).Click();
        }

        public string DeleteSolutionConfirmationTitle()
        {
            return Driver.FindElement(Objects.Pages.OrderForm.DeleteSolutionConfirmation).Text;
        }

        private ReadOnlyCollection<IWebElement> ListOfSupplierNames()
        {
            return Driver.FindElements(Objects.Pages.OrderForm.SupplierOptionsLabels);
        }
    }
}
