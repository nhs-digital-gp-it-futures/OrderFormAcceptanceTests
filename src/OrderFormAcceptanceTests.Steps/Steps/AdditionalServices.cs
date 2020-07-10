using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
	[Binding]
	public sealed class AdditionalServices : TestBase
	{
		public AdditionalServices(UITest test, ScenarioContext context) : base(test, context)
		{
		}

		[StepDefinition(@"the User is able to manage the Additional Services section")]
		[When(@"the User has chosen to manage the Additional Service section")]
		public void ThenTheUserIsAbleToManageTheAdditionalServicesSection()
		{
			Test.Pages.OrderForm.ClickEditAdditionalServices();
		}

		[StepDefinition(@"the Additional Service dashboard is presented")]
		public void ThenTheAdditionalServiceDashboardIsPresented()
		{
			Test.Pages.AdditionalServices.PageDisplayed();
		}

		[Then(@"there is a control to add a Additional Service")]
		public void ThenThereIsAControlToAddAAdditionalService()
		{
			Test.Pages.AdditionalServices.AddAdditionalServiceButtonDisplayed().Should().BeTrue();
		}

		[Then(@"there is content indicating there is no Additional Service added")]
		public void ThenThereIsContentIndicatingThereIsNoAdditionalServiceAdded()
		{
			Test.Pages.AdditionalServices.NoAddedOrderItemsDisplayed().Should().BeTrue();
		}

        [When(@"the User chooses to add a single Additional Service")]
        public void WhenTheUserChoosesToAddASingleAdditionalService()
        {
            Test.Pages.OrderForm.ClickAddSolutionButton();
        }

        [Then(@"they are presented with the Additional Service available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheAdditionalServiceAvailableFromTheirChosenSupplier()
        {
			Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Additional Service").Should().BeTrue();
        }

        [Then(@"they can select one Additional Service to add")]
        public void ThenTheyCanSelectOneAdditionalServiceToAdd()
        {
			Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
        }

        [Given(@"the User is presented with Additional Services available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithAdditionalServicesAvailableFromTheirChosenSupplier()
        {
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            WhenTheUserChoosesToAddASingleAdditionalService();
            ThenTheyArePresentedWithTheAdditionalServiceAvailableFromTheirChosenSupplier();
        }
    }
}
