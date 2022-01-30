using System.Threading;
using System.Threading.Tasks;

namespace Comandante
{
    /// <summary>
    /// A query handler to process queries
    /// </summary>
    /// <typeparam name="TQuery">A query type</typeparam>
    /// <typeparam name="TQueryResult">A query result</typeparam>
    public interface IQueryHandler<in TQuery, TQueryResult>
        where TQuery: IQuery<TQueryResult>
    {
        /// <summary>
        /// Asynchronously handles a single query
        /// </summary>
        /// <param name="query">A query</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>
        /// Returns a task that represents a query operation. The task result contains the query result
        /// </returns>
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}