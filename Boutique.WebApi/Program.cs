using Boutique.Business.Abstract;
using Boutique.Business.Concrate;
using Boutique.Core.CrossCuttingConcerns.Caching;
using Boutique.Core.CrossCuttingConcerns.Caching.Microsoft;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProductService, Boutique.Business.Concrate.ProductManager>();
builder.Services.AddTransient<IProductDal, Boutique.DataAccess.Concrate.ProductDal>();

builder.Services.AddTransient<IProductDetailService, Boutique.Business.Concrate.ProductDetailManager>();
builder.Services.AddTransient<IProductDetailDal, Boutique.DataAccess.Concrate.ProductDetailDal>();

builder.Services.AddTransient<IPhotosService, PhotosService>();
builder.Services.AddTransient<IPhotosDal, PhotosDal>();


builder.Services.AddTransient<ICategoryService, Boutique.Business.Concrate.CategoryManager>();
builder.Services.AddTransient<ICategoryDal, Boutique.DataAccess.Concrate.CategoryDal>();



builder.Services.AddTransient<ICacheManager, MemoryCacheManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
