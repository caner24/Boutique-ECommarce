using Boutique.Business.Abstract;
using Boutique.Core.DataAccess.EntityFramework;
using Boutique.Core.Entity;
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
    public class CategoryManager:EfQueryableRepository<Category,BoutiqueContext>, ICategoryService
    {
        private readonly ICategoryDal _categoryManager;

        public CategoryManager(ICategoryDal categoryManager)
        {
            _categoryManager = categoryManager;
        }


        public async Task<Category> Add(Category entity)
        {
            return await _categoryManager.Add(entity);
        }

        public int Delete(Category entity)
        {
            return _categoryManager.Delete(entity);
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> filter, string includeProperties = "")
        {
            return await _categoryManager.Get(filter, includeProperties);
        }

        public async Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
        {
          return await  _categoryManager.GetAll(filter,orderBy, includeProperties);
        }

        public async Task<List<Category>> GetFirst4Categories()
        {
            return await _categoryManager.GetFirst4Categories();
        }

        public Category Update(Category entity)
        {
            return _categoryManager.Update(entity);
        }
    }
}
