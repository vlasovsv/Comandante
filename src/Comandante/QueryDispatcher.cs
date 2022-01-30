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
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TQueryResult">A query result</typeparam>
        /// <returns>
        /// Returns a task that represents a query operation. The task result contains a query handler response.
        /// </returns>
        public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TQueryResult>
        {
            var handler = _serviceFactory.GetService<IQueryHandler<TQuery, TQueryResult>>();

            return handler.Handle(query, cancellationToken);
        }
    }
}