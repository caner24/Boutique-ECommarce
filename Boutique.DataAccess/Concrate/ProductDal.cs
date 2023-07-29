using Boutique.Core.DataAccess.EntityFramework;
using Boutique.DataAccess.Abstract;
using Boutique.Entities.Concrate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Concrate
{
    public class ProductDal : EfRepositoryBase<BoutiqueContext, Product>,IProductDal
    {

        public async Task<List<Product>> GetLast8Product()
        {
            using (BoutiqueContext boutiqueContext = new BoutiqueContext())
            {
                return await boutiqueContext.Product.Include(pd=>pd.ProductDetails).Include(ph=>ph.ProductDetails.Photos).OrderByDescending(_=>_.Id).Take(8).ToListAsync();
            }

        }
    }
}
