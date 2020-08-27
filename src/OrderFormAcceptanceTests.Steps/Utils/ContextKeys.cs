namespace OrderFormAcceptanceTests.Steps.Steps
{
    internal static class ContextKeys
    {
        internal const string CreatedOrder = nameof(CreatedOrder);
        internal const string CreatedCompletedOrders = nameof(CreatedCompletedOrders);
        internal const string CreatedIncompleteOrders = nameof(CreatedIncompleteOrders);
        internal const string CreatedContact = nameof(CreatedContact);
        internal const string CreatedAddress = nameof(CreatedAddress);
        internal const string CreatedSupplierAddress = nameof(CreatedSupplierAddress);
        internal const string CreatedServiceRecipient = nameof(CreatedServiceRecipient);
        internal const string CreatedSupplierContact = nameof(CreatedSupplierContact);
        internal const string CreatedOrderItem = nameof(CreatedOrderItem);
        internal const string CreatedOrderItems = nameof(CreatedOrderItems);
        internal const string CreatedAdditionalServiceOrderItem = nameof(CreatedAdditionalServiceOrderItem);
        internal const string CreatedOneOffOrderItems = nameof(CreatedOneOffOrderItems);
        internal const string CreatedRecurringOrderItems = nameof(CreatedRecurringOrderItems);

        internal const string CallOffAgreementId = nameof(CallOffAgreementId);
        internal const string ChosenItemId = nameof(ChosenItemId);
        internal const string ChosenSolutionId = nameof(ChosenSolutionId);
        internal const string ChosenOdsCode = nameof(ChosenOdsCode);

        internal const string EmailCount = nameof(EmailCount);
        internal const string SentEmail = nameof(SentEmail);
        internal const string ExpectedUrl = nameof(ExpectedUrl);
        internal const string ExpectedDescriptionValue = nameof(ExpectedDescriptionValue);
        internal const string ExpectedContact = nameof(ExpectedContact);
        internal const string ExpectedAddress = nameof(ExpectedAddress);

        internal const string AmendedEstimatedPeriod = nameof(AmendedEstimatedPeriod);
        internal const string AmendedQuantity = nameof(AmendedQuantity);
        internal const string AmendedPrice = nameof(AmendedPrice);
        internal const string AmendedDeliveryDate = nameof(AmendedDeliveryDate);

        internal const string NumberOfOrdersDisplayed = nameof(NumberOfOrdersDisplayed);
        internal const string OrderFromUI = nameof(OrderFromUI);

        internal const string User = nameof(User);
    }
}
