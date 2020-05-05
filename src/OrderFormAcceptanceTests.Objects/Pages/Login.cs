using OpenQA.Selenium;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class Login
	{
		public By Username => By.Id("EmailAddress");
		public By Password => By.Id("Password");
		public By LoginButton => By.CssSelector("button[type=submit]");
	}
}
