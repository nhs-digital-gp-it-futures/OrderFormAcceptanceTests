namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;

    public static class Login
    {
        public static By Username => By.Id("EmailAddress");

        public static By Password => By.Id("Password");

        public static By LoginButton => By.CssSelector("button[type=submit]");
    }
}
