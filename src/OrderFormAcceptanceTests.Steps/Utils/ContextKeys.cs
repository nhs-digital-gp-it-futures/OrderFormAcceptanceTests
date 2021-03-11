namespace OrderFormAcceptanceTests.Steps.Utils
{
    internal static class ContextKeys
    {
        internal const string CreatedOrder = nameof(CreatedOrder);
        internal const string CreatedOrderItem = nameof(CreatedOrderItem);
        internal const string CreatedOrderItems = nameof(CreatedOrderItems);
        internal const string CreatedAdditionalServiceOrderItem = nameof(CreatedAdditionalServiceOrderItem);
        internal const string CreatedOneOffOrderItems = nameof(CreatedOneOffOrderItems);
        internal const string CreatedRecurringOrderItems = nameof(CreatedRecurringOrderItems);

        internal const string ChosenItemId = nameof(ChosenItemId);
        internal const string ChosenSolutionName = nameof(ChosenSolutionName);
        internal const string ChosenOdsCode = nameof(ChosenOdsCode);

        internal const string EmailCount = nameof(EmailCount);
        internal const string ExpectedUrl = nameof(ExpectedUrl);

        internal const string AmendedEstimatedPeriod = nameof(AmendedEstimatedPeriod);
        internal const string AmendedQuantity = nameof(AmendedQuantity);
        internal const string AmendedPrice = nameof(AmendedPrice);
        internal const string AmendedDeliveryDate = nameof(AmendedDeliveryDate);

        internal const string NumberOfOrdersDisplayed = nameof(NumberOfOrdersDisplayed);
        internal const string User = nameof(User);
        internal const string DbContext = nameof(DbContext);
        internal const string NumberOfPrices = nameof(NumberOfPrices);
    }
}
