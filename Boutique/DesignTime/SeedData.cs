using Boutique.DataAccess.Concrate;
using Boutique.Entities.Concrate;
using Boutique.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;
using System.Drawing;

namespace Boutique.DesignTime
{
    public static class SeedData
    {
        public static async void EnsurePopulated(WebApplication app)
        {
            var myConfiguration = app.Configuration;
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BoutiqueContext>();
            context.Database.Migrate();

            if (!context.Product.Any())
            {
                var categories = new[]
                {
                    new Category(){ CategoryName="Electronics"},
                     new Category(){ CategoryName="Earphones" },
                     new Category(){ CategoryName="T-Shirt" },
                      new Category(){ CategoryName="Parfume"},
                              new Category(){ CategoryName="Shoes"},
                        new Category(){ CategoryName="Watches" },
                        };
                context.Category.AddRange(categories);

                var products = new[]
           {
                    new Product(){ Price=250,
                        ProductDetails=new ProductDetail(){Amount=10,Color="White",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Kui Ye Chen’s AirPods",Photos=new List<Photos>(){new Photos(){PhotoName="Kui Ye Chen’s AirPods",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-1.jpg")))} } } },
                      new Product(){ Price=300,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Red",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Air Jordan 12 gym red",Photos=new List<Photos>(){new Photos(){PhotoName="Air Jordan 12 gym red",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-2.jpg"))
)} } } },
                        new Product(){ Price=25,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Cyan",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Cyan cotton t-shirt",Photos=new List<Photos>(){new Photos(){PhotoName="Cyan cotton t-shirt",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-3.jpg"))
)} } } },
                          new Product(){ Price=351,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Brown",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Timex Unisex Originals",Photos=new List<Photos>(){new Photos(){PhotoName="Timex Unisex Originals",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-4.jpg"))
)} } } },
                            new Product(){ Price=250,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Red",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Red digital smartwatch",Photos=new List<Photos>(){new Photos(){PhotoName="Red digital smartwatch",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-5.jpg"))
)} } } },
                              new Product(){ Price=300,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Green",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Nike air max 95",Photos=new List<Photos>(){new Photos(){PhotoName="Nike air max 95",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-6.jpg"))
)} } } },
                                new Product(){ Price=25,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Non-Colorized",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Joemalone Women prefume",Photos=new List<Photos>(){new Photos(){PhotoName="Joemalone Women prefume",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-7.jpg"))
)} } } },
                                  new Product(){ Price=351,
                        ProductDetails=new ProductDetail(){Amount=10,Color="Black",ProductDetailText="Lorem ipsum dolor sit amet, consectetur adipiscing elit. In ut ullamcorper leo, eget euismod orci. Cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus. Vestibulum ultricies aliquam convallis.", ProductName="Apple Watch",Photos=new List<Photos>(){new Photos(){PhotoName="Apple Watch",PhotoArr=Models.ImageConverter.ImageToByteArray(new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", "product-8.jpg"))
)} } } },
                };
                context.Product.AddRange(products);
                context.ProductCategory.AddRange(
                    new ProductCategory() { Product = products[0], Category = categories[0] },
                      new ProductCategory() { Product = products[0], Category = categories[1] },
                        new ProductCategory() { Product = products[1], Category = categories[4] },
                          new ProductCategory() { Product = products[2], Category = categories[2] },
                            new ProductCategory() { Product = products[3], Category = categories[0] },
                              new ProductCategory() { Product = products[4], Category = categories[0] },
                                new ProductCategory() { Product = products[4], Category = categories[5] },
                                  new ProductCategory() { Product = products[5], Category = categories[4] },
                                    new ProductCategory() { Product = products[6], Category = categories[3] },
                                      new ProductCategory() { Product = products[7], Category = categories[0] },
                                        new ProductCategory() { Product = products[7], Category = categories[5] }
                    );


                await context.SaveChangesAsync();
            }
        }
    }
}
