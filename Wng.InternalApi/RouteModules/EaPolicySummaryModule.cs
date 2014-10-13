using Superscribe.Models;
using Superscribe.Owin;
using Wng.InternalApi.Interfaces;
using Wng.InternalApi.Repositories;

namespace Wng.InternalApi.RouteModules
{
    public class EaPolicySummaryModule : SuperscribeOwinModule
    {
        private ICarterClaimRepository _carterClaimRepository;

        private ICarterClaimRepository CarterClaimRepository
        {
            get { return _carterClaimRepository ?? new CarterClaimRepository(); }
        }

        public void InjectCarterClaimRepository(ICarterClaimRepository carterClaimRepository)
        {
            _carterClaimRepository = carterClaimRepository;
        }

        public EaPolicySummaryModule()
        {
            this.Get["EaPolicySummary" / (String)"PolicyNumber"] =
                o => CarterClaimRepository.GetPolicySummary(o.Parameters.PolicyNumber);
        }
    }
}