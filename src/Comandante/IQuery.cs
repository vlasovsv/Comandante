namespace Comandante
{
    /// <summary>
    /// Represents a query
    /// </summary>
    /// <typeparam name="TQuery">A query payload type</typeparam>
    /// <typeparam name="TQueryResult">A query result</typeparam>
    public interface IQuery<TQuery, out TQueryResult> where TQuery : IQuery<TQuery, TQueryResult>
    {
    }
}