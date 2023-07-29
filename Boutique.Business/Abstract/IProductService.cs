using Boutique.Core.DataAccess;
using Boutique.Core.Entity;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Abstract
{
    public interface IProductService : IQueryableRepository<Product>
    {
        public Task<Product> Get(Expression<Func<Product, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<Product>> GetAll(
           Expression<Func<Product, bool>> filter = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
           string includeProperties = "",int skip = 0, int take = 0);
        public Product Update(Product entity);
        public Task<Product> Add(Product entity);
        public int Delete(Product entity);
        Task<List<Product>> GetLast8Product();
    }
}
