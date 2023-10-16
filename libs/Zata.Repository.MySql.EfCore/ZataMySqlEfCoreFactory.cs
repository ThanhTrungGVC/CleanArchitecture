using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.Repository.MySql.EfCore.Uow;

namespace Zata.Factory
{
    public static class ZataMySqlEfCoreFactory
    {
        public static void AddUnitOfWorkManager(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(IUnitOfWorkManager<>), typeof(UnitOfWorkManager<>));
            services.TryAddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static void AddDbContextMySql<TContext>(this IServiceCollection services, string? connectionString) where TContext : DbContext
        {
            services.AddDbContextPool<TContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }
    }
}
