using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.Factory;
using Zata.Modules;

namespace Zata.Repository.MySql.EfCore
{
    public class ZataMySqlEfCoreModule : ZataModule
    {
        public ZataMySqlEfCoreModule(IServiceCollection services) : base(services)
        {
        }

        public override void ConfigureServices()
        {
            _services.AddUnitOfWorkManager();
        }
    }
}
