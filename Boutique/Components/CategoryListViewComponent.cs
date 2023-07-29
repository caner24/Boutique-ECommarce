using Boutique.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Boutique.Components
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var returnData = await _categoryService.GetAll();
            return View(returnData);
        }
    }
}
