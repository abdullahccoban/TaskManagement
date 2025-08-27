using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ILanguageService, LanguageService>();

            return services;
        }        
    }
}
