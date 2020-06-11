using Demo.Users.Application.Common.Interfaces;
using Demo.Users.Infrastructure.Persistence;
using Demo.Users.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Users.Infrastructure
{
    public static class DependencyInjection
    {
        private const string DbName = "in_memory_demo_db";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<IUsersDbContext, UsersDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: DbName));

            services.AddSingleton<IRabbitMQService, RabbitMQService>();

            return services;
        }
    }
}
