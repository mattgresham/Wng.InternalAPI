using Superscribe.Owin;

namespace Wng.InternalApi.RouteModules
{
    public class DefaultModule : SuperscribeOwinModule
    {
        public DefaultModule()
        {
            this.Get["/"] = o => "Default Route";
        }
    }
}