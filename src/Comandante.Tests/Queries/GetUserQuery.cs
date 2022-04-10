namespace Comandante.Tests.Queries
{
    public class GetUserQuery : IQuery<GetUserQuery, User>
    {
        public GetUserQuery(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; }
    }
}