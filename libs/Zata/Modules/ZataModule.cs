using Microsoft.Extensions.DependencyInjection;

namespace Zata.Modules
{
    /// <summary>
    /// Implement base module
    /// </summary>
    /// Created by nttrung 11.10.2023
    public abstract class ZataModule : IZataModule
    {
        protected readonly IServiceCollection _services;

        public ZataModule(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// Config service (options, DI, ...)
        /// </summary>
        /// Created by nttrung 11.10.2023
        public virtual void ConfigureServices()
        {
        }

        /// <summary>
        /// Config options (rebase easy write code)
        /// </summary>
        /// <typeparam name="TOptions">Class options config</typeparam>
        /// <param name="configureOptions">Action config</param>
        /// Created by nttrung 11.10.2023
        protected void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class
        {
            _services.Configure<TOptions>(configureOptions);
        }
    }
}
