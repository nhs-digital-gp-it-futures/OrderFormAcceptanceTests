using Bogus;
using Bogus.Extensions;
using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class CatalogueSolutions : TestBase
    {

        public CatalogueSolutions(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Given(@"there are one or more Service Recipients in the order")]
        public void GivenThereAreOneOrMoreServiceRecipientsInTheOrder()
        {
            Context.ContainsKey("CreatedServiceRecipient").Should().BeTrue();
        }

        [Given(@"there are no Service Recipients in the order")]
        public void GivenThereAreNoServiceRecipientsInTheOrder()
        {
            var order = (Order)Context["CreatedOrder"];
            order.ServiceRecipientsViewed.Should().Be(1);

            var serviceRecipient = (ServiceRecipient)Context["CreatedServiceRecipient"];
            serviceRecipient.Delete(Test.ConnectionString);
            Context.Remove("CreatedServiceRecipient");
        }

        [Given(@"there is no Catalogue Solution in the order")]
        public void GivenThereIsNoCatalogueSolutionInTheOrder()
        {

            Context.Should().NotContainKey("CreatedOrderItem");

            var order = (Order)Context["CreatedOrder"];
            var searchedOrderItem = new OrderItem().RetrieveByOrderId(Test.ConnectionString, order.OrderId);
            searchedOrderItem.Should().BeEmpty();
        }

        [Then(@"the User is able to manage the Catalogue Solutions section")]
        public void ThenTheUserIsAbleToManageTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.ClickEditCatalogueSolutions();
            ThenTheCatalogueSolutionDashboardIsPresented();
        }

        [Then(@"the User is not able to manage the Catalogue Solutions section")]
        [Then(@"the Catalogue Solution section is disabled")]
        public void ThenTheUserIsNotAbleToManageTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.EditCatalogueSolutionsSectionIsEnabled().Should().BeFalse();
        }

        [Then(@"the Catalogue Solution section is not complete")]
        public void ThenTheCatalogueSolutionSectionIsNotComplete()
        {
            Test.Pages.OrderForm.AssertThatEditCatalogueSolutionsSectionIsNotComplete();
        }

        [StepDefinition(@"the User has chosen to manage the Catalogue Solution section")]
        public void WhenTheUserHasChosenToManageTheCatalogueSolutionSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheCatalogueSolutionsSection();
        }

        [StepDefinition(@"the Catalogue Solution dashboard is presented")]
        public void ThenTheCatalogueSolutionDashboardIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Catalogue Solution").Should().BeTrue();
        }

        [Then(@"there is a control to add a Catalogue Solution")]
        public void ThenThereIsAControlToAddACatalogueSolution()
        {
            Test.Pages.OrderForm.AddSolutionButtonDisplayed().Should().BeTrue();
        }

        [Then(@"they are presented with the Catalogue Solutions available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheCatalogueSolutionsAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Catalogue Solution").Should().BeTrue();
        }

        [Given(@"the User is presented with Catalogue Solutions available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithCatalogueSolutionsAvailableFromTheirChosenSupplier()
        {
            WhenTheUserHasChosenToManageTheCatalogueSolutionSection();
            new CommonSteps(Test, Context).WhenTheUserChoosesToAddASingleCatalogueSolution();
            ThenTheyArePresentedWithTheCatalogueSolutionsAvailableFromTheirChosenSupplier();
        }

        [Then(@"the User is informed they have to select a Catalogue Solution")]
        public void ThenTheUserIsInformedTheyHaveToSelectACatalogueSolution()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectSolution");
        }

        [Then(@"the User is informed they have to select a Catalogue Solution price")]
        public void ThenTheUserIsInformedTheyHaveToSelectACatalogueSolutionPrice()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectSolutionPrice");
        }

        [Given(@"the User selects a catalogue solution to add")]
        public void GivenTheUserSelectsACatalogueSolutionToAdd()
        {
            var solutionId = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add("ChosenSolutionId", solutionId);
        }

        [Then(@"all the available prices for that Catalogue Solution are presented")]
        public void ThenAllTheAvailablePricesForThatCatalogueSolutionArePresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("List price").Should().BeTrue();
            var SolutionId = (string)Context["ChosenSolutionId"];
            var query = "Select count(*) FROM [dbo].[CataloguePrice] where CatalogueItemId=@SolutionId";
            var expectedNumberOfPrices = SqlExecutor.Execute<int>(Test.BapiConnectionString, query, new { SolutionId }).Single();
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().Be(expectedNumberOfPrices);
        }

        [Given(@"the User is presented with the prices for the selected Catalogue Solution")]
        public void GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution()
        {
            GivenTheUserIsPresentedWithCatalogueSolutionsAvailableFromTheirChosenSupplier();
            GivenTheUserSelectsACatalogueSolutionToAdd();
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
        }

        [Given(@"the User selects a price")]
        public void GivenTheUserSelectsAPrice()
        {
            Test.Pages.OrderForm.ClickRadioButton(2);
        }

        [Given(@"a Service Recipient is selected")]
        public void GivenAServiceRecipientIsSelected()
        {
            var odsCode = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add("ChosenOdsCode", odsCode);
        }

        [Given(@"the User is presented with the Service Recipients saved in the Order after selecting the variable flat price")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrder()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            GivenTheUserSelectsAPrice();
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
        }

        [Given(@"the User is presented with the Service Recipients saved in the Order after selecting the per patient flat price")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrderAfterSelectingThePerPatientFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton();
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
        }

        [Then(@"the User is informed they have to select a Service Recipient")]
        public void ThenTheUserIsInformedTheyHaveToSelectAServiceRecipient()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectRecipient");
        }

        [Then(@"they are presented with the Associated Service edit form")]
        [Then(@"they are presented with the Catalogue Solution edit form")]
        public void ThenTheyArePresentedWithTheOrderItemPriceEditForm()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("information for").Should().BeTrue();
        }

        [Then(@"the name of the selected Catalogue Solution is displayed on the Catalogue Solution edit form")]
        public void ThenTheNameOfTheSelectedCatalogueSolutionIsDisplayedOnTheCatalogueSolutionEditForm()
        {
            var SolutionId = (string)Context["ChosenSolutionId"];
            var query = "Select Name FROM [dbo].[CatalogueItem] where CatalogueItemId=@SolutionId";
            var expectedSolutionName = SqlExecutor.Execute<string>(Test.BapiConnectionString, query, new { SolutionId }).Single();
            Test.Pages.OrderForm.TextDisplayedInPageTitle(expectedSolutionName).Should().BeTrue();
        }

        [Then(@"the selected Service Recipient with their ODS code is displayed on the Catalogue Solution edit form")]
        public void ThenTheSelectedServiceRecipientWithTheirODSCodeIsDisplayedOnTheCatalogueSolutionEditForm()
        {
            var ChosenOdsCode = (string)Context["ChosenOdsCode"];
            var query = "Select Name FROM [dbo].[Organisations] where OdsCode=@ChosenOdsCode";
            var expectedOrganisationName = SqlExecutor.Execute<string>(Test.IsapiConnectionString, query, new { ChosenOdsCode }).Single();
            var expectedFormattedValue = string.Format("{0} ({1})", expectedOrganisationName, ChosenOdsCode);
            Test.Pages.OrderForm.TextDisplayedInPageTitle(expectedFormattedValue).Should().BeTrue();
        }

        [Then(@"the Associated Service edit form contains an input for the price")]
        [Then(@"the Catalogue Solution edit form contains an input for the price")]
        public void ThenTheCatalogueSolutionEditFormContainsAnInputForThePrice()
        {
            Test.Pages.OrderForm.PriceInputIsDisplayed().Should().BeTrue();
        }

        [Then(@"the price input is autopopulated with the list price for the flat list price selected")]
        public void ThenThePriceInputIsAutopopulatedWithTheListPriceForTheFlatListPriceSelected()
        {
            Test.Pages.OrderForm.GetPriceInputValue().Should().NotBeNullOrEmpty();            
        }

        [Then(@"the item on the Associated Service edit form contains a unit of order")]
        [Then(@"the item on the Catalogue Solution edit form contains a unit of order")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsAUnitOfOrder()
        {
            Test.Pages.OrderForm.OrderUnitIsDisplayed().Should().BeTrue();
        }

        [Then(@"the item on the Associated Service edit form contains an input for the quantity")]
        [Then(@"the item on the Catalogue Solution edit form contains an input for the quantity")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsAnInputForTheQuantity()
        {
            Test.Pages.OrderForm.QuantityInputIsDisplayed().Should().BeTrue();
        }

        [Then(@"the item on the Catalogue Solution edit form contains an input for date")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsAnInputForDate()
        {
            Test.Pages.OrderForm.ProposedDateInputIsDisplayed().Should().BeTrue();
        }

        [Then(@"the item on the Associated Service edit form contains a selection for the quantity estimation period")]
        [Then(@"the item on the Catalogue Solution edit form contains a selection for the quantity estimation period")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsASelectionForTheQuantityEstimationPeriod()
        {
            Test.Pages.OrderForm.EstimationPeriodIsDisplayed().Should().BeTrue();
        }

        [Then(@"the delete button is disabled")]
        public void ThenTheDeleteButtonIsDisabled()
        {
            Test.Pages.OrderForm.DeleteSolutionButtonIsDisabled().Should().BeTrue();
        }

        [Then(@"the delete button is enabled")]
        public void ThenTheDeleteButtonIsEnabled()
        {
            Test.Pages.OrderForm.DeleteSolutionButtonIsDisabled().Should().BeFalse();
        }

        [Then(@"the save button is enabled")]
        public void ThenTheSaveButtonIsEnabled()
        {
            Test.Pages.OrderForm.SaveButtonDisplayed().Should().BeTrue();            
        }


        [Given(@"the User is presented with the Catalogue Solution edit form for a variable flat price")]
        public void GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormVariableFlatPrice()
        {
            GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrder();
            GivenAServiceRecipientIsSelected();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the User is presented with the Catalogue Solution edit form for a declarative flat price")]
        public void GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormDeclarativeFlatPrice()
        {
            GivenTheSupplierAddedToTheOrderHasASolutionWithADeclarativeFlatPrice();
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton(0);
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
            GivenAServiceRecipientIsSelected();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the User is presented with the Catalogue Solution edit form for a per patient flat price")]
        public void GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormPerPatientFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton(0);
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
            GivenAServiceRecipientIsSelected();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the proposed date is an invalid date")]
        public void GivenTheProposedDateIsAnInvalidDate()
        {
            Test.Pages.OrderForm.EnterProposedDate(DateTime.Now.AddYears(1).Year.ToString(), "02", "30");
        }

        [Given(@"the User enters a Delivery Date that is equal to 183 weeks after the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsEqualToWeeksAfterTheCommencementDate()
        {
            var order = (Order)Context["CreatedOrder"];
            var deliveryDate = order.CommencementDate.Value.AddDays(7 * 183);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the User enters a Delivery Date that is less than 183 weeks after the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsLessThanWeeksAfterTheCommencementDate()
        {
            var order = (Order)Context["CreatedOrder"];
            var deliveryDate = order.CommencementDate.Value.AddDays(7 * 182);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the User enters a Delivery Date that is more than 183 weeks after the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsMoreThanWeeksAfterTheCommencementDate()
        {
            var order = (Order)Context["CreatedOrder"];
            var deliveryDate = order.CommencementDate.Value.AddDays((7 * 183) + 1);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the User enters a Delivery Date that is before the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsBeforeTheCommencementDate()
        {
            var order = (Order)Context["CreatedOrder"];
            var deliveryDate = order.CommencementDate.Value.AddDays(-1);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the price has 4 decimal places")]
        public void GivenThePriceHasDecimalPlaces()
        {
            Test.Pages.OrderForm.EnterPriceInputValue("1.1234");
        }

        [Given(@"the price is negative")]
        public void GivenThePriceIsNegative()
        {
            Test.Pages.OrderForm.EnterPriceInputValue("-12.398");
        }

        [Given(@"the price contains characters")]
        public void GivenThePriceContainsCharacters()
        {
            Test.Pages.OrderForm.EnterPriceInputValue("1point35");
        }

        [Given(@"the price is over the max value")]
        public void GivenThePriceIsOverTheMaxValue()
        {
            Test.Pages.OrderForm.EnterPriceInputValue("79228162514264337593543950335.50");
        }

        [Then(@"the price is displayed to two decimal places")]
        public void ThenThePriceIsDisplayedToTwoDecimalPlaces()
        {
            var actualPrice = Test.Pages.OrderForm.GetPriceInputValue();
            Regex.Match(actualPrice, @"^[0-9]*\.[0-9]{2,3}$").Success.Should().BeTrue();
        }


        [Given(@"the quantity contains characters")]
        public void GivenTheQuantityContainsCharacters()
        {
            Test.Pages.OrderForm.EnterQuantity("seven");
        }

        [Given(@"the quanitity is a decimal")]
        public void GivenTheQuanitityIsADecimal()
        {
            Test.Pages.OrderForm.EnterQuantity("3.142");
        }

        [Given(@"the quantity is negative")]
        public void GivenTheQuantityIsNegative()
        {
            Test.Pages.OrderForm.EnterQuantity("-100");
        }

        [Given(@"the quantity is over the max length")]
        public void GivenTheQuantityIsOverTheMaxLength()
        {
            Test.Pages.OrderForm.EnterQuantity("2147483648");
        }

        [Given(@"fills in the Catalogue Solution edit form with valid data")]
        public void GivenFillsInTheCatalogueSolutionEditFormWithValidData()
        {
            var order = (Order)Context["CreatedOrder"];
            var deliveryDate = order.CommencementDate.Value;
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);

            if (Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed() == 2)
            {
                Test.Pages.OrderForm.ClickRadioButton(1);
            }

            var f = new Faker();
            Test.Pages.OrderForm.EnterQuantity(f.Random.Number(min:1).ToString());
            Test.Pages.OrderForm.EnterPriceInputValue(f.Finance.Amount().ToString());
        }

        [StepDefinition(@"the Associated Service is saved in the DB")]
        [StepDefinition(@"the Catalogue Solution is saved in the DB")]
        public void GivenTheCatalogueSolutionIsSavedInTheDB()
        {
            var order = (Order)Context["CreatedOrder"];
            var orderItem = new OrderItem().RetrieveByOrderId(Test.ConnectionString, order.OrderId).First();
            Context.Add("CreatedOrderItem", orderItem);
            orderItem.Should().NotBeNull();
        }

        [Given(@"there is one or more Catalogue Solutions added to the order")]
        public void GivenThereIsOneOrMoreCatalogueSolutionsAddedToTheOrder()
        {
            GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormVariableFlatPrice();
            GivenFillsInTheCatalogueSolutionEditFormWithValidData();
            new OrderForm(Test, Context).WhenTheUserChoosesToSave();
            GivenTheCatalogueSolutionIsSavedInTheDB();
        }

        [Then(@"the Catalogue Solutions are presented")]
        public void ThenTheCatalogueSolutionsArePresented()
        {
            Test.Pages.OrderForm.AddedSolutionsTableIsPopulated().Should().BeTrue();
        }

        [Then(@"the name of the Catalogue Solution is displayed")]
        public void ThenTheNameOfTheCatalogueSolutionIsDisplayed()
        {
            Test.Pages.OrderForm.AddedSolutionNameIsDisplayed().Should().BeTrue();
        }

        [Then(@"the Service Recipient Name and Service Recipient ODS code are concatenated into a Presentation Name using the format ""(.*)""")]
        public void ThenTheServiceRecipientNameAndServiceRecipientODSCodeAreConcatenatedIntoAPresentationNameUsingTheFormat(string p0)
        {
            var serviceRecipient = (ServiceRecipient)Context["CreatedServiceRecipient"];
            var expectedFormattedValue = string.Format("{0} ({1})", serviceRecipient.Name, serviceRecipient.OdsCode);
            Test.Pages.OrderForm.GetAddedSolutionServiceRecipient().Should().Be(expectedFormattedValue);
        }

        [Then(@"there is a control to edit each Catalogue Solution")]
        public void ThenThereIsAControlToEditEachCatalogueSolution()
        {
            Test.Pages.OrderForm.AddedSolutionNamesAreLinks().Should().BeTrue();
        }

        [Given(@"the User amends the existing catalogue solution details")]
        public void GivenTheUserAmendsTheExistingCatalogueSolutionDetails()
        {
            WhenTheUserHasChosenToManageTheCatalogueSolutionSection();
            ThenTheCatalogueSolutionsArePresented();
            Test.Pages.OrderForm.ClickAddedCatalogueItem();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();

            var order = (Order)Context["CreatedOrder"];
            var deliveryDate = order.CommencementDate.Value.AddMonths(6).AddYears(1);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
            
            var estimatedPeriod = Test.Pages.OrderForm.ClickRadioButton();            

            var f = new Faker();
            var quantity = f.Random.Number(min: 1).ToString();
            var price = f.Random.Number(min: 1).ToString();
            Test.Pages.OrderForm.EnterQuantity(quantity);
            Test.Pages.OrderForm.EnterPriceInputValue(price);

            Context.Add("AmendedDeliveryDate", deliveryDate);
            Context.Add("AmendedEstimatedPeriod", estimatedPeriod);
            Context.Add("AmendedQuantity", quantity);
            Context.Add("AmendedPrice", price);
            new OrderForm(Test, Context).WhenTheUserChoosesToSave();
            ThenTheCatalogueSolutionsArePresented();
        }

        [When(@"the User re-visits the Associated Service")]
        [When(@"the User re-visits the Catalogue Solution")]
        public void WhenTheUserRe_VisitsTheCatalogueSolution()
        {
            Test.Pages.OrderForm.ClickAddedCatalogueItem();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Then(@"the values will be populated with the values that was saved by the User")]
        public void ThenTheValuesWillBePopulatedWithTheValuesThatWasSavedByTheUser()
        {
            if(Context.ContainsKey("AmendedDeliveryDate"))
            {
                var expectedDate = (DateTime)Context["AmendedDeliveryDate"];
                var dateValueFromPage = Test.Pages.OrderForm.GetProposedDate();
                dateValueFromPage.Should().Be(expectedDate.ToString("dd MM yyyy"));
            }

            if(Context.ContainsKey("AmendedEstimatedPeriod"))
            {
                var expectedPeriod = (string)Context["AmendedEstimatedPeriod"];
                var periodFromPage = Test.Pages.OrderForm.GetSelectedRadioButton();
                periodFromPage.Should().Be(expectedPeriod);
            }
                        
            var quantityFromPage = Test.Pages.OrderForm.GetQuantity();
            var priceFromPage = Test.Pages.OrderForm.GetPriceInputValue();            
            
            var expectedQuantity = (string)Context["AmendedQuantity"];
            var expectedPrice = (string)Context["AmendedPrice"];           
            
            quantityFromPage.Should().Be(expectedQuantity);
            priceFromPage.Should().Be(expectedPrice);
        }

        [Given(@"that the Supplier in the order has no associated services")]
        [Given(@"the supplier added to the order has a solution with a declarative flat price")]
        public void GivenTheSupplierAddedToTheOrderHasASolutionWithADeclarativeFlatPrice()
        {
            var order = (Order)Context["CreatedOrder"];
            order.SupplierId = 100003;
            order.SupplierName = "Avatar Solutions";
            order.Update(Test.ConnectionString);
        }

        [Given(@"the User is presented with the Service Recipients saved in the Order after selecting the declarative flat price")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrderAfterSelectingTheDeclarativeFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton(0);
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
        }
        
    }
}
