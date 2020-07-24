using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class FundingSource : TestBase
    {
        public FundingSource(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"the User is able to manage the Funding Source section")]
        public void ThenTheUserIsAbleToManageTheFundingSourceSection()
        {
            Test.Pages.OrderForm.ClickEditFundingSource();
            ThenTheFundingSourceScreenIsPresented();
        }

        [StepDefinition(@"the Edit Funding Source Page is presented")]
        public void ThenTheFundingSourceScreenIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Funding source for").Should().BeTrue();
        }
    }
}
