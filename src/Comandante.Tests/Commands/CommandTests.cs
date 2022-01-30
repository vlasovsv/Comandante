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
            var cmd = new CreateUserCommand("test");
            var sut = _provider.GetRequiredService<ICommandDispatcher>();

            // ACT
            var userId = await sut.Dispatch<CreateUserCommand, long>(cmd, default);

            // ASSERT
            userId.Should().Be(42);
        }
    }
}