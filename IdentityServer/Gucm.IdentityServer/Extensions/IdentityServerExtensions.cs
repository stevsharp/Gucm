using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Gucm.IdentityServer.Services;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gucm.IdentityServer.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder AddDefaultIdentityServer(this IServiceCollection services)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            });
            return builder;
        }

        public static IIdentityServerBuilder WithDatabase(this IIdentityServerBuilder builder, string connectionString, string migrationsAssembly)
        {
            builder
                //this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    // this sets the token cleanup interval (in seconds). The default is 3600 (1 hour).
                    options.TokenCleanupInterval = 3600;
                });
            return builder;
        }

        public static IApplicationBuilder InitializeIdentityServerDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var configService = serviceScope.ServiceProvider.GetRequiredService<IConfigService>();

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                configService.AddClients(configService.GetClients());
                configService.AddApiResources(configService.GetApiResources());
                configService.AddIdentityResources();
            }
            return app;
        }

        public static void AddMappings(this IServiceCollection services)
        {
            var assembly = typeof(IdentityServerExtensions).Assembly;
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
            }, assembly);
        }

    }
}
