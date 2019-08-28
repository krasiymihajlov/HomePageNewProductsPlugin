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
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Common;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using System.Linq;
using Nop.Services.Helpers;
using Nop.Core.Domain.Media;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Components
{
    [ViewComponent(Name = Constans.VIEW_COMPONENT_NAME)]
    public class WidgetsHomePageNewProductsPluginViewComponent : NopViewComponent
    {
        private const string PUBLIC_INFO_VIEW_PATH = "~/Plugins/Widgets.HomePageNewProductsPlugin/Views/WidgetsHomePageNewProducts/PublicInfo.cshtml";
        private const string PICTURE_URL_MODEL_CACHE_KEY = "Nop.plugins.widgets.homepagenewproducts.pictureurl.for.product-{0}";

        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
        private readonly MediaSettings _mediaSettings;

        public WidgetsHomePageNewProductsPluginViewComponent(IStoreContext storeContext,
            IStaticCacheManager cacheManager,
            ISettingService settingService,
            IPictureService pictureService,
            IProductService productService,
            MediaSettings mediaSettings)
        {
            _storeContext = storeContext;
            _cacheManager = cacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _productService = productService;
            _mediaSettings = mediaSettings;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(_storeContext.CurrentStore.Id);
            if (homePageNewProductsSettings == null)
            {
                throw new NullReferenceException(nameof(homePageNewProductsSettings));
            }

            const int pageIndex = 0;

            var model = _productService
                .SearchProducts(pageIndex: pageIndex, pageSize: homePageNewProductsSettings.NumberOfAddedProducts, searchManufacturerPartNumber: false, 
                                searchSku: false, orderBy: ProductSortingEnum.CreatedOn)
                                 .Select(p => new PublicViewModel
                                 {

                                     ProductId = p.Id,
                                     Name = p.Name,
                                     PictureUrl = GetPictureUrl(p),
                                     Price = p.Price,
                                 });

            return View(PUBLIC_INFO_VIEW_PATH, model);
        }

        private string GetPictureUrl(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var cacheKey = string.Format(PICTURE_URL_MODEL_CACHE_KEY, product.Id);

            return _cacheManager.Get(cacheKey, () =>
            {
                var picture = _pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
                return _pictureService.GetPictureUrl(picture, _mediaSettings.ProductThumbPictureSize);
            });
        }
    }
}
