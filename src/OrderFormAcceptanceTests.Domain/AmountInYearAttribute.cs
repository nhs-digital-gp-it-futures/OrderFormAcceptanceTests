using System;

namespace OrderFormAcceptanceTests.Domain
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AmountInYearAttribute : Attribute
    {
        public AmountInYearAttribute(int amountInYear)
        {
            AmountInYear = amountInYear;
        }

        public int AmountInYear { get; }
    }
}
