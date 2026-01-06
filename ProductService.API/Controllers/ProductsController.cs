using Microsoft.AspNetCore.Mvc;
using ProductService.API.Contracts.Products;
using ProductService.Application.Common.Interfaces;
using ProductService.Domain.Entities;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _repo.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var product = new Product(request.Name, request.Price, request.Stock);
        await _repo.AddAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }
}
