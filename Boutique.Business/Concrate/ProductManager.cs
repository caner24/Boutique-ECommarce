using Boutique.Business.Abstract;
using Boutique.Core.CrossCuttingConcerns.Caching;
using Boutique.Core.DataAccess.EntityFramework;
using Boutique.Core.Entity;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;
using Boutique.Entities.Concrate;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Concrate
{
    public class ProductManager : EfQueryableRepository<Product, BoutiqueContext>, IProductService
    {
        private readonly IProductDal _productManager;
        private ICacheManager _cache;
        private ILogger<Product> _logger;
        public ProductManager(ILogger<Product> logger, IProductDal productManager, ICacheManager cache)
        {
            _logger = logger;
            _cache = cache;
            _productManager = productManager;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                _logger.LogInformation("Parameter : " + entity);
                return await _productManager.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Product did not add : " + entity);
                return await _productManager.Add(entity);
                throw;
            }
            finally
            {
                _logger.LogInformation("Exiting . . .  : ");
            }
        }

        public int Delete(Product entity)
        {
            return _productManager.Delete(entity);
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> filter, string includeProperties = "")
        {
            try
            {
                _logger.LogInformation("Product Getting With Cache . . .  :  ");
                if (_cache.IsAdd("productDetail"))
                {
                    return await _productManager.Get(filter, includeProperties);
                }
                _cache.Add("productDetail", await _productManager.Get(filter, includeProperties), 60);
                return _cache.Get<Product>("productDetail");
            }
            catch 
            {
                _logger.LogInformation("Product did not get : " );
                throw;
            }
            finally
            {
                _logger.LogInformation("Exitting method . . .");
            }
        }

        public Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "", int skip = 0, int take = 0)
        {
            return _productManager.GetAll(filter, orderBy, includeProperties, skip, take);
        }

        public async Task<List<Product>> GetLast8Product()
        {
            if (_cache.IsAdd("productListsIndex"))
            {
                return _cache.Get<List<Product>>("productListsIndex");
            }
            _cache.Add("productListsIndex", await _productManager.GetLast8Product(), 60);
            return _cache.Get<List<Product>>("productListsIndex");
        }

        public Product Update(Product entity)
        {
            return _productManager.Update(entity);
        }
    }
}
