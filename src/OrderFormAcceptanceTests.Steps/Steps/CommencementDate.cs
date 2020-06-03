using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps
{
	[Binding]
    public class CommencementDate : TestBase
    {
		public CommencementDate(UITest test, ScenarioContext context) : base(test, context)
		{
		}

        [Given(@"a valid date is entered")]
        public void GivenAValidDateIsEntered()
        {
            var today = DateTime.Today;
            Test.Pages.CommencementDate.SetDate(today);
        }


        [Given(@"the Day is set to (.*)")]
        public void GivenTheDayIsSetTo(string day)
        {
            Test.Pages.CommencementDate.SetDayValue(day);
        }

        [Given(@"the Month is set to (.*)")]
        public void GivenTheMonthIsSetTo(string month)
        {
            Test.Pages.CommencementDate.SetMonthValue(month);
        }

        [Given(@"the Year is set to (.*)")]
        public void GivenTheYearIsSetTo(string year)
        {
            Test.Pages.CommencementDate.SetYearValue(year);
        }
        
        [Given(@"the Commencement Date entered is (.*) days earlier than today's date")]
        public void GivenTheCommencementDateEnteredIsDaysEarlierThanTodaySDate(int days)
        {
            // No option
            var date = DateTime.Today.AddDays((days * -1));
            Test.Pages.CommencementDate.SetDate(date);
        }
        
        [Then(@"there is a list of sections")]
        public void ThenThereIsAListOfSections()
        {
            Test.Pages.OrderForm.EditCallOffOrderingPartySectionDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.EditDescriptionSectionDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.EditSupplierSectionDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.EditCommencementDateSectionDisplayed().Should().BeTrue();
        }
        
        [Then(@"the user is able to manage the Commencement Date section")]
        [StepDefinition(@"the user chooses to manage the Commencement Date Section")]
        public void ThenTheUserIsAbleToManageTheCommencementDateSection()
        {
            Test.Pages.OrderForm.ClickEditCommencementDate();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("commencement date").Should().BeTrue();
        }        
        
        [Then(@"the enabled sections are Order description")]
        public void ThenTheEnabledSectionsAreOrderDescription()
        {
            Test.Pages.OrderForm.OrderDescriptionEnabled().Should().BeTrue();
        }
        
        [Then(@"Call Off Ordering Party")]
        public void ThenCallOffOrderingParty()
        {
            Test.Pages.OrderForm.CallOffOrderingPartyEnabled().Should().BeTrue();
        }
        
        [Then(@"Supplier")]
        public void ThenSupplier()
        {
            Test.Pages.OrderForm.SupplierInformationEnabled().Should().BeTrue();
        }
        
        [Then(@"Commencement date")]
        public void ThenCommencementDate()
        {
            Test.Pages.OrderForm.CommencementDateEnabled().Should().BeTrue();
        }
    }
}
