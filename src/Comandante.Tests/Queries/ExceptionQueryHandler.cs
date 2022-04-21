using System;
using System.Threading;
using System.Threading.Tasks;

namespace Comandante.Tests.Queries
{
    public class ExceptionQueryHandler : IQueryHandler<ExceptionQuery, long>
    {
        public Task<long> Handle(ExceptionQuery query, CancellationToken cancellationToken)
        {
            throw new QueryException();
        }
    }
    
    public class ExceptionQuery : IQuery<ExceptionQuery, long>
    {
        
    }

    public class QueryException : Exception
    {
        
    }
}