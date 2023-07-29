using Boutique.Business.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;
using Boutique.Entities.Concrate;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Boutique.Models;
using System.Threading;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Boutique.DataAccess.Concrate;
using System.Collections.Generic;
using Boutique.Business.Abstract;
using Boutique.MailService;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Boutique.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public AdminController(IEmailSender emailSender, UserManager<User> userManager, UnitOfWorkManager unitOfWorkManager)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync("Visitor");
            var product = await _unitOfWorkManager.ProductService.GetAll(includeProperties: "ProductDetails.Photos");
            return View(new AdminIndexViewModel { Products = product, Users = users });
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View(new AdminProductViewModel { Categories = await _unitOfWorkManager.CategoryService.GetAll() });
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, int[] categories, IFormFileCollection formFiles)
        {
            if (!ModelState.IsValid)
            {
               var productCategories = await _unitOfWorkManager.CategoryService.Table.Where(t => categories.Contains(t.Id)).AsNoTracking().ToListAsync();
                if (formFiles != null)
                {
                    foreach (var item in formFiles)
                    {

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", item.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                        var image = new Bitmap(path);
                        product.ProductDetails.Photos.Add(new Photos { PhotoName = item.FileName, PhotoArr = Models.ImageConverter.ImageToByteArray(image) });
                    }
                }
                var productId = await _unitOfWorkManager.ProductService.Add(product);
                foreach (var item in productCategories)
                {
                    await _unitOfWorkManager.ProductCategoryService.Add(new ProductCategory() { CategoryId = item.Id, ProductId = productId.Id });
                }
            }
            return View(new AdminProductViewModel { Categories = await _unitOfWorkManager.CategoryService.GetAll() });
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(string userId)
        {
            var user = await _unitOfWorkManager.VisitService.GetAll(filter: x => x.User.Id == userId, includeProperties: "User");
            return View(user);
        }
        [HttpPost]
        public IActionResult UserDetail(User user)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var products = await _unitOfWorkManager.ProductService.Get(x => x.Id == productId);
            _unitOfWorkManager.ProductService.Delete(products);

            await _unitOfWorkManager.ProductCategoryService.Table.Where(x => x.ProductId == productId).ExecuteDeleteAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            return View(new AdminProductViewModel
            {
                Product = await _unitOfWorkManager.ProductService.Table.AsNoTracking().Include(x => x.ProductDetails.Photos).Include(x => x.Categories).ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Id == productId)
             ,
                Categories = await _unitOfWorkManager.CategoryService.GetAll()
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(IFormFileCollection formFiles, AdminProductViewModel products, int[] categories)
        {
                int indis = 0;
                var photoList = await _unitOfWorkManager.PhotosService.Table.AsNoTracking().Where(x => x.ProductDetailId == products.Product.Id).ToListAsync();
            products.Product.ProductDetails.Id = products.Product.Id;

                if (formFiles != null)
                {
                    foreach (var item in formFiles)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", item.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                        var image = new Bitmap(path);
                        photoList[indis].PhotoArr = Models.ImageConverter.ImageToByteArray(image);
                        photoList[indis].PhotoName = item.FileName;
                        indis++;
                    }
                products.Product.ProductDetails.Photos = photoList;
                }
                _unitOfWorkManager.ProductService.Update(products.Product);
                await _unitOfWorkManager.ProductCategoryService.Table.Where(x => x.ProductId == products.Product.Id).ExecuteDeleteAsync();
                foreach (var item in categories)
                {
                    await _unitOfWorkManager.ProductCategoryService.Add(new ProductCategory() { CategoryId = item, ProductId = products.Product.Id });
                }
            return View(new AdminProductViewModel
            {
                Product = await _unitOfWorkManager.ProductService.Table.AsNoTracking().Include(x => x.ProductDetails.Photos).Include(x => x.Categories).ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Id == products.Product.Id)
            ,
                Categories = await _unitOfWorkManager.CategoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult NewsLetter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewsLetter(string ckeditor)
        {

            var newsLetterUsers = await _unitOfWorkManager.NewsLetterUserService.GetAll();
            if (newsLetterUsers.Any())
            {
                var message = new Message(newsLetterUsers.Select(x => x.Email).ToList(), "Yeni Ürünler ", ckeditor, null);
                await _emailSender.SendEmailAsync(message);
                await _unitOfWorkManager.NewsLetterService.Add(new NewsLetter { SubTitle = ckeditor, _NewsDate = DateTime.UtcNow });
            }

            return View();
        }

    }
}
