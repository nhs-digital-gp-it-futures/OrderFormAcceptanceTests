namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Bogus;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using OrderFormAcceptanceTests.TestData.Utils;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class CatalogueSolutions : TestBase
    {
        public CatalogueSolutions(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Given(@"there is no Catalogue Solution in the order but the section is complete")]
        public async Task SetCatalogueSolutionSectionToCompleteWith0SolutionsAdded()
        {
            await new OrderForm(Test, Context).GivenTheCatalogueSolutionSectionIsComplete();
            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemsInDb = (await DbContext.Order.FindAsync(order.Id)).OrderItems.Where(s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.Solution);
            orderItemsInDb.Should().HaveCount(0);
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

        [Then(@"the select Catalogue Solution form is presented")]
        [Then(@"they are presented with the Catalogue Solutions available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheCatalogueSolutionsAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Catalogue Solution").Should().BeTrue();
        }

        [Then(@"the Additional Services are displayed in alphabetical order")]
        [Then(@"they are displayed in alphabetical order")]
        public void ThenTheyAreDisplayedInAlphabeticalOrder()
        {
            CommonSteps.AssertListOfStringsIsInAscendingOrder(Test.Pages.OrderForm.GetRadioButtonText());
        }

        [Given(@"the User is presented with Catalogue Solutions available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithCatalogueSolutionsAvailableFromTheirChosenSupplier()
        {
            WhenTheUserHasChosenToManageTheCatalogueSolutionSection();
            new CommonSteps(Test, Context).WhenTheUserChoosesToAddAOrderItem();
            ThenTheyArePresentedWithTheCatalogueSolutionsAvailableFromTheirChosenSupplier();
        }

        [Then(@"the User is informed they have to select a Catalogue Solution")]
        public void ThenTheUserIsInformedTheyHaveToSelectACatalogueSolution()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectSolution");
        }

        [When(@"a quantity is entered")]
        public void WhenAQuantityIsEntered()
        {
            Test.Pages.OrderForm.EnterQuantity(new Random().Next(1000).ToString());
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
            var solutionName = Test.Pages.OrderForm.GetRadioButtonText()[0];
            Test.Pages.OrderForm.ClickRadioButtonWithText(solutionName);
            Context.Add(ContextKeys.ChosenSolutionName, solutionName);
        }

        [Given(@"the User is presented with select Service Recipient form")]
        public void GivenTheUserIsPresentedWithSelectServiceRecipientForm()
        {
            GivenTheUserIsPresentedWithCatalogueSolutionsAvailableFromTheirChosenSupplier();
            GivenTheUserSelectsACatalogueSolutionToAdd();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Then(@"all the available prices for that Catalogue Solution are presented")]
        public void ThenAllTheAvailablePricesForThatCatalogueSolutionArePresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("List price").Should().BeTrue();
            var expectedNumberOfPrices = Context.TryGetValue(ContextKeys.NumberOfPrices, out int expectedPrices);
            if (expectedNumberOfPrices)
            {
                Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().Be(expectedPrices);
            }
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
            var odsCode = Test.Pages.OrderForm.ClickCheckboxReturnName();
            Context.Add(ContextKeys.ChosenOdsCode, odsCode);
        }

        [Given(@"the User is presented with the Service Recipients for the Order after selecting the variable flat price")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrder()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            var numOfPrices = Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed();
            Context.Add(ContextKeys.NumberOfPrices, numOfPrices);
            GivenTheUserSelectsAPrice();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Given(@"the User is presented with the Service Recipients for the Order after selecting the per patient flat price")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrderAfterSelectingThePerPatientFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Then(@"the User is informed they have to select a Service Recipient")]
        public void ThenTheUserIsInformedTheyHaveToSelectAServiceRecipient()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("Recipient");
        }

        [Then(@"they are presented with the Associated Service edit form")]
        [Then(@"they are presented with the Catalogue Solution edit form")]
        public void ThenTheyArePresentedWithTheOrderItemPriceEditForm()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("information for").Should().BeTrue();
        }

        [When(@"an Estimation period is selected")]
        public void WhenAnEstimationPeriodIsSelected()
        {
            Test.Pages.OrderForm.ClickRadioButton();
        }

        [Then(@"the name of the selected Catalogue Solution is displayed on the Catalogue Solution edit form")]
        public void ThenTheNameOfTheSelectedCatalogueSolutionIsDisplayedOnTheCatalogueSolutionEditForm()
        {
            var addedSolutionName = Context.Get<string>(ContextKeys.ChosenSolutionName);
            Test.Pages.OrderForm.TextDisplayedInPageTitle(addedSolutionName).Should().BeTrue();
        }

        [Then(@"the selected Service Recipient with their ODS code is displayed on the Catalogue Solution edit form")]
        public async Task ThenTheSelectedServiceRecipientWithTheirODSCodeIsDisplayedOnTheCatalogueSolutionEditForm()
        {
            var chosenOdsCode = (string)Context[ContextKeys.ChosenOdsCode];
            var query = "Select Name FROM [dbo].[Organisations] where OdsCode=@ChosenOdsCode";
            var expectedOrganisationName = (await SqlExecutor.ExecuteAsync<string>(Test.IsapiConnectionString, query, new { chosenOdsCode })).Single();
            var expectedFormattedValue = string.Format("{0} ({1})", expectedOrganisationName, chosenOdsCode);
            Test.Pages.OrderForm.TextDisplayedInPageTitle(expectedFormattedValue).Should().BeTrue();
        }

        [Then(@"the Associated Service edit form contains an input for the price")]
        [Then(@"the Catalogue Solution edit form contains an input for the price")]
        [Then(@"the Additional Service edit form contains an input for the price")]
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
        [Then(@"the Additional Service edit form contains a unit of order")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsAUnitOfOrder()
        {
            Test.Pages.OrderForm.OrderUnitIsDisplayed().Should().BeTrue();
        }

        [Then(@"the item on the Associated Service edit form contains an input for the quantity")]
        [Then(@"the item on the Catalogue Solution edit form contains an input for the quantity")]
        [Then(@"the Additional Service edit form contains an input for the quantity")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsAnInputForTheQuantity()
        {
            Test.Pages.OrderForm.QuantityInputIsDisplayed().Should().BeTrue();
        }

        [Then(@"the item on the Catalogue Solution edit form contains an input for the practiceListSize")]
        public void ThenTheItemOnTheCatalogueSolutionEditFormContainsAnInputForThePracticeListSize()
        {
            Test.Pages.OrderForm.PracticeListSizeInputIsDisplayed().Should().BeTrue();
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

        [Then(@"the save button is enabled")]
        public void ThenTheSaveButtonIsEnabled()
        {
            Test.Pages.OrderForm.SaveButtonDisplayed().Should().BeTrue();
        }

        [Given(@"the User is presented with the Catalogue Solution edit form for a variable flat price")]
        public void GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormVariableFlatPrice()
        {
            CommonSteps common = new(Test, Context);

            GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrder();
            GivenAServiceRecipientIsSelected();
            common.WhenTheyChooseToContinue();
            common.WhenTheyChooseToContinue();
            WhenAQuantityIsEntered();
            WhenAnEstimationPeriodIsSelected();
            common.WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the User is presented with the Catalogue Solution edit form for a declarative flat price")]
        public async Task GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormDeclarativeFlatPrice()
        {
            CommonSteps common = new(Test, Context);

            await GivenTheSupplierAddedToTheOrderHasASolutionWithADeclarativeFlatPrice();
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton(0);
            common.ContinueAndWaitForCheckboxes();
            GivenAServiceRecipientIsSelected();
            common.WhenTheyChooseToContinue();
            common.WhenTheyChooseToContinue();
            WhenAQuantityIsEntered();
            common.WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the User is presented with the Catalogue Solution edit form for a per patient flat price")]
        public void GivenTheUserIsPresentedWithTheCatalogueSolutionEditFormPerPatientFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton(0);
            new CommonSteps(Test, Context).ContinueAndWaitForCheckboxes();
            GivenAServiceRecipientIsSelected();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            if (Test.Pages.OrderForm.IsPlannedDeliveryDateDisplayed())
            {
                new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            }

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
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var deliveryDate = order.CommencementDate.Value.AddDays(7 * 183);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the User enters a Delivery Date that is less than 183 weeks after the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsLessThanWeeksAfterTheCommencementDate()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var deliveryDate = order.CommencementDate.Value.AddDays(7 * 182);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the User enters a Delivery Date that is more than 183 weeks after the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsMoreThanWeeksAfterTheCommencementDate()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var deliveryDate = order.CommencementDate.Value.AddDays((7 * 183) + 1);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the User enters a Delivery Date that is before the Commencement Date")]
        public void GivenTheUserEntersADeliveryDateThatIsBeforeTheCommencementDate()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var deliveryDate = order.CommencementDate.Value.AddDays(-1);
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);
        }

        [Given(@"the price has (.*) decimal places")]
        public void GivenThePriceHasDecimalPlaces(int decimalPlaces)
        {
            decimal number = 1.1234567890M;
            Test.Pages.OrderForm.EnterPriceInputValue(decimal.Round(number, decimalPlaces).ToString());
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
            var actualPrice = Test.Pages.OrderForm.GetPriceInputValue().Replace(",", string.Empty);
            var match2Decimals = Regex.Match(actualPrice, @"^[0-9]*\.[0-9]{2,3}$").Success;
            var match0Decimals = Regex.Match(actualPrice, @"^[0-9]*$").Success;
            if (!match2Decimals && !match0Decimals)
            {
                false.Should().BeTrue("Expecting either 2 decimal places or 0 decimal places");
            }
        }

        [Given(@"the (.*) quantity contains characters")]
        public void GivenTheQuantityContainsCharacters(string quantityLabel = "quantity")
        {
            Test.Pages.OrderForm.EnterQuantity("seven", quantityLabel);
        }

        [Given("the quantity contains characters")]
        public void GivenTheQuantityContainsCharacters()
        {
            GivenTheQuantityContainsCharacters("quantity");
        }

        [Given(@"the (.*) quantity is a decimal")]
        public void GivenTheQuanitityIsADecimal(string quantityLabel = "quantity")
        {
            Test.Pages.OrderForm.EnterQuantity("3.142", quantityLabel);
        }

        [Given(@"the quantity is a decimal")]
        public void GivenTheQuanitityIsADecimal()
        {
            GivenTheQuanitityIsADecimal("quantity");
        }

        [Given(@"the (.*) quantity is negative")]
        public void GivenTheQuantityIsNegative(string quantityLabel = "quantity")
        {
            Test.Pages.OrderForm.EnterQuantity("-100", quantityLabel);
        }

        [Given(@"the quantity is negative")]
        public void GivenTheQuantityIsNegative()
        {
            GivenTheQuantityIsNegative("quantity");
        }

        [Given(@"the (.*) quantity is over the max length")]
        public void GivenTheQuantityIsOverTheMaxLength(string quantityLabel = "quantity")
        {
            Test.Pages.OrderForm.EnterQuantity("2147483648", quantityLabel);
        }

        [Given(@"the quantity is over the max length")]
        public void GivenTheQuantityIsOverTheMaxLength()
        {
            GivenTheQuantityIsOverTheMaxLength("quantity");
        }

        [Given(@"fills in the Catalogue Solution edit form with valid data for (.*)")]
        public void GivenFillsInTheCatalogueSolutionEditFormWithValidData(string quantityLabel)
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var deliveryDate = order.CommencementDate.Value;
            Test.Pages.OrderForm.EnterProposedDate(deliveryDate);

            if (Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed() == 2)
            {
                Test.Pages.OrderForm.ClickRadioButton(1);
            }

            decimal maxValue = decimal.Parse(Test.Pages.OrderForm.GetPriceInputValue());

            var f = new Faker();
            Test.Pages.OrderForm.EnterQuantity(f.Random.Number(min: 1, max: 1000).ToString(), quantityLabel: quantityLabel);
            Test.Pages.OrderForm.EnterPriceInputValue(f.Finance.Amount(min: 0, max: maxValue).ToString());
        }

        [StepDefinition(@"the Catalogue Solution is saved in the DB")]
        public async Task GivenTheCatalogueSolutionIsSavedInTheDB()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            var orderItemsInDb = (await DbContext.Order.FindAsync(order.Id)).OrderItems.Where(s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.Solution);
            orderItemsInDb.Should().NotBeNull();
        }

        [Given(@"the supplier chosen has more than one solution")]
        public async Task GivenTheSupplierChosenHasMoreThanOneSolution()
        {
            var commonSteps = new CommonSteps(Test, Context);
            await commonSteps.GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder();
            await commonSteps.GivenACatalogueSolutionWithAFlatPriceVariableOn_DemandOrderTypeWithTheQuantityPeriodPerMonthIsSavedToTheOrder();
        }

        [Then(@"the Catalogue Solutions are presented")]
        [Then(@"the Associated Services are presented")]
        [Then(@"the Additional Services are presented")]
        public void ThenTheOrderItemsArePresented()
        {
            Test.Pages.OrderForm.AddedOrderItemsTableIsPopulated().Should().BeTrue();
        }

        [Given(@"a User has added a per patient solution to the order")]
        public async Task GivenAUserHasAddedAPerPatientSolutionToTheOrder()
        {
            await new CommonSteps(Test, Context).GivenACatalogueSolutionWithAFlatPriceVariablePer_PatientOrderTypeIsSavedToTheOrder();
        }

        [Given(@"a User has added multiple solutions to the order")]
        public async Task GivenAUserHasAddedMultipleSolutionsToTheOrder()
        {
            var commonSteps = new CommonSteps(Test, Context);
            await commonSteps.GivenACatalogueSolutionWithAFlatPriceVariablePer_PatientOrderTypeIsSavedToTheOrder();
            await commonSteps.GivenACatalogueSolutionWithAFlatPriceVariableDeclarativeOrderTypeIsSavedToTheOrder();
        }

        [Then(@"the name of each Associated Service is displayed")]
        [Then(@"the name of each Additional Service is displayed")]
        [Then(@"the name of the Catalogue Solution is displayed")]
        public void ThenTheNameOfTheOrderItemIsDisplayed()
        {
            Test.Pages.OrderForm.AddedOrderItemNameIsDisplayed().Should().BeTrue();
        }

        [Then(@"they are able to manage each Associated Service")]
        [Then(@"they are able to manage each Additional Service")]
        [Then(@"there is a control to edit each Catalogue Solution")]
        public void ThenThereIsAControlToEditEachCatalogueItem()
        {
            Test.Pages.OrderForm.AddedOrderItemNamesAreLinks().Should().BeTrue();
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
            if (Context.ContainsKey(ContextKeys.AmendedDeliveryDate))
            {
                var expectedDate = (DateTime)Context[ContextKeys.AmendedDeliveryDate];
                var dateValueFromPage = Test.Pages.OrderForm.GetProposedDate();
                dateValueFromPage.Should().Be(expectedDate.ToString("dd MM yyyy"));
            }

            if (Context.ContainsKey(ContextKeys.AmendedEstimatedPeriod))
            {
                var expectedPeriod = (string)Context[ContextKeys.AmendedEstimatedPeriod];
                var periodFromPage = Test.Pages.OrderForm.GetSelectedRadioButton();
                periodFromPage.Should().Be(expectedPeriod);
            }

            var quantityFromPage = Test.Pages.OrderForm.GetQuantity();
            var priceFromPage = Test.Pages.OrderForm.GetPriceInputValue();

            var expectedQuantity = (string)Context[ContextKeys.AmendedQuantity];
            var expectedPrice = (string)Context[ContextKeys.AmendedPrice];

            quantityFromPage.Should().Be(expectedQuantity);
            priceFromPage.Should().Be(expectedPrice);
        }

        [Given(@"that the Supplier in the order has no associated services")]
        public async Task GivenTheSupplierInTheOrderHasNoAssociatedServices()
        {
            var supplier = (await SupplierInfo.SuppliersWithout(Test.BapiConnectionString, CatalogueItemType.AssociatedService)).First().ToDomain() ?? throw new NullReferenceException("Supplier not found");

            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.Supplier = supplier;

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();
        }

        [Given(@"the supplier added to the order has a solution with a declarative flat price")]
        public async Task GivenTheSupplierAddedToTheOrderHasASolutionWithADeclarativeFlatPrice()
        {
            var supplierInfo = (await SupplierInfo.SuppliersWithCatalogueSolution(Test.BapiConnectionString, ProvisioningType.Declarative)).First().ToDomain();

            var supplier = await DbContext.Supplier.FindAsync(supplierInfo.Id) ?? supplierInfo;

            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.Supplier = supplier;
            order.SupplierContact = ContactHelper.Generate();

            DbContext.Update(order);
            await DbContext.SaveChangesAsync();
        }

        [Given(@"the User is presented with the Service Recipients for the Order after selecting the declarative flat price")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrderAfterSelectingTheDeclarativeFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            Test.Pages.OrderForm.ClickRadioButton(0);
            new CommonSteps(Test, Context).ContinueAndWaitForCheckboxes();
        }

        [Then(@"they are presented with the (.*) for the order")]
        public void ThenTheyArePresentedWithTheCommencementDateForTheOrder(string pageName)
        {
            Test.Pages.OrderForm.TextDisplayedInPageTitle(pageName).Should().BeTrue();
        }

        [Given(@"the User chooses to edit a saved Catalogue Solution")]
        public void GivenTheUserChoosesToEditASavedCatalogueSolution()
        {
            Test.Pages.OrderForm.ClickAddedCatalogueItem();
        }

        [Then(@"the Catalogue Solutions are in alphabetical order")]
        public void ThenTheCatalogueSoltutionsAreInAlphabeticalOrder()
        {
            var solutions = Test.Pages.OrderForm.GetAddedCatalogueItems();
            solutions.Should().BeInAscendingOrder();
        }

        [When(@"the User chooses to edit the service recipients")]
        public void WhenTheUserChoosesToEditTheServiceRecipients()
        {
            Context.Add("AddedRecipientCount", Test.Pages.OrderForm.GetNumberOfAddedRecipients());
            Test.Pages.OrderForm.ClickEditServiceRecipientsButton();
        }

        [When(@"the User adds another service recipient to the order")]
        public void WhenTheUserAddsAnotherServiceRecipientToTheOrder()
        {
            Test.Pages.OrderForm.ClickUnselectedCheckbox();
        }

        [Then(@"the Edit Price form displays the expected number of recipients")]
        public void ThenTheEditPriceFormDisplaysTheExpectedNumberOfRecipients()
        {
            var newRecipients = Test.Pages.OrderForm.GetNumberOfAddedRecipients();
            newRecipients.Should().Be((int)Context["AddedRecipientCount"] + 1);
        }

        [Then(@"the Edit Price form displays the same number of recipients as earlier")]
        public void ThenTheEditPriceFormDisplaysTheSameNumberOfRecipientsAsEarlier()
        {
            var newRecipients = Test.Pages.OrderForm.GetNumberOfAddedRecipients();
            newRecipients.Should().Be((int)Context["AddedRecipientCount"]);
        }

        [Given(@"the delete button is enabled")]
        public void GivenTheDeleteButtonIsEnabled()
        {
            Test.Pages.OrderForm.DeleteSolutionButtonIsDisabled().Should().BeFalse();
        }

        [StepDefinition(@"the User chooses to delete the Catalogue Solution")]
        public void WhenTheUserChoosesToDeleteTheCatalogueSolution()
        {
            Test.Pages.OrderForm.ClickDeleteCatalogueSolutionButton();
        }

        [Then(@"the User is asked to confirm the choice to delete the catalogue solution")]
        public async Task ThenTheUserIsAskedToConfirmTheChoiceToDeleteTheCatalogueSolutionAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var orderitem = order.OrderItems[0].CatalogueItem.Name;

            Test.Pages.OrderForm.EditNamedSectionPageDisplayed($"Delete {orderitem}").Should().BeTrue();
        }

        [StepDefinition(@"the User chooses to confirm the delete")]
        public async Task WhenTheUserChoosesToConfirmTheDeleteAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            Context.Add(ContextKeys.DeletedOrderItem, order.OrderItems[0]);
            Test.Pages.DeleteOrder.ClickDeleteButtonYes();
        }

        [Then(@"the deleted Catalogue Solution with the unit is not on the Catalogue Solution dashboard")]
        public void ThenTheDeletedCatalogueSolutionWithTheUnitIsNotOnTheCatalogueSolutionDashboard()
        {
            var orderItem = Context.Get<Domain.OrderItem>(ContextKeys.DeletedOrderItem);

            Test.Pages.OrderForm.GetAddedCatalogueItems().Should().NotContain(orderItem.CatalogueItem.Name);
        }

        [When(@"the User chooses not to delete the Catalogue Solution")]
        public void WhenTheUserChoosesNotToDeleteTheCatalogueSolution()
        {
            Test.Pages.OrderForm.ClickCancelDelete();
        }

        [Then(@"only the Catalogue Solution with the unit is deleted from the order")]
        public async Task ThenOnlyTheCatalogueSolutionWithTheUnitIsDeletedFromTheOrderAsync()
        {
            var orderItem = Context.Get<OrderItem>(ContextKeys.DeletedOrderItem);
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);

            if (order.OrderItems.Count > 0)
            {
                order.OrderItems.Should().NotContain(orderItem);
            }
        }

        [StepDefinition(@"the User is informed the Catalogue Solution is deleted")]
        public async Task ThenTheUserIsInformedTheCatalogueSolutionIsDeletedAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            Test.Pages.OrderForm.DeleteSolutionConfirmationTitle().Should().Match($"* deleted from {order.CallOffId}");
        }

        [StepDefinition(@"the edit Catalogue Solution form is presented")]
        [Then(@"they are presented with the Edit Catalogue Solution form for the order type")]
        [Then(@"the Call-off Agreement ID is displayed")]
        public async Task ThenTheCall_OffAgreementIDIsDisplayedAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var callOffID = order.CallOffId.ToString();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed(callOffID).Should().BeTrue();
        }

        [Then(@"the order description is displayed")]
        public async Task ThenTheOrderDescriptionIsDisplayedAsync()
        {
            var order = await OrderHelpers.GetFullOrderAsync(Context.Get<Order>(ContextKeys.CreatedOrder).CallOffId, DbContext);
            var orderDescription = order.Description;
            Test.Pages.OrderForm.DeleteConfirmationOrderDescription().Should().BeEquivalentTo(orderDescription);
        }

        [Given(@"the User selects a Catalogue Solution previously saved in the Order")]
        public async Task GivenTheUserSelectsACatalogueSolutionPreviouslySavedInTheOrder()
        {
            Test.Pages.OrderForm.ClickAddSolutionButton();

            // Get order item from DB
            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemInDB = (await DbContext.Order.FindAsync(order.Id))
                .OrderItems.First(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.Solution);

            // Get index of radio button matching DB entry
            var solutionRadioButton = Test.Pages.OrderForm.GetRadioButtonText().IndexOf(orderItemInDB.CatalogueItem.Name);

            // Click radio button that matches text in DB entry
            Test.Pages.OrderForm.ClickRadioButton(solutionRadioButton);
        }

        [Then(@"the previously saved data is displayed")]
        public async Task ThenThePreviouslySavedDataIsDisplayed()
        {
            var enteredPrice = decimal.Parse(Test.Pages.OrderForm.GetPriceInputValue());
            var numberOfRecipients = Test.Pages.OrderForm.GetNumberOfAddedRecipients();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemsInDb = (await DbContext.Order.FindAsync(order.Id))
                .OrderItems.Single(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.Solution);

            orderItemsInDb.OrderItemRecipients.Count.Should().Be(numberOfRecipients);
            orderItemsInDb.Price.Value.Should().Be(enteredPrice);
        }

        [When(@"they choose to go back")]
        public void WhenTheyChooseToGoBack()
        {
            Test.Pages.OrderForm.ClickBackLink();
        }

        [Then(@"the previously selected Catalogue Solution persist")]
        public async Task ThenThePreviouslySelectedCatalogueSolutionPersistAsync()
        {
            var selectedRadioId = Test.Pages.OrderForm.GetSelectedRadioButton();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            var orderItemInDb = (await DbContext.Order.FindAsync(order.Id)).OrderItems.Single(
                s => s.CatalogueItem.CatalogueItemType == CatalogueItemType.Solution);

            orderItemInDb.CatalogueItem.Id.ToString().Should().BeEquivalentTo(selectedRadioId);
        }
    }
}
