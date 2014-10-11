using System.Collections.Generic;
using System.Linq;

using Superscribe.Models;
using Superscribe.Owin;

namespace Wng.InternalAPI.Service.RouteModules
{
    public class DefaultModule : SuperscribeOwinModule
    {
        public DefaultModule()
        {
            this.Get["/"] = o => "Default Route";
        }
    }
}