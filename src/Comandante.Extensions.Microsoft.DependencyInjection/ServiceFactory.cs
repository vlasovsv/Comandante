using System;
using Microsoft.Extensions.DependencyInjection;

namespace Comandante
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public TService GetService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}