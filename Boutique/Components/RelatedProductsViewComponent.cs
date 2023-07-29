using Boutique.Business.Abstract;
using Boutique.Business.Concrate;
using Boutique.Entities.Concrate;
using Boutique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boutique.Components
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public RelatedProductsViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var productDetail =  _categoryService.Table.Where(x=>x.Id==categoryId).Include(x => x.Products).ThenInclude(x => x.Product.ProductDetails.Photos).FirstOrDefault();
            return View(productDetail.Products.Select(x=>x.Product).Take(4).ToList());
        }
    }
}
