using Superscribe.Models;
using Superscribe.Owin;
using Wng.InternalApi.Interfaces;

namespace Wng.InternalApi.RouteModules
{
    public class EaPolicySummaryModule : SuperscribeOwinModule
    {
        private ICarterClaimRepository _carterClaimRepository;

        public EaPolicySummaryModule(ICarterClaimRepository carterClaimRepository)
        {
            _carterClaimRepository = carterClaimRepository;

            this.Get["EaPolicySummary" / (String)"PolicyNumber"] = 
                o => _carterClaimRepository.GetPolicySummary(o.Parameters.PolicyNumber);
        }
    }
}