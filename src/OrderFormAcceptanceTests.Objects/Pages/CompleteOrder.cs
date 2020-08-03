using System;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public sealed class CompleteOrder
    {
        public By FundingSourceContent => CustomBy.DataTestId("");
        public By DownloadPDF => CustomBy.DataTestId("");
    }
}
