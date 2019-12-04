using Common.Data.MongoCollection.Model;
using Common.Data.MongoCollection.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Data.MongoCollection
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterMongoDBDataServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();

            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            var databaseSettings = Configuration.GetSection("MongoDBDatabaseSettings").Get<DatabaseSettings>();

            services.AddTransient(_ => new RepositoryOptions(databaseSettings.ConnectionString, databaseSettings.DatabaseName, databaseSettings.CollectionName));

            return services;
        }
    }
}
