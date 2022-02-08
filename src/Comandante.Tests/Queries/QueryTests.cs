using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Comandante.Tests.Queries
{
    public class QueryTests
    {
        private IServiceProvider _provider;
        
        public QueryTests()
        {
            var conf = new ServiceCollection();
            conf.AddComandate(this.GetType().Assembly);
            _provider = conf.BuildServiceProvider();
        }
        
        [Fact]
        public async Task Query_Is_Null_Throws_ArgumentException()
        {
            // ARRANGE
            GetUserQuery query = null;
            var sut = _provider.GetRequiredService<IQueryDispatcher>();

            // ACT + ASSERT
            await Assert.ThrowsAsync<ArgumentException>(() => sut.Dispatch(query, default));
        }
        
        [Fact]
        public async Task Query_Handle_As_Expected()
        {
            // ARRANGE
            var query = new GetUserQuery(42);
            var sut = _provider.GetRequiredService<IQueryDispatcher>();

            // ACT
            var user = await sut.Dispatch(query, default);

            // ASSERT
            user.UserId.Should().Be(42);
            user.UserName.Should().Be("The one");
        }

        [Fact]
        public async Task Query_Without_Handler_Throws_Exception()
        {
            // ARRANGE
            var query = new MissingQuery();
            var sut = _provider.GetRequiredService<IQueryDispatcher>();

            // ACT + ASSERT
            await Assert.ThrowsAsync<ComandanteException>(() => sut.Dispatch(query, default));
        }
    }
}