using Boutique.Core.DataAccess.EntityFramework;
using Boutique.DataAccess.Abstract;
using Boutique.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Concrate
{
    public class CategoryDal:EfRepositoryBase<BoutiqueContext, Category>,ICategoryDal
    {
        public IQueryable<Category> Table => throw new NotImplementedException();

        public async Task<List<Category>> GetFirst4Categories()
        {
            using (BoutiqueContext context = new BoutiqueContext())
            {
                return await context.Category.OrderBy(_=>_.Id).Take(4).ToListAsync();
            }
        }
    }
}
