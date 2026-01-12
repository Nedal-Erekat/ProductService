using BuildingBlocks.Contracts.Events;
using BuildingBlocks.Contracts.Messaging;
using ProductService.Application.Common.Interfaces;

namespace ProductService.Application.EventHandlers;

public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly IProductRepository _productRepository;

    public OrderCreatedEventHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task HandleAsync(OrderCreatedEvent @event)
    {
        var product = await _productRepository.GetByIdAsync(@event.ProductId);

        if (product is null)
        {
            Console.WriteLine($"❌ Product {@event.ProductId} not found");
            return;
        }

        product.DecreaseStock(@event.Quantity);

        await _productRepository.UpdateAsync(product);

        Console.WriteLine(
            $"✅ Stock updated for Product {@event.ProductId}. Remaining: {product.Stock}");
    }
}
