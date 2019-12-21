using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.Mq
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterMqServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<SendMessageConsumer>();
            services.AddMassTransit(c =>
            {
                c.AddConsumer<SendMessageConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(
           cfg =>
           {
               var host = cfg.Host("localhost", "/", h => { });

               cfg.ReceiveEndpoint(host, "web-service-endpoint", e =>
               {
                   e.PrefetchCount = 16;
                   //e.UseMessageRetry(x => x.(2, 100));
                   //e.UseMessageRetry(r =>
                   //{
                   //    r.Handle<ArgumentNullException>();
                   //    r.Ignore(typeof(InvalidOperationException), typeof(InvalidCastException));
                   //    r.Ignore<ArgumentException>(t => t.ParamName == "orderTotal");
                   //});

                   e.LoadFrom(provider);
                   EndpointConvention.Map<SendMessageConsumer>(e.InputAddress);
               });
           }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();

            return services;
        }
    }
}
