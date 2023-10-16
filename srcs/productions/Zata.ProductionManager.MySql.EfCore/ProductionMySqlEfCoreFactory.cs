using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.ProductionManager.MySql.EfCore;

namespace Zata.Factory
{
    public static class ProductionMySqlEfCoreFactory
    {
        public static void AddProductionDbContextMySql(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContextMySql<ProductionDbContext>(connectionString);
        }
    }
}
