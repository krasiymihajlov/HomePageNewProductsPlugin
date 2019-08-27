using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Components
{
    [ViewComponent(Name = "WidgetsHomePageNewProducts")]
    public class WidgetsHomePageNewProductsViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        public WidgetsHomePageNewProductsViewComponent(IStoreContext storeContext,
            IStaticCacheManager cacheManager,
            ISettingService settingService,
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var homeNewPageProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(_storeContext.CurrentStore.Id);
            var model = new PublicViewModel
            {
            };

            return View("~/Plugins/Widgets.HomePageNewProducts/Views/WidgetsHomePageNewProducts/PublicInfo.cshtml", model);
        }
    }
}
