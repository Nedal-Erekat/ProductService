# ProductService

ProductService is a microservice built with **ASP.NET Core** following a **clean architecture** approach.  
It manages products and provides CRUD operations via REST APIs. The service uses **SQLite** for local development.

---

## **Technologies**

- .NET 8 / C#
- ASP.NET Core Web API
- Entity Framework Core (EF Core)
- SQLite (for local development)
- Swagger / OpenAPI
- Clean Architecture (Domain, Application, Infrastructure, API layers)

---

## **Project Structure**

ProductService/
├─ ProductService.API # API project, entry point, controllers
├─ ProductService.Application # Application layer, business logic, interfaces
├─ ProductService.Infrastructure # Data access layer, EF Core, repositories
├─ ProductService.Domain # Domain entities and models


---

## **Getting Started**

### **Prerequisites**

- .NET 8 SDK
- Visual Studio / VS Code
- Optional: SQLite (installed automatically via NuGet)

### **Run the API**

1. Restore dependencies:
```bash
dotnet restore
dotnet build
```
2. Apply database migrations (creates SQLite DB):

`dotnet ef database update --project ProductService.Infrastructure --startup-project ProductService.API`

3. Run the API:

`dotnet run --project ProductService.API`

The API should be available at:

https://localhost:5001
https://localhost:5001/swagger/index.html
to explore and test API endpoints.

API Endpoints
Products
Method	URL	Description
GET	/api/products	Get all products
POST	/api/products	Create a new product

Create Product Request Example:

json
Copy code
{
  "name": "Product 1",
  "price": 99.99,
  "stock": 10
}
Development Notes
Repository Pattern is used for data access (IProductRepository).

Clean Architecture separates concerns into layers:

Domain → Entities

Application → Interfaces, business logic

Infrastructure → Repositories, DbContext

API → Controllers and Program setup

SQLite is used for simplicity in local development. You can replace it with PostgreSQL or SQL Server for production.

Swagger is integrated for API testing and documentation.

Future Enhancements
Add Update/Delete endpoints for products.

Implement integration events for cross-service communication.

Switch to PostgreSQL or SQL Server in production.

Add authentication and authorization.

Author
Nedal Erekat