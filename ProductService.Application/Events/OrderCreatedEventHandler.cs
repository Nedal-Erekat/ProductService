using BuildingBlocks.Contracts.Events;
using BuildingBlocks.Contracts.Messaging;

namespace ProductService.Application.Events;

public class OrderCreatedEventHandler 
    : IEventHandler<OrderCreatedEvent>
{
    public Task HandleAsync(OrderCreatedEvent @event)
    {
        Console.WriteLine(
            $"[ProductService] Order received. " +
            $"ProductId={@event.ProductId}, Qty={@event.Quantity}"
        );

        // 🔜 Later:
        // - Load product
        // - Reduce stock
        // - Save changes

        return Task.CompletedTask;
    }
}
