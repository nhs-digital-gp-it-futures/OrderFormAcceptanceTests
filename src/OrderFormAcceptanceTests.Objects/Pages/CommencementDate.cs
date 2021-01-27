namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;

    public static class CommencementDate
    {
        public static By Day => By.Id("commencementDate-day");

        public static By Month => By.Id("commencementDate-month");

        public static By Year => By.Id("commencementDate-year");
    }
}
