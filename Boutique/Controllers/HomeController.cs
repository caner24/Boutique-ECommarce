using AutoMapper;
using Boutique.Business.Abstract;
using Boutique.Business.Concrate;
using Boutique.Core.CrossCuttingConcerns.Caching;
using Boutique.Core.DataAccess.EntityFramework;
using Boutique.Core.Entity;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;
using Boutique.Entities.Concrate;
using Boutique.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Azure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Internal;
using System.Drawing;
using Serilog;

namespace Boutique.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private const string employeeListCacheKey = "productLists";
        private readonly ILogger<HomeController> _logger;
        private ICacheManager _cache;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private readonly SignInManager<User> _signInManager;
        public HomeController(SignInManager<User> signInManager, IMapper mapper, ILogger<HomeController> logger, UnitOfWorkManager unitOfWorkManager)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            await semaphore.WaitAsync();
            var products = await _unitOfWorkManager.ProductService.GetLast8Product();
            IndexViewModel indexViewModel = new IndexViewModel() { Products = products };
            semaphore.Release();
            return View(indexViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Products(int index = 1, string? categories = null, string? groupBy = "asc")
        {
            await semaphore.WaitAsync();
            var productsCount = categories == null ? await _unitOfWorkManager.ProductService.GetAll() : await _unitOfWorkManager.ProductService.GetAll(includeProperties: "Categories", filter: x => x.Categories.Any(y => y.Category.CategoryName == categories));
            var products = categories == null ? await _unitOfWorkManager.ProductService.GetAll(includeProperties: "ProductDetails.Photos", skip: index == 1 ? 0 : (index - 1) * 6, take: 6, orderBy: groupBy == "asc" ? x => x.OrderBy(_ => _.Price) : x => x.OrderByDescending(_ => _.Price)) : await _unitOfWorkManager.ProductService.GetAll(includeProperties: "ProductDetails.Photos", filter: x => x.Categories.Any(y => y.Category.CategoryName == categories), skip: index == 1 ? 0 : (index - 1) * 6, take: 6, orderBy: groupBy == "asc" ? x => x.OrderBy(_ => _.Price) : x => x.OrderByDescending(_ => _.Price));
            ProductListModel paggingInfo = new ProductListModel() { PagingInfo = new PagingInfo() { CurrentPage = index, ItemsPerPage = 6, TotalItems = productsCount.Count() }, Products = products, GroupBy = groupBy, Categories = categories };
            semaphore.Release();
            return View(paggingInfo);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            await semaphore.WaitAsync();
            var productDetail = _unitOfWorkManager.ProductService.Table.Include(x => x.ProductDetails.Photos).Include(x => x.Categories).ThenInclude(x => x.Category).Where(x => x.Id == productId).FirstOrDefault();
            semaphore.Release();
            return View(productDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNewsletter(string email = "")
        {
            var product = await _unitOfWorkManager.NewsLetterUserService.Table.AsNoTracking().Where(x => x.Email == email).FirstOrDefaultAsync();
            if (product == null)
            {
                NewsLetterUser newsLetterUser = new NewsLetterUser();
                newsLetterUser.Email = email;
                newsLetterUser._SubmitDate = DateTime.Now;
                newsLetterUser.IsActive = true;
                await _unitOfWorkManager.NewsLetterUserService.Add(newsLetterUser);
            }
            return Redirect("/");
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1, string isIncrease = "increase")
        {
            var product = await _unitOfWorkManager.ProductService.Get(x => x.Id == productId, "ProductDetails.Photos");
            if (product != null)
            {
                int cartCount = Convert.ToInt32(Request.Cookies["miniCartDetail"]);
                cartCount++;
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/"
                };
                Response.Cookies.Append("miniCartDetail", cartCount.ToString(), cookieOptions);
                var cart = GetCart();
                cart.AddProduct(product, quantity, isIncrease);
                SaveCart(cart);
            }
            return Redirect("/Home/Cart");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteToCart(int productId)
        {
            var product = await _unitOfWorkManager.ProductService.Get(x => x.Id == productId, "ProductDetails.Photos");
            int cartCount = Convert.ToInt32(Request.Cookies["miniCartDetail"]);
            cartCount--;
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                Path = "/"
            };
            Response.Cookies.Append("miniCartDetail", cartCount.ToString(), cookieOptions);
            var cart = GetCart();
            cart.RemoveProduct(product);
            SaveCart(cart);

            return Redirect("/Home/Cart");
        }

        private void SaveCart(CartViewModel cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var datas = GetCart();
            return View(datas);
        }

        private CartViewModel GetCart()
        {
            return HttpContext.Session.GetJSon<CartViewModel>("Cart") ?? new CartViewModel();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + upload.FileName;

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\newsLetterImg");

                var fullPath = Path.Combine(imagePath, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }
                var imageUrl = fullPath;
                return Ok(new { url = imageUrl });
            }


            return BadRequest();
        }
        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";
            return View("~/Views/Shared/HandleError.cshtml");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}