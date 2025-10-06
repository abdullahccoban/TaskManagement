using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Services;

public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapping));
            services.AddAutoMapper(typeof(GroupMapping));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStatusService, StatusService>();

            return services;
        }        
    }
