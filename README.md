# 📚 Book Library API — Clean Architecture (.NET)

A modular **ASP.NET Core Web API** built with **Clean Architecture**, **SOLID principles**, **Entity Framework Core**, **SQL Server**, and **Fluent API** configurations.  
Designed with production-ready backend structure using **Domain–Application–Infrastructure–API** layering, **repository pattern**, **DTO mappings**, and **unit-tested** application services.

> This project serves as a real-world backend example suitable for enterprise use, interviews, and portfolio demonstration.

---

## 📐 Architecture Overview

This project is structured using **Clean Architecture**:

```
┌───────────────────────────────────────────────────────┐
│                        API Layer                       │◄───── Presentation (Controllers)
└───────────────────────────────────────────────────────┘
                            |
                            v              
┌───────────────────────────────────────────────────────┐
│                   Application Layer                    │◄───── Use Cases, DTOs, Services
└───────────────────────────────────────────────────────┘
                            |
                            v
┌───────────────────────────────────────────────────────┐
│                      Domain Layer                      │◄───── Entities, Business Rules
└───────────────────────────────────────────────────────┘
                            ^
                            |
┌───────────────────────────────────────────────────────┐
│                   Application Layer                    │
└───────────────────────────────────────────────────────┘
                            ^
                            |
┌───────────────────────────────────────────────────────┐
│                  Infrastructure Layer                  │◄───── EF Core, Repositories, SQL Server
└───────────────────────────────────────────────────────┘

Infrastructure depends on the **Domain/Application** layers (implements interfaces, uses entities),
while **Domain** and **Application** never depend on Infrastructure.
```

---

## 🗂 Folder Structure

```
BookLibrary/
│   BookLibrary.sln
│
├── BookLibrary.Api/                # API Layer: Controllers, DI, startup config
│
├── BookLibrary.Application/        # Use cases, DTOs, service layer
│
├── BookLibrary.Domain/             # Entities, business rules, interfaces
│
└── BookLibrary.Infrastructure/     # EF Core DbContext, Migrations, Repositories
```

---

## 🛠️ Tech Stack

* **.NET 10 / ASP.NET Core Web API**
* **C# 14**
* **Entity Framework Core 10 (SQL Server)**
* **xUnit** for unit testing
* **NSubstitute** for mocking repository layer
* **Swagger / OpenAPI**
* **Clean Architecture**
  (API/Infrastructure → Application → Domain)
* **Dependency Injection**

---

## ✨ Features

* CRUD operations for managing books
* Domain validation rules inside entities
* Repository abstraction + EF Core implementation
* Fluent API configurations for EF Core model building
* Asynchronous operations (`async/await`)
* DTO-based API responses (no leaking of EF entities)
* Clean layering following SOLID & DIP
* Unit tests for `BookService`
* SQL Server database + EF Core migrations
* Swagger UI for API testing

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_NAME/YOUR_REPO_NAME.git
cd YOUR_REPO_NAME
```

---

## 🧰 Configuration

### 2. Update the connection string

Edit: **BookLibrary.Api/appsettings.json**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BookLibraryDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Replace with your SQL Server instance if needed.

---

## 📦 Running Migrations

Open **Package Manager Console** in Visual Studio:

* **Startup Project:** `BookLibrary.Api`
* **Default Project:** `BookLibrary.Infrastructure`

### Create migration

```powershell
Add-Migration InitialCreate
```

### Apply migration

```powershell
Update-Database
```

This creates your `BookLibraryDb` and `Books` table.

---

## ▶️ Run the API

Inside Visual Studio:

* Press **F5**
* Swagger UI automatically opens:
  `https://localhost:{PORT}/swagger`

---

# 📚 API Endpoints

### **Books Controller (/api/books)**

| Method | Endpoint          | Description             |
| ------ | ----------------- | ----------------------- |
| GET    | `/api/books`      | Get all books           |
| GET    | `/api/books/{id}` | Get book by ID          |
| POST   | `/api/books`      | Create a new book       |
| PUT    | `/api/books/{id}` | Update an existing book |
| DELETE | `/api/books/{id}` | Delete a book           |

### Sample Create Request

```json
{
  "title": "Clean Architecture",
  "author": "Robert C. Martin",
  "price": 99.90
}
```

---

# 🧪 Unit Testing

The test project uses:

* **xUnit** → testing framework
* **NSubstitute** → mocking `IBookRepository`

Example test: `CreateAsync_Should_Create_Book_And_Return_Dto`

```csharp
var repo = Substitute.For<IBookRepository>();
var service = new BookService(repo);

var request = new BookCreateRequest("Clean Code", "Robert C. Martin", 99.90m);

var result = await service.CreateAsync(request);

Assert.Equal("Clean Code", result.Title);
await repo.Received(1).AddAsync(Arg.Any<Book>());
```

Run tests:

```
Test → Run All Tests
```

---

# 🔧 Future Improvements

* Add FluentValidation for request validation
* Add pagination/filtering for book list
* Add JWT authentication
* Async repository pattern with cancellation tokens
* Docker support
* Azure Deployment (Container Apps or App Service)
* Add integration tests (WebApplicationFactory)

---

# 📄 License

MIT License.

---

# ✏️ Author’s Note

This project aims to **demonstrate** how to structure a real-world ASP.NET Core backend with:

* Clean Architecture
* Testability
* Maintainability
* Separation of concerns
* Good domain modeling
* Clean layering

This project reflects my effort to better understand Clean Architecture by implementing it hands-on.
I welcome any feedback or corrections that can help me improve.
