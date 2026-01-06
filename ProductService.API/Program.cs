using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductService.Application.Common.Interfaces;
using ProductService.Infrastructure.Persistence;
using ProductService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Use SQLite instead of PostgreSQL
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Repositories / DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Service API",
        Version = "v1"
    });
});

var app = builder.Build();

// Middleware / HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service API v1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}
app.MapControllers();
app.UseHttpsRedirection();

// // Example: Minimal API route for products (optional)
// app.MapGet("/products", async (IProductRepository repo) =>
// {
//     var products = await repo.GetAllAsync();
//     return Results.Ok(products);
// })
// .WithName("GetProducts")
// .WithOpenApi();

app.Run();
