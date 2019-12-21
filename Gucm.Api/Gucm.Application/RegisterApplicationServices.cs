using Gucm.Application.ViewModel;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gucm.Application
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMediatR(typeof(CreateGdprCommand));
            
            return services;
        }
    }

    public static class MediatorPair
    {
        //public static IEnumerable<Type> FindUnmatchedRequests(Assembly assembly)
        //{
        //    var requests = assembly.GetTypes()
        //        .Where(t => t.IsClass && t.IsClosedTypeOf(typeof(IRequest<>)))
        //        .ToList();

        //    var handlerInterfaces = assembly.GetTypes()
        //        .Where(t => t.IsClass && (t.IsClosedTypeOf(typeof(IRequestHandler<>)) || t.IsClosedTypeOf(typeof(IRequestHandler<,>))))
        //        .SelectMany(t => t.GetInterfaces())
        //        .ToList();

        //    return (from request in requests
        //            let resultType = request.GetInterfaces()
        //                .Single(i => i.IsClosedTypeOf(typeof(IRequest<>)) && i.GetGenericArguments().Any())
        //                .GetGenericArguments()
        //                .First()
        //            let handlerType = resultType == typeof(Unit)
        //                ? typeof(IRequestHandler<>).MakeGenericType(request)
        //                : typeof(IRequestHandler<,>).MakeGenericType(request, resultType)
        //            where handlerInterfaces.Any(t => t == handlerType) == false
        //            select request).ToList();
        //}

    }
}
