
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class ServicesConfiguration
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabase, Database>();
            services.AddScoped<ITankService, TankService>();
        }
    }
}