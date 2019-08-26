using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models
{
    public class PublicViewModel : BaseNopModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
