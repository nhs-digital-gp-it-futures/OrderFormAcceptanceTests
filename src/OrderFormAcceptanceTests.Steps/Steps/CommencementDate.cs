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
        
        [Then(@"the user is able to manage the Commencement Date section")]
        [StepDefinition(@"the user chooses to manage the Commencement Date Section")]
        public void ThenTheUserIsAbleToManageTheCommencementDateSection()
        {
            Test.Pages.OrderForm.ClickEditCommencementDate();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("commencement date").Should().BeTrue();
        }

        [Then(@"the date remains (.*), (.*) and (.*)")]
        public void ThenTheDateRemainsAnd(string day, string month, string year)
        {
            Test.Pages.CommencementDate.GetDay().Should().Be(day);
            Test.Pages.CommencementDate.GetMonth().Should().Be(month);
            Test.Pages.CommencementDate.GetYear().Should().Be(year);
        }
    }
}
