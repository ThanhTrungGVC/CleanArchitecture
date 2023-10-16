using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.ProductionManager.Domain.Entities;
using Zata.Repository.MySql.EfCore.Repositories;

namespace Zata.ProductionManager.Domain.Repositories.EfCore
{
    public interface IProductEfCoreRepository : IEfCoreRepository<Product>
    {
        
    }
}
