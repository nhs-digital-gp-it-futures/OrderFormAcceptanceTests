namespace OrderFormAcceptanceTests.TestData.Extensions
{
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData.Models;

    public static class CatalogueItemExtensions
    {
        public static CatalogueItemModel ToModel(this CatalogueItem item)
        {
            if (item is null)
            {
                return null;
            }

            return new CatalogueItemModel
            {
                CatalogueItemId = item.Id.ToString(),
                Name = item.Name,
                CatalogueItemType = item.CatalogueItemType,
                ParentCatalogueItemId = item.ParentCatalogueItemId,
            };
        }

        public static CatalogueItem ToDomain(this CatalogueItemModel model)
        {
            return model is null
                ? null
                : new CatalogueItem
                {
                    Id = CatalogueItemId.ParseExact(model.CatalogueItemId),
                    Name = model.Name,
                    CatalogueItemType = model.CatalogueItemType,
                    ParentCatalogueItemId = model.ParentCatalogueItemId,
                };
        }
    }
}
