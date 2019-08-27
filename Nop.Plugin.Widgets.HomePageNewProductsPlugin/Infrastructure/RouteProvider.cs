using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //override some of default routes in Admin area
            //routeBuilder.MapRoute("Plugin.Widgets.HomePageNewProductsPlugin.TestRouting", "Plugins/WidgetsHomePageNewProducts/TestRouting",
            //    new { controller = "WidgetsHomePageNewProducts", action = "TestRouting" });
        }

        public int Priority => -1; 
    }
}


