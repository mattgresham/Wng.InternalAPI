using System.Collections.Generic;
using Wng.InternalApi.Domain;

namespace Wng.InternalApi.Interfaces
{
    public interface ICarterClaimRepository
    {
        IEnumerable<EaPolicySummary> GetPolicySummary(string policyNumber);
    }
}