using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Abstract
{
    public interface IProductDetailService
    {
        public Task<ProductDetail> Get(Expression<Func<ProductDetail, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<ProductDetail>> GetAll(
         Expression<Func<ProductDetail, bool>> filter = null,
  Func<IQueryable<ProductDetail>, IOrderedQueryable<ProductDetail>> orderBy = null,
         string includeProperties = "");
        public ProductDetail Update(ProductDetail entity);
        public Task<ProductDetail> Add(ProductDetail entity);
        public int Delete(ProductDetail entity);
    }
}
