using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Comandante
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddComandate(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.TryAddTransient<IServiceFactory, ServiceFactory>();
            services.TryAddTransient<ICommandDispatcher, CommandDispatcher>();
            services.TryAddTransient<IQueryDispatcher, QueryDispatcher>();

            var commandHandlerType = typeof(ICommandHandler<,>);
            var queryHandlerType = typeof(IQueryHandler<,>);

            services.Scan
            (
                x => x.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>))
                        .Where(_ => !_.IsGenericType))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                        .Where(_ => !_.IsGenericType))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );
            
            return services;
        }
    }
}