# School Management System API

A RESTful Web API built with ASP.NET Core for managing school data — students, teachers, and classes — following clean architecture principles.

## Features

- CRUD operations for core school entities (Students, Teachers, Classes)
- Clean architecture with separation of concerns
- Entity Framework Core (Code-First) with MS SQL Server
- Dependency injection throughout
- Environment-based configuration via `appsettings.json`
- Endpoints tested and documented with Postman

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- MS SQL Server
- Postman (for endpoint testing)

## Getting Started

### Prerequisites
- .NET SDK
- SQL Server (local or remote instance)

### Setup

```bash
git clone https://github.com/ausaf37/school-management-system-api.git
cd school-management-system-api
dotnet restore
```

Update the connection string in `appsettings.json` to match your local SQL Server instance.

Apply migrations:

```bash
dotnet ef database update
```

Run the project:

```bash
dotnet run
```

Open Swagger UI (if enabled) or test endpoints directly via Postman.

## Author

Ausaf Ahmed Chohan — [LinkedIn](https://linkedin.com/in/ausafahmedchohan) | [GitHub](https://github.com/ausaf37)
