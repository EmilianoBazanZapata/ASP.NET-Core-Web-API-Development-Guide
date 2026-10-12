[English](README.md) · **Español**

# 🏨 Hotel Listing API

![.NET](https://img.shields.io/badge/.NET-6-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-EF%20Core-CC2927?logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT%20%2B%20Identity-000000?logo=jsonwebtokens&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-App%20Service-0078D4?logo=microsoftazure&logoColor=white)

**API REST de países y hoteles, con autenticación JWT, roles, paginación y manejo centralizado de errores.**

---

## Sobre el proyecto

Una Web API que administra un catálogo de países y sus hoteles. Cualquiera puede registrarse e iniciar sesión; para leer hace falta un token válido, y para crear, modificar o eliminar hace falta el rol Administrator.

Es un proyecto de formación que hice siguiendo el curso de Trevoir Williams [*Ultimate ASP.NET Core Web API Development Guide*](https://www.udemy.com/course/ultimate-aspnet-5-web-api-development-guide/) (Udemy), y lo desplegué en Azure App Service.

## Características

- CRUD de países y hoteles, con DTOs mapeados con AutoMapper.
- Registro, login y **refresh tokens** con ASP.NET Core Identity y JWT.
- **Roles** (`Administrator`, `User`) cargados en la base mediante seeds y aplicados por endpoint.
- Consultas **paginadas** de países y hoteles.
- **Middleware global de excepciones** que convierte `NotFoundException` y `BadRequestException` en respuestas HTTP consistentes.
- Logging estructurado con **Serilog**, a archivos rotativos y a Seq.
- Response caching y documentación con Swagger.

## Endpoints

| Área      | Endpoints                                                                        | Acceso                |
| --------- | -------------------------------------------------------------------------------- | --------------------- |
| Account   | `POST /api/Account/registerUser` · `registerAdministrator` · `login`             | Público               |
| Account   | `POST /api/Account/refreshToken`                                                 | Usuario autenticado   |
| Countries | `GET /api/Countries` (paginado) · `GET {id}`                                     | Usuario autenticado   |
| Countries | `POST` · `PUT {id}` · `DELETE {id}`                                              | Administrator         |
| Hotels    | `GET /api/Hotels` (paginado) · `GET {id}`                                        | Usuario autenticado   |
| Hotels    | `POST` · `PUT {id}` · `DELETE {id}`                                              | Administrator         |

## Highlights técnicos

- **Repositorio genérico** (`IGenericRepository<T>`) con repositorios específicos para países y hoteles, así los controllers nunca hablan directamente con EF Core.
- **DTOs separados por operación** (crear, modificar, obtener, respuesta), así el contrato de la API no expone las entidades.
- **Ciclo de vida del token:** JWT de corta duración más refresh tokens, manejados por `AuthManager`.
- **Errores en un solo lugar:** excepciones propias y un único middleware, en lugar de try/catch en cada controller.

<details>
<summary><b>Estructura del proyecto</b></summary>

```text
HotelListing.NET6/HotelListing.API/
├── Configuration/      Perfil de AutoMapper
├── Contracts/          Interfaces de repositorios y autenticación
├── Controllers/        Account, Countries, Hotels
├── Data/               DbContext y seeds (países, hoteles, roles)
├── Exceptions/         NotFoundException, BadRequestException
├── Middleware/         ExceptionMiddleware
├── Models/             Entidades, DTOs, paginación
├── Repository/         Repositorios genérico y específicos, AuthManager
└── Program.cs
```

</details>

## Cómo ejecutar el proyecto

### Requisitos

- .NET 6 SDK
- SQL Server (sirve LocalDB o Express)
- CLI de EF Core: `dotnet tool install --global dotnet-ef`

### Ejecutar en local

```bash
git clone https://github.com/EmilianoBazanZapata/ASP.NET-Core-Web-API-Development-Guide.git
cd ASP.NET-Core-Web-API-Development-Guide/HotelListing.NET6/HotelListing.API
```

1. Configurar la cadena de conexión `ConnectionStrings:DbConnection` en `appsettings.json` con tu SQL Server.
2. Definir tu propia clave de firma JWT, preferentemente con user secrets en lugar de `appsettings.json`:

   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "JwtSettings:Key" "<una clave larga y aleatoria>"
   ```

3. Crear la base de datos. Las migraciones no están en el repo, así que primero hay que generarlas; los seeds cargan países y hoteles de ejemplo y los dos roles:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Ejecutar con `dotnet run` y abrir Swagger en `https://localhost:7141/swagger`.

### Probarlo

Registrarse con `POST /api/Account/registerUser`, iniciar sesión con `POST /api/Account/login` y enviar el token como `Authorization: Bearer <token>`. Para crear, modificar y eliminar hace falta una cuenta registrada con `registerAdministrator`.

## Créditos

Hecho por Emiliano Bazán-Zapata siguiendo el curso de **Trevoir Williams** en Udemy.

---

**Binary Suns Studio** · [Web](https://binarysunsstudio.com.ar) · [GitHub](https://github.com/EmilianoBazanZapata) · [LinkedIn](https://www.linkedin.com/in/emiliano-bazán-zapata-18705721a)
