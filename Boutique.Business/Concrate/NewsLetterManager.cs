using Boutique.Business.Abstract;
using Boutique.Core.DataAccess;
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
    public class NewsLetterManager : EfQueryableRepository<NewsLetter, BoutiqueContext>, INewsLetterService
    {
        private readonly INewsLetterDal _newsLetterDal;
        public NewsLetterManager(INewsLetterDal newsLetterDal)
        {
            _newsLetterDal = newsLetterDal;
        }

        public async Task<NewsLetter> Add(NewsLetter entity)
        {
            return await _newsLetterDal.Add(entity);
        }

        public int Delete(NewsLetter entity)
        {
            throw new NotImplementedException();
        }

        public Task<NewsLetter> Get(Expression<Func<NewsLetter, bool>> filter, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsLetter>> GetAll(Expression<Func<NewsLetter, bool>> filter = null, Func<IQueryable<NewsLetter>, IOrderedQueryable<NewsLetter>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public NewsLetter Update(NewsLetter entity)
        {
            throw new NotImplementedException();
        }
    }
}
