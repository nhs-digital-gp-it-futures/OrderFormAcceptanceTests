namespace OrderFormAcceptanceTests.TestData.Models
{
    using OrderFormAcceptanceTests.Domain;

    public sealed class CatalogueItemModel
    {
        public string CatalogueItemId { get; init; }

        public CatalogueItemType CatalogueItemType { get; set; }

        public CatalogueItemId? ParentCatalogueItemId { get; set; }

        public string Name { get; set; }
    }
}
