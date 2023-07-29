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
    public interface ICategoryService:IQueryableRepository<Category>
    {
        public Task<Category> Get(Expression<Func<Category, bool>> filter,string includeProperties = "");
        public Task<IEnumerable<Category>> GetAll(
               Expression<Func<Category, bool>> filter = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
               string includeProperties = "");
        public Category Update(Category entity);
        public Task<Category> Add(Category entity);
        public int Delete(Category entity);
        Task<List<Category>> GetFirst4Categories();
    }
}
