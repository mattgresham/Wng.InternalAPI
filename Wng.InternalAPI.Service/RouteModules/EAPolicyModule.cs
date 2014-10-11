using System.Collections.Generic;
using System.Linq;

using Superscribe.Models;
using Superscribe.Owin;

using Wng.InternalAPI.Service.Repositories;

namespace Wng.InternalAPI.Service.RouteModules
{
    public class EAPolicyModule : SuperscribeOwinModule
    {
        public EAPolicyModule()
        {
            this.Get["EAPolicy" / (String)"policyNumber"] = o =>
            {
                EAPolicyRepository policies = new EAPolicyRepository();
                return policies.getPolicies(o.Parameters.policyNumber);
            };
        }
    }
}