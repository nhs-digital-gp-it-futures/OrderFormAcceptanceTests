﻿namespace OrderFormAcceptanceTests.Domain
{
    public sealed class OrderProgress
    {
        public int OrderId { get; init; }

        public bool CatalogueSolutionsViewed { get; set; }

        public bool AdditionalServicesViewed { get; set; }

        public bool AssociatedServicesViewed { get; set; }
    }
}
