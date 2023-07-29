using Boutique.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Boutique.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewComponent(ICategoryService categoryService)
        {
                _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var returnData = await _categoryService.GetFirst4Categories();
            return View(returnData);
        }
    }
}
