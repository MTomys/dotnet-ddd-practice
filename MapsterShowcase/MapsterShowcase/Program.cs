using Mapster;
using MapsterShowcase.Models;

Console.WriteLine("Hello, World!");

var traceId = Guid.NewGuid();
var user = new User("1", "First", "Last");

var config = TypeAdapterConfig.GlobalSettings;

TypeAdapterConfig<(User User, Guid TraceId), UserResponse>.NewConfig()
    .Map(dest => dest.TraceId, src => src.TraceId)
    .Map(dest => dest, src => src.User);

var userResponse = (user, traceId).Adapt<UserResponse>();

Console.WriteLine($"User -> {user}");
Console.WriteLine($"User response -> {userResponse}");