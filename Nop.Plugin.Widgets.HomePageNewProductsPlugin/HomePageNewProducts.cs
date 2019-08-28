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
        private const string CONFIGURATION_PATH = "Admin/WidgetsHomePageNewProducts/Configure";
        //private const string GET_WIDGET_VIEW_COMPONENT_NAME = "WidgetsHomePageNewProducts";
        private const string GET_WIDGET_VIEW_COMPONENT_NAME = "WidgetsHomePageNewProductsPlugin";

        private const int DEFAULT_NUMBER_OF_ADDED_PRODUCTS = 4;
        private const string RESOURCE_NAME_WIDDGET_ZONE = "HomePageNewProductsPlugin.WidgetZone";
        private const string RESOURCE_VALUE_WIDDGET_ZONE = "Widget zone";
        private const string RESOURCE_NAME_NUMBER_OF_ADEED_PRODUCTS = "HomePageNewProductsPlugin.NumberOfAddedProducts";
        private const string RESOURCE_VALUE_NUMBER_OF_ADEED_PRODUCTS = "Number of products to display";

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
            return GET_WIDGET_VIEW_COMPONENT_NAME;
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
            return $"{_webHelper.GetStoreLocation()}{CONFIGURATION_PATH}";
        }
               
        public override void Install()
        {
            //settings
            var settings = new HomePageNewProductsSettings
            {
                NumberOfAddedProducts = DEFAULT_NUMBER_OF_ADDED_PRODUCTS,
                WidgetZone = WidgetZone.home_page_top,
            };

            _settingService.SaveSetting(settings);

            //locales
            _localizationService.AddOrUpdatePluginLocaleResource(RESOURCE_NAME_WIDDGET_ZONE, RESOURCE_VALUE_WIDDGET_ZONE);
            _localizationService.AddOrUpdatePluginLocaleResource(RESOURCE_NAME_NUMBER_OF_ADEED_PRODUCTS, RESOURCE_VALUE_NUMBER_OF_ADEED_PRODUCTS);

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageNewProductsSettings>();

            _localizationService.DeletePluginLocaleResource(RESOURCE_NAME_WIDDGET_ZONE);
            _localizationService.DeletePluginLocaleResource(RESOURCE_NAME_NUMBER_OF_ADEED_PRODUCTS);

            base.Uninstall();
        }
    }
}
