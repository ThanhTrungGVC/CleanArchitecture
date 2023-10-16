using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.ProductionManager.Domain.Entities;

namespace Zata.ProductionManager.MySql.EfCore
{
    public interface IProductionDbContext
    {
        DbSet<Product> Products { get; }
    }
}
