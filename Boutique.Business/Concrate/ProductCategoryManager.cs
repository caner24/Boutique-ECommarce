using Boutique.Business.Abstract;
using Boutique.Core.DataAccess.EntityFramework;
using Boutique.DataAccess.Abstract;
using Boutique.DataAccess.Concrate;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Concrate
{
    public class ProductCategoryManager : EfQueryableRepository<ProductCategory, BoutiqueContext>,  IProductCategoryService
    {
        private readonly IProductCategoryDal _productCategoryDal;
        public ProductCategoryManager(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        public async Task<ProductCategory> Add(ProductCategory entity)
        {
            return await _productCategoryDal.Add(entity);
        }

        public int Delete(ProductCategory entity)
        {
           return _productCategoryDal.Delete(entity);
        }

        public Task<ProductCategory> Get(Expression<Func<ProductCategory, bool>> filter, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetAll(Expression<Func<ProductCategory, bool>> filter = null, Func<IQueryable<ProductCategory>, IOrderedQueryable<ProductCategory>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public ProductCategory Update(ProductCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
