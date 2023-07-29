using Boutique.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Boutique.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductListController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductListController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ProductList> Get()
        {
            ProductList list = new ProductList();
            list.Products = await _productService.GetAll();
            return list;
        }
    }
}