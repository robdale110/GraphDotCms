using GraphDotCms.Application.Interfaces;
using GraphDotCms.Persistence.Configuration;
using GraphDotCms.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphDotCms.Persistence.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            services.AddScoped<IMongoDbContext, MongoDbContext>(context =>
                new MongoDbContext(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName));

            return services;
        }
    }
}
