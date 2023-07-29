using Boutique.Business.Abstract;
using Boutique.Core.Entity;
using Boutique.DataAccess.Abstract;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.Concrate
{
    public class ProductDetailManager : IProductDetailService
    {
        private readonly IProductDetailDal _productDetailManager;

        public ProductDetailManager(IProductDetailDal productDetailManager)
        {
            _productDetailManager = productDetailManager;
        }
        public async Task<ProductDetail> Add(ProductDetail entity)
        {
            return await _productDetailManager.Add(entity);
        }

        public int Delete(ProductDetail entity)
        {
            return _productDetailManager.Delete(entity);
        }

        public Task<ProductDetail> Get(Expression<Func<ProductDetail, bool>> filter,string includeProperties="")
        {
            return _productDetailManager.Get(filter, includeProperties);
        }

        public async Task<IEnumerable<ProductDetail>> GetAll(Expression<Func<ProductDetail, bool>> filter = null, Func<IQueryable<ProductDetail>, IOrderedQueryable<ProductDetail>> orderBy = null, string includeProperties = "")
        {
            return await _productDetailManager.GetAll(filter,orderBy,includeProperties);
        }

        public ProductDetail Update(ProductDetail entity)
        {
            return _productDetailManager.Update(entity);
        }
    }
}
