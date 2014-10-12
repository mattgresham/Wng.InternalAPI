using Superscribe.Models;
using Superscribe.Owin;
using Wng.InternalApi.Repositories;

namespace Wng.InternalApi.RouteModules
{
    public class EaPolicySummaryModule : SuperscribeOwinModule
    {
        public EaPolicySummaryModule()
        {
            this.Get["EaPolicySummary" / (String)"PolicyNumber"] = o =>
            {
                CarterClaimRepository policies = new CarterClaimRepository();
                return policies.GetPolicySummary(o.Parameters.PolicyNumber);
            };
        }
    }
}