using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Controllers
{

    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AdminAntiForgery]
    public class WidgetsHomePageNewProductsController : BasePluginController
    {
        private const string CONFIGURE_VIEW_PATH = "~/Plugins/Widgets.HomePageNewProductsPlugin/Views/WidgetsHomePageNewProducts/Configure.cshtml";
        private const string RESOURCE_KEY = "Admin.Plugins.Saved";

        private readonly IPermissionService _permissionService;
        readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        public WidgetsHomePageNewProductsController(IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IProductService productService)
        {
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            {
                return AccessDeniedView();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);
            var model = new ConfigurationModel
            {
                WidgetZone = homePageNewProductsSettings.WidgetZone,
                NumberOfAddedProducts = homePageNewProductsSettings.NumberOfAddedProducts,
                ActiveStoreScopeConfiguration = storeScope,
            };

            if (storeScope > 0)
            {
                model.WidgetZone_OverrideForStore = _settingService.SettingExists(homePageNewProductsSettings, x => x.WidgetZone, storeScope);
                model.NumberOfAddedProducts_OverrideForStore = _settingService.SettingExists(homePageNewProductsSettings, x => x.NumberOfAddedProducts, storeScope);
            }

            return View(CONFIGURE_VIEW_PATH, model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);
            homePageNewProductsSettings.WidgetZone = model.WidgetZone;
            homePageNewProductsSettings.NumberOfAddedProducts = model.NumberOfAddedProducts;

            if (storeScope == 0)
            {
                _settingService.SaveSetting(homePageNewProductsSettings, x => x.NumberOfAddedProducts, storeScope, false);
                _settingService.SaveSetting(homePageNewProductsSettings, x => x.WidgetZone, storeScope, false);
            }
            else if (storeScope > 0)
            {
                _settingService.DeleteSetting(homePageNewProductsSettings, x => x.NumberOfAddedProducts, storeScope);
                _settingService.DeleteSetting(homePageNewProductsSettings, x => x.WidgetZone, storeScope);
            }

            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource(RESOURCE_KEY));

            return Configure();
        }
    }
}
