using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //override some of default routes in Admin area
            //routeBuilder.MapRoute("Plugin.Widgets.HomePageNewProductsPlugin.Configure", "",
            //    new { controller = "WidgetsHomePageNewProducts", action = "Configure", area = AreaNames.Admin });
        }

        public int Priority => -1; 
    }
}
