using System.Threading;
using System.Threading.Tasks;

namespace Comandante
{
    /// <summary>
    /// Send a single query to be handled by a single query handler.
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Asynchronously dispatches a query to a single query handler
        /// </summary>
        /// <param name="query">A query</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <typeparam name="TQuery">A query payload type</typeparam>
        /// <typeparam name="TQueryResult">A query result type</typeparam>
        /// <returns>
        /// Returns a task that represents a query operation. The task result contains a query handler response.
        /// </returns>
        Task<TQueryResult> Dispatch<TQuery, TQueryResult>(
            IQuery<TQuery, TQueryResult> query,
            CancellationToken cancellationToken
        ) where TQuery : IQuery<TQuery, TQueryResult>;
    }
}