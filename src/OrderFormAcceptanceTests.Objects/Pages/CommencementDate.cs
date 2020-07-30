using OpenQA.Selenium;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public class CommencementDate
    {
        public By Day => By.Id("commencementDate-day");
        public By Month => By.Id("commencementDate-month");
        public By Year => By.Id("commencementDate-year");
    }
}
