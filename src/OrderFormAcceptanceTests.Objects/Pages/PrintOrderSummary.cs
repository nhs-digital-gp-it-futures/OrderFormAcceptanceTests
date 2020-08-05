using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public class PrintOrderSummary
    {
        public By PrintableOrderSummary => CustomBy.DataTestId("printable-order-summary");
        public By PrintPreview => By.Name("print-preview-app");
    }
}
