using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models
{
    public class ProductModel : BaseNopModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string SeName { get; set; }
        public ProductPicture Picture { get; set; }
    }
}
