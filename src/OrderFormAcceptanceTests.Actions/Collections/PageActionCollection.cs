﻿using OrderFormAcceptanceTests.Actions.Pages;

namespace OrderFormAcceptanceTests.Actions.Collections
{
    public sealed class PageActionCollection
    {
        public Authentication Authentication { get; set; }
        public Homepage Homepage { get; set; }
        public OrderForm OrderForm { get; set; }
        public OrganisationsOrdersDashboard OrganisationsOrdersDashboard { get; set; }
        public CommencementDate CommencementDate { get; set; }
        public AdditionalServices AdditionalServices { get; set; }
        public CompleteOrder CompleteOrder { get; set; }
        public DeleteOrder DeleteOrder { get; set; }
        public PreviewOrderSummary PreviewOrderSummary { get; set; }
    }
}
