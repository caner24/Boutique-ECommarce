using Boutique.Core.DataAccess;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Abstract
{
    public interface IProductCategoryService : IQueryableRepository<ProductCategory>
    {
        public Task<ProductCategory> Get(Expression<Func<ProductCategory, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<ProductCategory>> GetAll(
         Expression<Func<ProductCategory, bool>> filter = null,
  Func<IQueryable<ProductCategory>, IOrderedQueryable<ProductCategory>> orderBy = null,
         string includeProperties = "");
        public ProductCategory Update(ProductCategory entity);
        public Task<ProductCategory> Add(ProductCategory entity);
        public int Delete(ProductCategory entity);
    }
}
