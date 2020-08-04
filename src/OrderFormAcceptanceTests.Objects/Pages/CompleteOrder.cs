using System;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public sealed class CompleteOrder
    {
        public By FundingSourceContent => CustomBy.DataTestId("TODO");
        public By DownloadPDF => CustomBy.DataTestId("TODO");
    }
}
