using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class SupplierInformation : TestBase
    {
        public SupplierInformation(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"the user is able to manage the Supplier section")]
        public void ThenTheUserIsAbleToManageTheSupplierSection()
        {
            Test.Pages.OrderForm.ClickEditSupplier();
            ThenTheSearchSupplierScreenIsPresented();
        }

        [StepDefinition(@"the User chooses to edit the Supplier section for the first time")]
        public void WhenTheUserChoosesToEditTheSupplierSectionForTheFirstTime()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheSupplierSection();
        }

        [StepDefinition(@"the Search supplier screen is presented")]
        [StepDefinition(@"the Edit Supplier Form Page is presented")]
        public void ThenTheSearchSupplierScreenIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("supplier information").Should().BeTrue();
        }

        [When(@"the User has entered a valid Supplier search criterion")]
        public void WhenTheUserHasEnteredAValidSupplierSearchCriterion()
        {
            Test.Pages.OrderForm.EnterTextIntoTextField("a");
        }

        [StepDefinition(@"the User has entered a non matching Supplier search criterion")]
        public void WhenTheUserHasEnteredANonMatchingSupplierSearchCriterion()
        {
            Test.Pages.OrderForm.EnterTextIntoTextField("1 a 3");
        }

        [StepDefinition(@"the User chooses to search")]
        public void WhenTheyChooseToSearch()
        {
            Test.Pages.OrderForm.ClickSearchButton();
        }

        [Then(@"the matching Suppliers are presented")]
        public void ThenTheMatchingSuppliersArePresented()
        {
            Test.Pages.OrderForm.ListOfSuppliers().Count.Should().BeGreaterThan(0);
        }

        [StepDefinition(@"no matching Suppliers are presented")]
        public void ThenNoMatchingSuppliersArePresented()
        {
            Test.Pages.OrderForm.ListOfSuppliers().Count.Should().Be(0);
        }

        [Then(@"the User is informed that no matching Suppliers exist")]
        public void ThenTheUserIsInformedThatNoMatchingSuppliersExist()
        {
            Test.Pages.OrderForm.ErrorTitle().Should().BeEquivalentTo("No Supplier found");
        }

        [Then(@"they are informed that a Supplier name needs to be entered")]
        public void ThenTheyAreInformedThatASupplierNameNeedsToBeEntered()
        {
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("supplierName");
        }

        [Given(@"the User has been presented with matching Suppliers")]
        public void GivenTheUserHasBeenPresentedWithMatchingSuppliers()
        {
            new CommonSteps(Test, Context).GivenAnUnsubmittedOrderExists();
            new OrderForm(Test, Context).GivenTheSupplierSectionIsNotComplete();
            WhenTheUserChoosesToEditTheSupplierSectionForTheFirstTime();
            WhenTheUserHasEnteredAValidSupplierSearchCriterion();
            WhenTheyChooseToSearch();
            ThenTheMatchingSuppliersArePresented();
        }

        [When(@"they select a Supplier")]
        public void WhenTheySelectASupplier()
        {
            Test.Pages.OrderForm.SelectSupplier();
        }

        [When(@"they choose to continue")]
        public void WhenTheyChooseToContinue()
        {
            Test.Pages.OrderForm.ClickContinueButton();
        }

        [Then(@"they are informed that a Supplier needs to be selected")]
        public void ThenTheyAreInformedThatASupplierNeedsToBeSelected()
        {
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectSupplier");
        }

        [Then(@"the Supplier name is autopopulated")]
        public void ThenTheSupplierNameIsAutopopulated()
        {
            Test.Pages.OrderForm.SupplierNameIsDisplayed().Should().BeTrue();
        }

        [Then(@"the Supplier Registered Address is autopopulated")]
        public void ThenTheSupplierRegisteredAddressIsAutopopulated()
        {
            Test.Pages.OrderForm.AddressDisplayedAndNotEditable().Should().BeTrue();
        }

        [Then(@"the Supplier Contact details are autopopulated")]
        public void ThenTheSupplierContactDetailsAreAutopopulated()
        {
            var contactDetails = Test.Pages.OrderForm.GetContact();
            contactDetails.FirstName.Should().NotBeNullOrEmpty();
            contactDetails.LastName.Should().NotBeNullOrEmpty();
            contactDetails.Email.Should().NotBeNullOrEmpty();
            contactDetails.Phone.Should().NotBeNullOrEmpty();
        }


    }
}
