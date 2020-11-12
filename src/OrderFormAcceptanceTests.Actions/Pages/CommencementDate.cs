using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public sealed class CommencementDate : PageAction
    {
        public CommencementDate(IWebDriver driver) : base(driver)
        {
        }

        public bool IsPageDisplayed()
        {
            return Driver.FindElement(Pages.OrderForm.PageTitle).Text.Contains("Commencement date", StringComparison.OrdinalIgnoreCase);
        }

        public void SetDate(DateTime date)
        {
            SetDayValue(date.Day.ToString());
            SetMonthValue(date.Month.ToString());
            SetYearValue(date.Year.ToString());
        }

        public void SetDayValue(string day)
        {
            Driver.FindElement(Pages.CommencementDate.Day).SendKeys(day);
        }

        public void SetMonthValue(string month)
        {
            Driver.FindElement(Pages.CommencementDate.Month).SendKeys(month);
        }

        public void SetYearValue(string year)
        {
            Driver.FindElement(Pages.CommencementDate.Year).SendKeys(year);
        }

        public string GetDay()
        {
            return Driver.FindElement(Pages.CommencementDate.Day).GetAttribute("value");
        }

        public string GetMonth()
        {
            return Driver.FindElement(Pages.CommencementDate.Month).GetAttribute("value");
        }

        public string GetYear()
        {
            return Driver.FindElement(Pages.CommencementDate.Year).GetAttribute("value");
        }
    }
}
