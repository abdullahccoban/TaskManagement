using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Services;

public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapping));
            services.AddScoped<IUserService, UserService>();

            return services;
        }        
    }
