﻿using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public sealed class OrderForm
	{
		public By DeleteOrderButton => CustomBy.DataTestId("delete-order-button");
		public By PreviewOrderButton => CustomBy.DataTestId("preview-order-button");
		public By SubmitOrderButton => CustomBy.DataTestId("submit-order-button");
		public By PageTitle => By.CssSelector("[data-test-id$='-page-title']");
		public By TaskList => CustomBy.DataTestId("task-list");
		public By SectionStatus => By.CssSelector("[data-test-id$='-complete-tag']");
		public By EditDescription => CustomBy.DataTestId("task-0-item-0-description");
		public By EditCallOffOrderingParty => CustomBy.DataTestId("task-1-item-0-description");
		public By GenericSection(string sectionHrefRoute) {
			return By.CssSelector(string.Format("[href$='{0}']", sectionHrefRoute));
		}
	}
}