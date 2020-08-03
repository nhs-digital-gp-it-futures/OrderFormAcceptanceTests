using System;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public sealed class CompleteOrder
    {
        public By FundingSourceYesContent => CustomBy.DataTestId("");
        public By FundingSourceNoContent => CustomBy.DataTestId("");
    }
}
