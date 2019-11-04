using Gucm.Data.Context;
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

            return services;
        }
    }
}
