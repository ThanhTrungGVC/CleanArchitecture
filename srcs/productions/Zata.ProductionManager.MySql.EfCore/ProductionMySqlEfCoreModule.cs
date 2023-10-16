using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.Modules;
using Zata.Repository.MySql.EfCore;
using Zata.Factory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Zata.ProductionManager.Domain.Repositories.EfCore;
using Zata.ProductionManager.MySql.EfCore.Repositories;
using Microsoft.Extensions.Configuration;

namespace Zata.ProductionManager.MySql.EfCore
{
    public class ProductionMySqlEfCoreModule : ZataModule
    {
        public ProductionMySqlEfCoreModule(IServiceCollection services) : base(services)
        {
        }

        public override void ConfigureServices()
        {
            AddEfCoreBaseModule();

            _services.TryAddScoped<IProductEfCoreRepository, ProductEfCoreRepository>();
        }

        private void AddEfCoreBaseModule()
        {
            _services.AddUnitOfWorkManager();
        }
    }
}
