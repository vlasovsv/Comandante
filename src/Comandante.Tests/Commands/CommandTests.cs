using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Comandante.Tests.Commands
{
    public class CommandTests
    {
        private IServiceProvider _provider;
        
        public CommandTests()
        {
            var conf = new ServiceCollection();
            conf.AddComandate(this.GetType().Assembly);
            conf.Decorate(typeof(ICommandHandler<,>), typeof(CommandDecorator<,>));
            _provider = conf.BuildServiceProvider();
        }
        
        [Fact]
        public async Task Command_Handle_As_Expected()
        {
            // ARRANGE
            var cmd = new CreateUserCommand("The one");
            var sut = _provider.GetRequiredService<ICommandDispatcher>();

            // ACT
            var userId = await sut.Dispatch(cmd, default);

            // ASSERT
            userId.Should().Be(42);
        }
        
        [Fact]
        public async Task Command_Without_Handler_Throws_Exception()
        {
            // ARRANGE
            var query = new MissingCommand();
            var sut = _provider.GetRequiredService<ICommandDispatcher>();

            // ACT + ASSERT
            await Assert.ThrowsAsync<ComandanteException>(() => sut.Dispatch(query, default));
        }
    }
}