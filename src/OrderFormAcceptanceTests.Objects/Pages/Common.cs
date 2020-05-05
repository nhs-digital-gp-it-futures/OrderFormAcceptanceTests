using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class Common
	{
		public By ErrorTitle => CustomBy.DataTestId("error-title");
	}
}
