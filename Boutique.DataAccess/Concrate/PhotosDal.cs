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
    public class PhotosDal:EfRepositoryBase<BoutiqueContext,Photos>,IPhotosDal
    {

    }
}
