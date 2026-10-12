**English** · [Español](README.es.md)

# 🏨 Hotel Listing API

![.NET](https://img.shields.io/badge/.NET-6-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-EF%20Core-CC2927?logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT%20%2B%20Identity-000000?logo=jsonwebtokens&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-App%20Service-0078D4?logo=microsoftazure&logoColor=white)

**A REST API for countries and hotels, with JWT authentication, roles, pagination and centralized error handling.**

---

## About the project

A Web API that manages a catalog of countries and their hotels. Anyone can register and log in; reading requires a valid token, and creating, updating or deleting requires the Administrator role.

It's a training project built while following Trevoir Williams' course [*Ultimate ASP.NET Core Web API Development Guide*](https://www.udemy.com/course/ultimate-aspnet-5-web-api-development-guide/) (Udemy), and I deployed it to Azure App Service.

## Features

- CRUD for countries and hotels, with DTOs mapped by AutoMapper.
- Registration, login and **refresh tokens** with ASP.NET Core Identity and JWT.
- **Roles** (`Administrator`, `User`) seeded in the database and enforced per endpoint.
- **Paginated** queries for countries and hotels.
- **Global exception middleware** that turns `NotFoundException` and `BadRequestException` into consistent HTTP responses.
- Structured logging with **Serilog**, to rolling files and to Seq.
- Response caching and Swagger documentation.

## Endpoints

| Area      | Endpoints                                                                        | Access              |
| --------- | -------------------------------------------------------------------------------- | ------------------- |
| Account   | `POST /api/Account/registerUser` · `registerAdministrator` · `login`             | Public              |
| Account   | `POST /api/Account/refreshToken`                                                 | Authenticated user  |
| Countries | `GET /api/Countries` (paged) · `GET {id}`                                        | Authenticated user  |
| Countries | `POST` · `PUT {id}` · `DELETE {id}`                                              | Administrator       |
| Hotels    | `GET /api/Hotels` (paged) · `GET {id}`                                           | Authenticated user  |
| Hotels    | `POST` · `PUT {id}` · `DELETE {id}`                                              | Administrator       |

## Technical highlights

- **Generic repository** (`IGenericRepository<T>`) with specific repositories for countries and hotels, so controllers never talk to EF Core directly.
- **Separate DTOs for each operation** (create, update, get, response), so the API contract doesn't leak the entities.
- **Token lifecycle:** short-lived JWTs plus refresh tokens, handled by `AuthManager`.
- **Errors in one place:** custom exceptions and a single middleware instead of try/catch in every controller.

<details>
<summary><b>Project structure</b></summary>

```text
HotelListing.NET6/HotelListing.API/
├── Configuration/      AutoMapper profile
├── Contracts/          Repository and auth interfaces
├── Controllers/        Account, Countries, Hotels
├── Data/               DbContext and seeds (countries, hotels, roles)
├── Exceptions/         NotFoundException, BadRequestException
├── Middleware/         ExceptionMiddleware
├── Models/             Entities, DTOs, paging
├── Repository/         Generic and specific repositories, AuthManager
└── Program.cs
```

</details>

## Getting started

### Requirements

- .NET 6 SDK
- SQL Server (LocalDB or Express works)
- EF Core CLI: `dotnet tool install --global dotnet-ef`

### Run locally

```bash
git clone https://github.com/EmilianoBazanZapata/ASP.NET-Core-Web-API-Development-Guide.git
cd ASP.NET-Core-Web-API-Development-Guide/HotelListing.NET6/HotelListing.API
```

1. Set the connection string `ConnectionStrings:DbConnection` in `appsettings.json` to your SQL Server.
2. Set your own JWT signing key, preferably with user secrets instead of `appsettings.json`:

   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "JwtSettings:Key" "<a long random key>"
   ```

3. Create the database. Migrations are not committed, so generate them first; the seeds add sample countries, hotels and both roles:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Run it with `dotnet run` and open Swagger at `https://localhost:7141/swagger`.

### Try it

Register with `POST /api/Account/registerUser`, log in with `POST /api/Account/login`, and send the token as `Authorization: Bearer <token>`. Create, update and delete require an account registered through `registerAdministrator`.

## Credits

Built by Emiliano Bazán-Zapata following the course by **Trevoir Williams** on Udemy.

---

**Binary Suns Studio** · [Website](https://binarysunsstudio.com.ar) · [GitHub](https://github.com/EmilianoBazanZapata) · [LinkedIn](https://www.linkedin.com/in/emiliano-bazán-zapata-18705721a)
