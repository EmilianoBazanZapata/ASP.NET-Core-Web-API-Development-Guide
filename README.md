
# 🌐 ASP.NET Core Web API Development Guide

Este proyecto es una implementación práctica desarrollada como parte del curso **"Ultimate ASP.NET Core Web API Development Guide"** de **Trevoir Williams**. El objetivo es construir una API RESTful robusta y escalable utilizando las mejores prácticas y patrones recomendados en **ASP.NET Core**.

---

## 📋 Características
- **CRUD Completo**: Implementación de operaciones para gestionar recursos.
- **Entity Framework Core**: Uso de EF Core para manejar operaciones con bases de datos.
- **Autenticación y Autorización**:
  - Autenticación basada en **JWT (JSON Web Tokens)**.
  - Control de acceso mediante roles.
- **Manejo de Errores**: Middleware personalizado para el manejo centralizado de excepciones.
- **Logging**: Registro de logs utilizando **Serilog**.
- **Documentación**: Integración con **Swagger** para documentar y probar la API.
- **Despliegue en Azure**: Desplegado utilizando Azure App Services.

---

## 🛠️ Requisitos Previos
Asegúrate de tener los siguientes elementos configurados antes de iniciar:
- **.NET 6 SDK**
- **SQL Server** o **PostgreSQL** como base de datos.
- **Postman** o un cliente API para probar endpoints.
- **Visual Studio 2022** o cualquier editor compatible con .NET.

---

## 🚀 Configuración Local

### **1. Clona el Repositorio**
```bash
git clone https://github.com/EmilianoBazanZapata/ASP.NET-Core-Web-API-Development-Guide.git
cd aspnet-core-webapi-guide
```

### **2. Configura las Variables de Entorno**
Edita el archivo `appsettings.json` para establecer la cadena de conexión a la base de datos y la clave secreta de JWT:

#### Ejemplo de `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YourDatabase;User Id=your_user;Password=your_password;"
  },
  "JwtSettings": {
    "Key": "YourSecretKey12345!",
    "Issuer": "YourApp",
    "Audience": "YourAppUsers",
    "DurationInMinutes": 60
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "File",
        "Args": { "path": "Logs/log-.txt", "rollingInterval": "Day" }
      }
    ]
  }
}
```

### **3. Ejecuta Migraciones**
Genera y aplica las migraciones de la base de datos:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### **4. Ejecuta la API**
Inicia la aplicación localmente:
```bash
dotnet run
```

Por defecto, la API estará disponible en:
- **Swagger**: `https://localhost:5001/swagger`

---

## 🧪 Pruebas
- **Autenticación**:
  - Registra un usuario utilizando el endpoint `POST /api/auth/register`.
  - Inicia sesión en `POST /api/auth/login` para obtener un token JWT.
  - Usa el token en las solicitudes protegidas enviándolo en el encabezado:
    ```bash
    Authorization: Bearer <your-jwt-token>
    ```

- **Operaciones CRUD**:
  - Prueba los endpoints para crear, leer, actualizar y eliminar recursos.

---

## 🌐 Despliegue en Azure
El proyecto está configurado para desplegarse fácilmente en **Azure App Services**:
1. Publica la API desde Visual Studio o usando Azure CLI:
   ```bash
   az webapp up --name your-app-name --resource-group your-resource-group --runtime "DOTNET|6"
   ```

2. Configura las variables de entorno en Azure:
   - **ConnectionStrings:DefaultConnection**
   - **JwtSettings:Key**, **Issuer**, **Audience**

3. Verifica la API en la URL proporcionada por Azure.

---

## 📚 Recursos Relacionados
- **Curso de Trevoir Williams**: [Ultimate ASP.NET Core Web API Development Guide](https://www.udemy.com/course/ultimate-aspnet-5-web-api-development-guide/)
- **Documentación de ASP.NET Core**: [learn.microsoft.com](https://learn.microsoft.com/es-es/aspnet/core/)
- **Entity Framework Core**: [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- **Swagger**: [Swagger Documentation](https://swagger.io/)

---

## 🏆 Créditos
Este proyecto fue desarrollado como parte del curso de **Trevoir Williams**, quien enseña las mejores prácticas y herramientas para construir APIs con ASP.NET Core.
