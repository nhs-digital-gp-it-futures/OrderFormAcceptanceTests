using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class CatalogueSolutions : TestBase
    {

        public CatalogueSolutions(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the User is able to manage the Catalogue Solutions section")]
        public void ThenTheUserIsAbleToManageTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.ClickEditCatalogueSolutions();
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeFalse();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Catalogue Solution").Should().BeTrue();
        }

    }
}
