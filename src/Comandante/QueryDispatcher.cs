using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Comandante
{
    /// <summary>
    /// Default query dispatcher implementation
    /// </summary>
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceFactory _serviceFactory;

        /// <summary>
        /// Creates a new query dispatcher
        /// </summary>
        /// <param name="serviceFactory">A service factory</param>
        public QueryDispatcher(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        /// <summary>
        /// Asynchronously dispatches a query to a single query handler
        /// </summary>
        /// <param name="query">A query</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <typeparam name="TQueryResult">A query result type</typeparam>
        /// <returns>
        /// Returns a task that represents a query operation. The task result contains a query handler response.
        /// </returns>
        public async Task<TQueryResult> Dispatch<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken)
        {
            if (query is null)
                throw new ArgumentException("Query cannot be null");

            var queryType = query.GetType();
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TQueryResult));

            var handler = _serviceFactory.GetService(handlerType);

            if (handler is null)
                throw new ComandanteException(
                    $"Handler was not found for query of type {queryType}. Register your handlers with the container.");
            
            var magicMethod = handlerType.GetMethod("Handle");

            try
            {
                var invocationResult = await (Task<TQueryResult>)magicMethod.Invoke(
                    handler, new object[] { query, cancellationToken });

                return invocationResult;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException ?? e;
            }
        }
    }
}