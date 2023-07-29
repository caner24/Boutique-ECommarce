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
    public interface INewsLetterService:IQueryableRepository<NewsLetter>
    {
        public Task<NewsLetter> Get(Expression<Func<NewsLetter, bool>> filter, string includeProperties = "");
        public Task<IEnumerable<NewsLetter>> GetAll(
               Expression<Func<NewsLetter, bool>> filter = null,
        Func<IQueryable<NewsLetter>, IOrderedQueryable<NewsLetter>> orderBy = null,
               string includeProperties = "");
        public NewsLetter Update(NewsLetter entity);
        public Task<NewsLetter> Add(NewsLetter entity);
        public int Delete(NewsLetter entity);
    }
}
