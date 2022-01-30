**Comandante** is a small and simple library that was developed to make easier developing CQRS applications.

## Setup
Install the package via NuGet first:
`Install-Package Comandante`

Comandante has separate pipeline for commands and queries. That principle let you optimize processing commands and queries independently from one another.

Comandante rests on dependency injection to create command and query handlers.
So to work correctly you have to add some of supported DI library such as:
* Comandante.Extensions.Microsoft.DependencyInjection

Otherwise, you have to implement `IServiceFactory`

```csharp
public class ServiceFactory : IServiceFactory
{   
    public TService GetService<TService>()
    {
        // Creates a new service
    }
}
```

### ASP.NET Core (or .NET Core in general)
To register all necessary Comandante handlers and dispatchers you can use Comandante.Extensions.Microsoft.DependencyInjection method
```csharp
public void ConfigureServices(IServiceCollection services)
{
  services.AddMvc();

  services.AddComandate(Assembly);
}
```

## Basics
Comandante provides two types of messages:
* `ICommand` for command operations that create something
* `IQuery` for query operations that retrieve some information

### Commands
Command message is just a simple POCO class that implements `ICommand` interface
```csharp
public class CreateUserCommand : ICommand<long>
{
    public CreateUserCommand(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; }
}
```

Next, create a handler:
```csharp
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, long>
{
    public Task<long> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(42l);
    }
}
```
Finally, send a command through the command dispatcher `ICommandDispatcher`:
```csharp
var cmd = new CreateUserCommand("test");
var userId = await dispatcher.Dispatch<CreateUserCommand, long>(cmd, default);
Debug.WriteLine(userId); // 42
```

### Queries
Query message is also a simple POCO class that implements `IQuery` interface
```csharp
public class GetUserQuery : IQuery<User>
{
    public GetUserQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; }
}
```

Next, create a handler:
```csharp
public record User(long UserId, string UserName);

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    public Task<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(new User(42, "The one"));
    }
}
```
Finally, send a command through the command dispatcher `IQueryDispatcher`:
```csharp
var query = new GetUserQuery(42)
var user = await dispatcher.Dispatch<GetUserQuery, User>(query, default);
Debug.WriteLine(user.ToString()); // User { UserId = 42, UserName = The one }
```