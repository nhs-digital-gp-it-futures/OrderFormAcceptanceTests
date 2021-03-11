using System;

namespace OrderFormAcceptanceTests.Domain
{
    public interface IAudited
    {
        void SetLastUpdatedBy(Guid userId, string userName);
    }
}
