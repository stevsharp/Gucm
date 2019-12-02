using System.Linq;
using Common.Api.Extensions;
using Common.Api.Middlewares;
using Common.Api.Validation;
using Common.Infrastructure.Bus;
using Common.Infrastructure.Notifications;
using Common.ServiceBus;
using FluentValidation.AspNetCore;
using Gucm.Application;
using Gucm.Application.Validation;
using Gucm.Data;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Gucm.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.AddOData();
            services.AddODataQueryFilter();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
                options.EnableEndpointRouting = false;
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<CreateGdprValidation>();
             });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Yield Api", Description = "Yield Api" });
            });


            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.RegisterDataServices(Configuration);
            services.RegisterApplicationServices(Configuration);

            services.AddSignalR();


            /* Configuration for authorization */

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                   // Policy here 
                });


            services
               .AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   var settings = new IdentityServerAuthenticationOptions();

                   Configuration.Bind("IdentityServerSettings", settings);

                   options.Authority = settings.Authority;
                   options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                   options.ApiName = settings.ApiName;
                   options.ApiSecret = settings.ApiSecret;
               });
            /********************************************* */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseCors("Cors");
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
            });

            app.UseMvc(routebuilder =>
            {
                routebuilder.MapODataServiceRoute("odata", "odata", EdmModel.GetEdmModel(app.ApplicationServices));
                // Workaround: https://github.com/OData/WebApi/issues/1175
                routebuilder.EnableDependencyInjection();
            });

            //app.UseHealthChecks("/health", new HealthCheckOptions()
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

            //});

            app.UseHealthChecksUI();

        }
    }
}
