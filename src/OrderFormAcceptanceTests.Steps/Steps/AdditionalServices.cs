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

		[Then(@"the Additional Service dashboard is presented")]
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


	}
}
