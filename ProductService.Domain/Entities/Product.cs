namespace ProductService.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    private Product() { } // EF Core

    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void Update(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (Stock < quantity)
            throw new InvalidOperationException("Insufficient stock");

        Stock -= quantity;
    }
}
