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
    public interface INewsLetterUserService:IQueryableRepository<NewsLetterUser>
    {
        public Task<NewsLetterUser> Get(Expression<Func<NewsLetterUser, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<NewsLetterUser>> GetAll(
         Expression<Func<NewsLetterUser, bool>> filter = null,
  Func<IQueryable<NewsLetterUser>, IOrderedQueryable<NewsLetterUser>> orderBy = null,
         string includeProperties = "");
        public NewsLetterUser Update(NewsLetterUser entity);
        public Task<NewsLetterUser> Add(NewsLetterUser entity);
        public int Delete(NewsLetterUser entity);
    }
}
