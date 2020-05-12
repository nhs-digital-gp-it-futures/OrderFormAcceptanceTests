﻿using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class Common
	{
		public By ErrorTitle => CustomBy.DataTestId("error-title");
		public By BackLink => By.ClassName("nhsuk-back-link__link");
		public By LoggedInDisplayName => CustomBy.DataTestId("loggedin-in-text");
	}
}
