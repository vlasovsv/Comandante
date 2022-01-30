namespace Comandante.Tests.Queries
{
    public class GetUserQuery : IQuery<User>
    {
        public GetUserQuery(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; }
    }
}