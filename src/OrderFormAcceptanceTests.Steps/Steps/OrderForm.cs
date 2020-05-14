using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class OrderForm : TestBase
    {
        public OrderForm(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"there is the Order description section")]
        public void ThenThereIsTheOrderDescriptionSection()
        {
            Test.Pages.OrderForm.EditDescriptionSectionDisplayed().Should().BeTrue();
        }

        [Then(@"the user is able to manage the Order Description section")]
        public void ThenTheUserIsAbleToManageTheOrderDescriptionSection()
        {
            Test.Pages.OrderForm.ClickEditDescription();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Order description").Should().BeTrue();
        }

    }
}
