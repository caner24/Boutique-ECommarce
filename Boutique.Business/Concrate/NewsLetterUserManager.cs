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
    public class NewsLetterUserManager:EfQueryableRepository<NewsLetterUser,BoutiqueContext> , INewsLetterUserService
    {
        private readonly INewsLetterUserDal _newsLetterUserManager;
        public NewsLetterUserManager(INewsLetterUserDal newsLetterUserManager)
        {
            _newsLetterUserManager = newsLetterUserManager;
        }
        public async Task<NewsLetterUser> Add(NewsLetterUser entity)
        {
           return await _newsLetterUserManager.Add(entity);
        }

        public int Delete(NewsLetterUser entity)
        {
            return _newsLetterUserManager.Delete(entity);
        }

        public Task<NewsLetterUser> Get(Expression<Func<NewsLetterUser, bool>> filter, string includeProperties = "")
        {
            return _newsLetterUserManager.Get(filter, includeProperties);
        }

        public async Task<IEnumerable<NewsLetterUser>> GetAll(Expression<Func<NewsLetterUser, bool>> filter = null, Func<IQueryable<NewsLetterUser>, IOrderedQueryable<NewsLetterUser>> orderBy = null, string includeProperties = "")
        {
            return await _newsLetterUserManager.GetAll(filter, orderBy, includeProperties);
        }

        public NewsLetterUser Update(NewsLetterUser entity)
        {
            return _newsLetterUserManager.Update(entity);
        }
    }
}
