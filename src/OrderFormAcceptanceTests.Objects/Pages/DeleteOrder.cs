using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public sealed class DeleteOrder
    {
        public By DeleteButtonYes => CustomBy.DataTestId("yes-button", "button");
        public By DeleteButtonNo => CustomBy.DataTestId("no-button", "a");
    }
}
