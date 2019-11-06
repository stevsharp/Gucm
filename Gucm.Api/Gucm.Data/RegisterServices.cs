using Common.Infrastructure.UnitOfWork;
using Gucm.Data.Context;
using Gucm.Data.Repository;
using Gucm.Domain.Gdpr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gucm.Data
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContextPool<GucmDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay:
                        TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    sqlOptions.CommandTimeout(60);

                }).UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            services.AddDbContextPool<ODataDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ODataDBConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    sqlOptions.CommandTimeout(60);

                }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGdprDomainRepository, GdprDomainRepository>();

            return services;
        }
    }
}
