using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.ProductionManager.Domain.Entities;
using Zata.ProductionManager.Domain.Repositories.EfCore;
using Zata.Repository.MySql.EfCore.Repositories;

namespace Zata.ProductionManager.MySql.EfCore.Repositories
{
    public class ProductEfCoreRepository : EfCoreRepository<ProductionDbContext, Product>, IProductEfCoreRepository
    {
        public ProductEfCoreRepository(ProductionDbContext dbContext) : base(dbContext)
        {
        }


    }
}
