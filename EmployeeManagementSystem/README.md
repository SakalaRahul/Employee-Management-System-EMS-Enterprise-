# Employee Management System (EMS Enterprise)

An enterprise-grade Employee Management System built using **C#, .NET 10, ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core**, and **SQL Server**.

## Key Features & Architecture
- **Layered Architecture**: Decoupled Web API and MVC Frontend layers.
- **RESTful API**: Full CRUD endpoints for Employees and Departments with JWT Auth & Swagger UI.
- **ASP.NET Core Identity**: Secure role-based authorization (Admin & HR Manager).
- **Comprehensive Database Management**: Entity Framework Core with SQL Server, Fluent API mapping, and automatic database migration / seed data.
- **Unit Testing**: xUnit suite with in-memory database assertions.

---

## Installation & Execution Commands

### 1. Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/)
- SQL Server / LocalDB

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Apply Database Migrations
```bash
dotnet ef database update --project EmployeeManagementSystem.API
```

### 4. Build & Run
- **Run Web API**:
  ```bash
  dotnet run --project EmployeeManagementSystem.API
  ```
- **Run MVC Frontend**:
  ```bash
  dotnet run --project EmployeeManagementSystem.Web
  ```

### 5. Run Unit Tests
```bash
dotnet test
```

---

## Default Admin Credentials
- **Email**: `admin@ems.com`
- **Password**: `Admin123!`