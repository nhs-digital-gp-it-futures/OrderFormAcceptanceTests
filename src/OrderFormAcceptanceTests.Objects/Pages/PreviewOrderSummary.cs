using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public class PreviewOrderSummary
    {
        public By TopGetOrderSummaryLink => CustomBy.DataTestId("summary-page-orderSummaryButton-top","a");
        public By BottomGetOrderSummaryLink => CustomBy.DataTestId("summary-page-orderSummaryButton-bottom", "a");
    }
}
