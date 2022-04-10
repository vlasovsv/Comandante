using System;
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
        
        /// <inheritdoc cref="IQueryDispatcher.Dispatch{TQuery,TQueryResult}"/>
        public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(
            IQuery<TQuery, TQueryResult> query,
            CancellationToken cancellationToken
        ) where TQuery : IQuery<TQuery, TQueryResult>
        {
            if (!(query is TQuery concreteQuery))
            {
                throw new ArgumentException($"Query must be an instance of {typeof(TQuery)}");
            }

            var handler = _serviceFactory.GetService<IQueryHandler<TQuery, TQueryResult>>();

            if (handler is null)
                throw new ComandanteException(
                    $"Handler was not found for query of type {typeof(TQuery)}. Register your handlers with the container.");

            return handler.Handle(concreteQuery, cancellationToken);
        }
    }
}