﻿using Boutique.Core.DataAccess.EntityFramework;
using Boutique.DataAccess.Abstract;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Concrate
{
    public class VisitsDal:EfRepositoryBase<BoutiqueContext,Visits>,IVisitDal
    {
    }
}
