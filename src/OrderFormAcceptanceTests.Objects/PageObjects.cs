using OrderFormAcceptanceTests.Objects.Collections;

namespace OrderFormAcceptanceTests.Objects
{
    public sealed class PageObjects
    {
        public PageObjects()
        {
            Pages = new PageCollection
            {
            };
        }
        public PageCollection Pages { get; set; }
    }
}
