using Boutique.Business.Abstract;
using Boutique.Business.Concrate;
using Boutique.Business.CrosCuttingConcerns.Validation.FluentValidation;
using Boutique.Core.CrossCuttingConcerns.Caching;
using Boutique.Core.CrossCuttingConcerns.Caching.Microsoft;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;
using Boutique.DesignTime;
using Boutique.Entities.Concrate;
using Boutique.MailService;
using Boutique.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BoutiqueContext>(_ => _.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 7;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<BoutiqueContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromHours(2));
builder.Services.AddAutoMapper(typeof(Program));

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddAuthentication()
    .AddGoogle("google", opt =>
    {
        var googleAuth = builder.Configuration.GetSection("Authentication:Google");
        opt.ClientId = googleAuth["ClientId"];
        opt.ClientSecret = googleAuth["ClientSecret"];
        opt.SignInScheme = IdentityConstants.ExternalScheme;
    });

builder.Services.AddScoped<UnitOfWorkManager>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddTransient<IProductService, ProductManager>();
builder.Services.AddTransient<IProductDal, ProductDal>();

builder.Services.AddTransient<IProductDetailService, ProductDetailManager>();
builder.Services.AddTransient<IProductDetailDal, ProductDetailDal>();

builder.Services.AddTransient<IPhotosService, PhotosManager>();
builder.Services.AddTransient<IPhotosDal, PhotosDal>();


builder.Services.AddTransient<ICategoryService, CategoryManager>();
builder.Services.AddTransient<ICategoryDal, CategoryDal>();


builder.Services.AddTransient<INewsLetterDal, NewsLetterDal>();
builder.Services.AddTransient<INewsLetterService, NewsLetterManager>();

builder.Services.AddTransient<INewsLetterUserService, NewsLetterUserManager>();
builder.Services.AddTransient<INewsLetterUserDal, NewsLetterUserDal>();


builder.Services.AddTransient<IProductCategoryService, ProductCategoryManager>();
builder.Services.AddTransient<IProductCategoryDal, ProductCategoryDal>();

builder.Services.AddTransient<IVisitDal, VisitsDal>();
builder.Services.AddTransient<IVisitService, VisitManager>();

builder.Services.AddTransient<ICacheManager, MemoryCacheManager>();

builder.Services.AddScoped<IValidator<ProductDetail>, ProductDetailValidator>();

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSession();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Debug(new RenderedCompactJsonFormatter())
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/HandleError");
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.UseMiddleware<CustomAuthMiddleware>();
app.UseMiddleware<CookiesMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
SeedData.EnsurePopulated(app);
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();
    await SeedIdentity.CreateIdentityUsers(services, configuration);
}

app.Run();
