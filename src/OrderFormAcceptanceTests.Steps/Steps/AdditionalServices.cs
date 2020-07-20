using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
	[Binding]
	public sealed class AdditionalServices : TestBase
	{
		public AdditionalServices(UITest test, ScenarioContext context) : base(test, context)
		{
		}

		[Given(@"there are no Additional Services in the order")]
		public void GivenThereAreNoAdditionalServicesInTheOrder()
		{
            Context.Should().NotContainKey("CreatedAdditionalServiceOrderItem");
            var order = (Order)Context["CreatedOrder"];
			var searchedOrderItem = new OrderItem().RetrieveByOrderId(Test.ConnectionString, order.OrderId, 2);
            searchedOrderItem.Should().BeEmpty();
		}

		[Given(@"an Additional Service is added to the order")]
		public void GivenAnAdditionalServiceIsAddedToTheOrder()
		{
			new OrderForm(Test, Context).GivenTheAdditionalServicesSectionIsComplete();
			var orderItem = new OrderItem().GenerateAdditionalServiceOrderItemWithFlatPricedPerPatient((Order)Context["CreatedOrder"]);
			orderItem.Create(Test.ConnectionString);
			Context.Add("CreatedAdditionalServiceOrderItem", orderItem);
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
        [Then(@"the Additional Services of the Catalogue Solutions in the order is displayed")]
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

        [Given(@"the User has selected an Additional Service to add")]
        public void GivenTheUserHasSelectedAnAdditionalServiceToAdd()
        {
            ThenTheUserIsAbleToManageTheAdditionalServicesSection();
            WhenTheUserChoosesToAddASingleAdditionalService();
            ThenTheyArePresentedWithTheAdditionalServiceAvailableFromTheirChosenSupplier();
            Test.Pages.OrderForm.ClickRadioButton();            
        }

        [Then(@"all the available prices for that Additional Service are presented")]
        public void ThenAllTheAvailablePricesForThatAdditionalServiceArePresented()
        {
            Test.Pages.AdditionalServices.PricePageTitle().Should().ContainEquivalentOf("List price");
            ThenTheyCanSelectOneAdditionalServiceToAdd();            
        }

        [Given(@"the available prices for the selected Additional Service are presented")]
        public void GivenTheAvailablePricesForTheSelectedAdditionalServiceArePresented()
        {
            GivenTheUserHasSelectedAnAdditionalServiceToAdd();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Then(@"the User's selected Additional Service is selected")]
        public void ThenTheUserSSelectedAdditionalServiceIsSelected()
        {
            Test.Pages.OrderForm.IsRadioButtonSelected().Should().BeTrue();
        }

        [Given(@"the User has selected a Additional Service price")]
        public void GivenTheUserHasSelectedAAdditionalServicePrice()
        {
            Test.Pages.AdditionalServices.PricePageTitle().Should().ContainEquivalentOf("List price");
            Test.Pages.OrderForm.ClickRadioButton();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
        }

        [Then(@"the Additional Service name is displayed")]
        public void ThenTheAdditionalServiceNameIsDisplayed()
        {
            Test.Pages.AdditionalServices.ServiceRecipientsTitle().Should().MatchRegex("Service Recipient for .*");
        }

        [Then(@"the Service Recipients are presented in ascending alphabetical order by Presentation Name")]
        public void ThenTheServiceRecipientsArePresentedInAscendingAlphabeticalOrderByPresentationName()
        {
            var displayedRecipients = Test.Pages.AdditionalServices.ServiceRecipientNames();

            displayedRecipients.Should().BeInAscendingOrder();
        }
    }
}
