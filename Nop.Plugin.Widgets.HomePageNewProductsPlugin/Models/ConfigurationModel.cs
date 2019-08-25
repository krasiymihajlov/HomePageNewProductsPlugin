using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageNewProductsPlugin.WidgetZone")]
        public WidgetZone WidgetZone { get; set; }
        public bool WidgetZone_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageNewProductsPlugin.NumberOfAddedProducts")]
        public int NumberOfAddedProducts { get; set; }
        public bool NumberOfAddedProducts_OverrideForStore { get; set; }
    }
}
