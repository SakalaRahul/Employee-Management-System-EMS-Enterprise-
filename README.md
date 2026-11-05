# 🏢 Employee Management System (EMS Enterprise)

An enterprise-grade, full-featured **Employee Management System** built with **C# 14**, **.NET 10**, **ASP.NET Core MVC**, **ASP.NET Core Web API**, **Entity Framework Core 10**, and **SQL Server**.

This system features a decoupled architecture separating the RESTful Web API service layer, administrative MVC web application, and data persistence layers. It includes workforce analytics, multi-parameter search/filtering, department/designation management, ASP.NET Core Identity authentication with JWT token authorization, and automated unit/integration testing with xUnit.

---

## 📋 Table of Contents

1. [System Features & Modules](#-system-features--modules)
2. [Technology Stack](#-technology-stack)
3. [Architecture & System Design](#-architecture--system-design)
4. [Database Schema & Data Models](#-database-schema--data-models)
5. [UI Screenshots & Interface Layouts](#-ui-screenshots--interface-layouts)
6. [Getting Started & Local Installation](#-getting-started--local-installation)
7. [REST API Documentation & Swagger](#-rest-api-documentation--swagger)
8. [Testing & Quality Assurance](#-testing--quality-assurance)
9. [Role-Based Access Control & Seed Data](#-role-based-access-control--seed-data)
10. [Troubleshooting & Verification](#-troubleshooting--verification)

---

## 🚀 System Features & Modules

### 📊 Executive Workforce Dashboard
* **Real-Time HR Analytics:** Live counter cards displaying Total Employees, Active Workforce, Terminated/Inactive Staff, and Total Departments.
* **Department Distribution Charts:** Graphical representation of workforce distribution across various corporate departments.
* **Recent Onboardings:** Activity stream showing recently registered employees with join dates and assigned roles.

### 👥 Complete Employee Lifecycle Management
* **Employee Directory:** Server-side paginated data table supporting sorting by name, department, joining date, and salary.
* **Advanced Search & Filtering:** Dynamic search by Employee ID (`EMP-XXXX`), Name, or Email Address, alongside multi-select filters for Department, Designation, and Employment Status (`Active`, `Inactive`, `Resigned`).
* **Profile Management:** Detailed individual view showing personal details, contact info, job metadata, salary structure, and uploaded profile image.
* **Full CRUD Support:** Secure creation, updating, soft/hard deletion, and department transfers.

### 🏬 Department & Designation Management
* **Department Hierarchy:** Create, view, edit, and remove departments with active employee count tracking.
* **Job Titles & Designations:** Structured management of job designations tied to specific operational responsibilities.
* **Relational Integrity:** Foreign key constraints with delete protection to preserve organizational history when modifying structural units.

### 🔒 Enterprise Security & Web API Integration
* **Decoupled RESTful API:** Independent ASP.NET Core Web API engine serving JSON payloads over standard HTTP verbs (`GET`, `POST`, `PUT`, `DELETE`).
* **Authentication:** ASP.NET Core Identity integrated with JWT (JSON Web Token) Bearer authentication and cookie fallback.
* **Role-Based Security:** Granular access controls assigning `Admin` and `HRManager` roles with restricted administrative actions.

---

## 🛠 Technology Stack

| Layer / Component | Technology / Library |
| :--- | :--- |
| **Framework Runtime** | .NET 10.0 (C# 14) |
| **API Architecture** | ASP.NET Core Web API with DTOs and OpenAPI/Swagger |
| **Frontend UI** | ASP.NET Core MVC, Razor Views, Bootstrap 5, FontAwesome 6 |
| **Data Access / ORM** | Entity Framework Core 10 (Code-First with Fluent API) |
| **Database** | Microsoft SQL Server (LocalDB / Express / Enterprise) |
| **Security & Auth** | ASP.NET Core Identity & JWT Bearer Token Authentication |
| **Testing Suite** | xUnit, Moq, EF Core In-Memory Database |

---

## 📐 Architecture & System Design

The application follows **Clean Architecture** principles with a strict separation between API services, MVC client web interfaces, domain models, and data access layers.

```
                                  ┌─────────────────────────────────────────┐
                                  │   ASP.NET Core MVC Front-End Web App   │
                                  │   (Bootstrap 5 / Razor / JavaScript)   │
                                  └────────────────────┬────────────────────┘
                                                       │
                                            HTTP / JSON API Calls
                                                       │
                                                       ▼
                                  ┌─────────────────────────────────────────┐
                                  │      ASP.NET Core Web API Engine        │
                                  │   (JWT Auth / Swagger / Controllers)    │
                                  └────────────────────┬────────────────────┘
                                                       │
                                            Dependency Injection
                                                       │
                                                       ▼
                                  ┌─────────────────────────────────────────┐
                                  │    Service & Business Logic Layer       │
                                  │   (EmployeeService / DeptService)       │
                                  └────────────────────┬────────────────────┘
                                                       │
                                              EF Core 10 ORM
                                                       │
                                                       ▼
                                  ┌─────────────────────────────────────────┐
                                  │       SQL Server Database Instance      │
                                  │   (Identity / Employees / Departments)  │
                                  └─────────────────────────────────────────┘
```

---

## 💾 Database Schema & Data Models

```
 ┌─────────────────────────┐               ┌─────────────────────────┐
 │       Departments       │               │      Designations       │
 ├─────────────────────────┤               ├─────────────────────────┤
 │ PK  Id (int)            │               │ PK  Id (int)            │
 │     Name (nvarchar)     │               │     Title (nvarchar)    │
 │     Code (nvarchar)     │               │     Responsibilities    │
 │     Description         │               └────────────┬────────────┘
 └────────────┬────────────┘                            │
              │ 1                                       │ 1
              │                                         │
              │ N                                       │ N
 ┌────────────┴─────────────────────────────────────────┴────────────┐
 │                              Employees                            │
 ├───────────────────────────────────────────────────────────────────┤
 │ PK  Id (int)                                                      │
 │ UI  EmployeeCode (nvarchar(20))                                   │
 │ UI  Email (nvarchar(100))                                         │
 │     FirstName (nvarchar(50))                                      │
 │     LastName (nvarchar(50))                                       │
 │     Phone (nvarchar(20))                                          │
 │     DateOfBirth (datetime2)                                       │
 │     Gender (int enum)                                             │
 │     Address (nvarchar(250))                                       │
 │     JoiningDate (datetime2)                                       │
 │     Salary (decimal(18,2))                                        │
 │     Status (int enum: Active, Inactive, Resigned)                 │
 │     ProfileImagePath (nvarchar(250))                              │
 │ FK  DepartmentId (int)                                            │
 │ FK  DesignationId (int)                                           │
 └───────────────────────────────────────────────────────────────────┘
```

---

## 🖼 UI Screenshots & Interface Layouts

Below are visual layouts representing the primary administrative interfaces:

### 1. Executive Dashboard UI
```
+-----------------------------------------------------------------------------------+
|  [Logo] EMS Enterprise          [Employees]  [Departments]  [Reports]  (Admin v)  |
+-----------------------------------------------------------------------------------+
|  Workforce Dashboard Overview                                                     |
|                                                                                   |
|  +-------------------+  +-------------------+  +-------------------+  +---------+ |
|  |  Total Employees  |  | Active Workforce  |  | Inactive/Resigned |  | Depts   | |
|  |        124        |  |        118        |  |        6          |  |    5    | |
|  +-------------------+  +-------------------+  +-------------------+  +---------+ |
|                                                                                   |
|  +-------------------------------------+  +-------------------------------------+ |
|  | Department Distribution             |  | Recent Onboardings                  | |
|  | - IT: 45 Employees (36%)              |  | - Sarah Jenkins (Software Eng)      | |
|  | - HR: 12 Employees (10%)              |  | - Michael Chang (Financial Analyst) | |
|  +-------------------------------------+  +-------------------------------------+ |
+-----------------------------------------------------------------------------------+
```

### 2. Employee Directory & Search UI
```
+-----------------------------------------------------------------------------------+
|  Employee Directory                                         [+ Register Employee] |
+-----------------------------------------------------------------------------------+
|  [ Search by name, ID, email... ]  [ Dept: All v ] [ Status: Active v ] [Filter]  |
+----------+--------------------+-----------------+------------------+--------+-----+
| Emp ID   | Full Name          | Department      | Designation      | Status | Action
+----------+--------------------+-----------------+------------------+--------+-----+
| EMP-1001 | Rahul Sakala       | IT              | Sr. Developer    | Active | [View]
| EMP-1002 | Emily Watson       | Human Resources | HR Manager       | Active | [View]
| EMP-1003 | David Miller       | Finance         | Accountant       | Inact  | [View]
+----------+--------------------+-----------------+------------------+--------+-----+
| Showing 1 to 10 of 124 records                      [< Prev] [1] [2] [3] [Next >] |
+-----------------------------------------------------------------------------------+
```

---

## ⚙️ Getting Started & Local Installation

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/)
* SQL Server 2022, SQL Server Express, or LocalDB installed locally.

### Step-by-Step Execution Guide

1. **Extract Project Zip:**
   Extract `Employee-Management-System.zip` to your target directory.

2. **Configure Database Connection String:**
   Check `appsettings.json` inside `EmployeeManagementSystem.API`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Restore & Apply Migrations:**
   Open a terminal in the root solution folder and run:
   ```bash
   dotnet restore
   dotnet ef database update --project EmployeeManagementSystem.API
   ```

4. **Launch Web API Server:**
   ```bash
   dotnet run --project EmployeeManagementSystem.API
   ```
   * *The Web API runs at `https://localhost:7100` and `http://localhost:5100`.*
   * *Swagger UI is available at `https://localhost:7100/swagger`.*

5. **Launch MVC Web Application:**
   Open a second terminal window and run:
   ```bash
   dotnet run --project EmployeeManagementSystem.Web
   ```
   * *The Web Application runs at `https://localhost:7000`.*

---

## 🌐 REST API Documentation & Swagger

The solution provides a fully interactive **Swagger UI** for testing endpoints directly.

### Main API Routes

| Verb | Endpoint Route | Description | Auth Required |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/employees` | Search, filter, and fetch paged employees | Yes (Bearer JWT) |
| `GET` | `/api/employees/{id}` | Retrieve individual employee profile | Yes (Bearer JWT) |
| `POST` | `/api/employees` | Register a new employee record | Yes (`Admin`/`HRManager`) |
| `PUT` | `/api/employees/{id}` | Update employee metadata or salary | Yes (`Admin`/`HRManager`) |
| `DELETE` | `/api/employees/{id}` | Delete employee record | Yes (`Admin` Only) |
| `GET` | `/api/departments` | List all departments with headcount | Yes (Bearer JWT) |

---

## 🧪 Testing & Quality Assurance

Run the automated xUnit test suite using the .NET CLI:

```bash
dotnet test
```

### Test Coverage Includes:
* **Service Layer Unit Tests:** Validates creation, updates, and deletion logic using Moq and EF Core In-Memory providers.
* **Validation Testing:** Confirms proper response behavior when invalid email formats or negative salary values are supplied.
* **Search & Filter Operations:** Verifies LINQ filter queries for pagination and multi-parameter searching.

---

## 🔐 Role-Based Access Control & Seed Data

The database seeds initial development credentials automatically via `DbInitializer.cs`:

| Account Role | Email Address | Password | Privileges |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@ems.com` | `Admin123!` | Full System Access (All CRUD, API, and Deletion privileges) |
| **HR Manager**| `hr@ems.com` | `HrManager123!` | Employee Registration, Department Transfers, Updates |

---

## 🔍 Troubleshooting & Verification

* **Database Connection Issues:** Verify that `LocalDB` is running by executing `sqllocaldb info mssqllocaldb` in Command Prompt.
* **JWT Token Validation:** Ensure that both the API and Web projects share identical `Jwt:Key` values in their respective `appsettings.json` files.
* **Port Conflicts:** If ports `7100` or `7000` are occupied, update the binding URLs in `Properties/launchSettings.json`.
