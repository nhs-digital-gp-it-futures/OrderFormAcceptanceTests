namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class FundingSource : TestBase
    {
        public FundingSource(UITest test, ScenarioContext context)
            : base(test, context)
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

        [Then(@"they are informed they have to select a Funding Source option")]
        public void ThenTheyAreInformedTheyHaveToSelectAFundingSourceOption()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectFundingSource");
        }

        [Given(@"the minimum data needed to enable the Funding Source section exists")]
        public async Task GivenTheMinimumDataNeededToEnableTheFundingSourceSectionExists()
        {
            var commonSteps = new CommonSteps(Test, Context);
            await commonSteps.GivenAnIncompleteOrderExists();

            var order = (Order)Context[ContextKeys.CreatedOrder];

            order.OrderingPartyContact = ContactHelper.Generate();
            order.CommencementDate = DateTime.Today;

            var supplier = await DbContext.Supplier.SingleOrDefaultAsync(s => s.Id == "100000")
                ?? (await SupplierInfo.GetSupplierWithId("100000", Test.BapiConnectionString)).ToDomain();

            var orderBuilder = new OrderBuilder(order)
                .WithExistingSupplier(supplier)
                .WithSupplierContact(ContactHelper.Generate());

            order = orderBuilder.Build();

            DbContext.Update(order);

            await new AssociatedServices(Test, Context).GivenAnAssociatedServiceWithAFlatPriceDeclarativeOrderTypeIsSavedToTheOrder();

            await commonSteps.SetOrderCatalogueSectionToComplete();
            await commonSteps.SetOrderAdditionalServicesSectionToComplete();
            await commonSteps.SetOrderAssociatedServicesSectionToComplete();
        }

        [Given(@"the User is presented with the edit Funding Source page")]
        public void GivenTheUserIsPresentedWithTheEditFundingSourcePage()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheFundingSourceSection();
        }

        [When(@"the User chooses a Funding Source option")]
        public void WhenTheUserChoosesAFundingSourceOption()
        {
            var option = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add("ChosenOption", option);
        }

        [Then(@"the Funding Source section is complete")]
        public void ThenTheFundingSourceSectionIsComplete()
        {
            Test.Pages.OrderForm.SectionComplete("funding-source").Should().BeTrue();
        }
    }
}
