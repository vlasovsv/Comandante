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
        /// <typeparam name="T">A service type</typeparam>
        /// <returns>
        /// Returns the requested service
        /// </returns>
        T GetService<T>();
    }
}