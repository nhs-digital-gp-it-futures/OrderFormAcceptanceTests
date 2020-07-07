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

		[Then(@"the User is able to manage the Additional Services section")]
		public void ThenTheUserIsAbleToManageTheAdditionalServicesSection()
		{
			Test.Pages.OrderForm.ClickEditAdditionalServices();
		}
	}
}
