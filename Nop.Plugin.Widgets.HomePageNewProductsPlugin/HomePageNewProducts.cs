using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin
{
    public class HomePageNewProductsPlugin : BasePlugin, IWidgetPlugin
    {
        private const string RESOURCE_NAME_WIDDGET_ZONE = "HomePageNewProductsPlugin.WidgetZone";
        private const string RESORCE_NAME_NUMBER_OF_ADEED_PRODUCTS = "HomePageNewProductsPlugin.NumberOfAddedProducts";

        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;       

        #endregion

        #region Ctor

        public HomePageNewProductsPlugin(ILocalizationService localizationService,
            IWebHelper webHelper,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _settingService = settingService;
        }

        #endregion

        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return $"WidgetsHomePageNewProducts";
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                PublicWidgetZones.HomepageTop,
                PublicWidgetZones.HomepageBeforeCategories,
                PublicWidgetZones.HomepageBeforeProducts,
                PublicWidgetZones.HomepageBeforeBestSellers,
                PublicWidgetZones.HomepageBeforeNews,
                PublicWidgetZones.HomepageBeforePoll,
                PublicWidgetZones.HomepageBottom
            };
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/WidgetsHomePageNewProducts/Configure";
        }
               
        public override void Install()
        {
            //settings
            var settings = new HomePageNewProductsSettings
            {
                NumberOfAddedProducts = 4,
                WidgetZone = WidgetZone.home_page_top,
            };

            _settingService.SaveSetting(settings);

            //locales
            _localizationService.AddOrUpdatePluginLocaleResource(RESOURCE_NAME_WIDDGET_ZONE, "Widget zone");
            _localizationService.AddOrUpdatePluginLocaleResource(RESORCE_NAME_NUMBER_OF_ADEED_PRODUCTS, "Number of products to display");

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageNewProductsSettings>();

            _localizationService.DeletePluginLocaleResource(RESOURCE_NAME_WIDDGET_ZONE);
            _localizationService.DeletePluginLocaleResource(RESORCE_NAME_NUMBER_OF_ADEED_PRODUCTS);

            base.Uninstall();
        }
    }
}
