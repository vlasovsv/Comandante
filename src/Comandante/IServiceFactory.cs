using System;

namespace Comandante
{
    /// <summary>
    /// Creates an object that requested by dispatchers
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Creates a new service by requested type
        /// </summary>
        /// <param name="serviceType">A service type</param>
        /// <returns>
        /// Returns the requested service
        /// </returns>
        object GetService(Type serviceType);
    }
}