using Boutique.Business.Abstract;
using Boutique.Core.CrossCuttingConcerns.Caching;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;
using Boutique.Entities.Concrate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Concrate
{
    public class UnitOfWorkManager :IDisposable
    {
  
        private BoutiqueContext context = new BoutiqueContext();
        private IProductService productService ;
        private IProductDetailService productDetailService;
        private ICategoryService categoryService;
        private INewsLetterUserService newsLetterUserService;
        private IVisitService visitService;
        private IProductCategoryService productCategoryService;
        private INewsLetterService newsLetterService;
        private IPhotosService photosService;
        IServiceProvider _serviceProvider;
        public UnitOfWorkManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IProductService ProductService
        {
            get
            {
                if (this.productService == null)
                {
                    this.productService = new ProductManager(_serviceProvider.GetRequiredService<ILogger<Product>>(), _serviceProvider.GetRequiredService<IProductDal>(), _serviceProvider.GetRequiredService<ICacheManager>());
                }
                return productService;
            }
        }

        public IProductDetailService ProductDetailService
        {
            get
            {
                if (this.productDetailService == null)
                {
                    this.productDetailService = new ProductDetailManager(_serviceProvider.GetRequiredService<IProductDetailDal>());
                }
                return productDetailService;
            }
        }

        public ICategoryService CategoryService
        {
            get
            {
                if (this.categoryService == null)
                {
                    this.categoryService = new CategoryManager(_serviceProvider.GetRequiredService<ICategoryDal>());
                }
                return categoryService;
            }
        }
        public INewsLetterUserService NewsLetterUserService
        {
            get
            {
                if (this.newsLetterUserService == null)
                {
                    this.newsLetterUserService = new NewsLetterUserManager(_serviceProvider.GetRequiredService<INewsLetterUserDal>());
                }
                return newsLetterUserService;
            }
        }

        public IVisitService VisitService
        {
            get
            {
                if (this.visitService == null)
                {
                    this.visitService = new VisitManager(_serviceProvider.GetRequiredService<IVisitDal>());
                }
                return visitService;
            }
        }

        public IProductCategoryService ProductCategoryService
        {
            get
            {
                if (this.productCategoryService == null)
                {
                    this.productCategoryService = new ProductCategoryManager(_serviceProvider.GetRequiredService<IProductCategoryDal>());
                }
                return productCategoryService;
            }
        }

        public INewsLetterService NewsLetterService
        {
            get
            {
                if (this.newsLetterService == null)
                {
                    this.newsLetterService = new NewsLetterManager(_serviceProvider.GetRequiredService<INewsLetterDal>());
                }
                return newsLetterService;
            }
        }

        public IPhotosService PhotosService
        {
            get
            {
                if (this.photosService == null)
                {
                    this.photosService = new PhotosManager(_serviceProvider.GetRequiredService<IPhotosDal>());
                }
                return photosService;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
