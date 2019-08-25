using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin
{
    public class HomePageNewProductsSettings : ISettings
    {
        public int NumberOfAddedProducts { get; set; }
        public WidgetZone WidgetZone { get; set; }
    }
}
