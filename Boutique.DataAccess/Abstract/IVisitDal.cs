﻿using Boutique.Core.DataAccess;
using Boutique.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Abstract
{
    public interface IVisitDal:IEntityRepository<Visits>
    {
    }
}
