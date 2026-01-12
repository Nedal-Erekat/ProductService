using BuildingBlocks.Contracts.Messaging;

namespace ProductService.API.EventBus;

public class ConsoleEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Type>> _handlers = new();
    private readonly IServiceProvider _serviceProvider;

    public ConsoleEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task PublishAsync<T>(T @event) where T : class
    {
        var eventType = typeof(T);

        if (!_handlers.TryGetValue(eventType, out var handlerTypes))
            return Task.CompletedTask;

        foreach (var handlerType in handlerTypes)
        {
            var handler = _serviceProvider.GetRequiredService(handlerType);

            // Safe cast instead of reflection invoke
            var method = handlerType.GetMethod(nameof(IEventHandler<T>.HandleAsync))
                         ?? throw new InvalidOperationException(
                             $"Handler {handlerType.Name} does not implement HandleAsync");

            method.Invoke(handler, new object[] { @event });
        }

        return Task.CompletedTask;
    }

    public void Subscribe<T, TH>()
        where T : class
        where TH : IEventHandler<T>
    {
        var eventType = typeof(T);
        var handlerType = typeof(TH);

        if (!_handlers.ContainsKey(eventType))
            _handlers[eventType] = new List<Type>();

        _handlers[eventType].Add(handlerType);
    }
}
