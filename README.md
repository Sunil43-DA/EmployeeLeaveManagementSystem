# Employee Leave Management System

A secure and scalable **Employee Leave Management System** built using **ASP.NET Core 8 Web API**, **Entity Framework Core**, and **SQL Server**. The application provides secure user authentication using **JWT**, role-based authorization, employee management, refresh token support, and RESTful API endpoints documented with Swagger.

---

## Project Overview

The Employee Leave Management System is designed to simplify employee management by providing secure CRUD operations through a RESTful Web API.

The project follows clean architecture principles using the **Repository Pattern**, **Unit of Work**, **Dependency Injection**, and **AutoMapper** to create a maintainable and scalable backend application.

---

## Features

### Authentication
- User Registration
- User Login
- JWT Access Token Authentication
- Refresh Token Rotation
- Secure Logout
- BCrypt Password Hashing
- Role-Based Authorization (Admin & Manager)

### Employee Management
- Create Employee
- View All Employees
- View Employee by ID
- Update Employee
- Delete Employee

### Additional Features
- Pagination
- Search
- Sorting
- Global Exception Handling
- Request Validation using FluentValidation
- Logging using Serilog
- Swagger API Documentation

---

## Technology Stack

### Backend
- ASP.NET Core 8 Web API
- C#

### Database
- SQL Server
- Entity Framework Core

### Security
- JWT Authentication
- Refresh Tokens
- BCrypt Password Hashing

### Design Patterns
- Repository Pattern
- Unit of Work
- Dependency Injection

### Libraries
- AutoMapper
- FluentValidation
- Serilog
- Swagger (OpenAPI)

### Tools
- Visual Studio Code
- SQL Server Management Studio
- Git
- GitHub
- Postman / Swagger

---

## Project Structure

```text
EmployeeLeaveManagementSystem
│
├── Controllers
├── Data
├── DTOs
├── Helpers
├── Interfaces
├── Mappings
├── Middleware
├── Models
├── Repositories
├── Services
├── Validators
├── Database
├── Architecture
├── Screenshots
├── PowerBI
│
├── Program.cs
├── appsettings.json
└── README.md
```

---

## Architecture

```text
Client (Swagger / Power Apps)
            │
            ▼
ASP.NET Core 8 Web API
            │
 Repository Pattern
            │
 Unit of Work
            │
 Entity Framework Core
            │
 SQL Server Database
```

---

## Database

The application uses SQL Server with Entity Framework Core.

Main tables include:

- Users
- Employees
- Departments
- RefreshTokens

---

## Authentication Flow

1. User logs in with username and password.
2. Password is verified using BCrypt.
3. JWT Access Token is generated.
4. Refresh Token is generated and stored in SQL Server.
5. Protected endpoints require a valid Bearer Token.
6. Refresh Token is used to obtain a new Access Token without logging in again.

---

## API Endpoints

### Authentication

| Method | Endpoint |
|---------|----------|
| POST | /api/Auth/register |
| POST | /api/Auth/login |
| POST | /api/Auth/refresh-token |
| POST | /api/Auth/logout |

### Employee

| Method | Endpoint |
|---------|----------|
| GET | /api/Employee |
| GET | /api/Employee/{id} |
| POST | /api/Employee |
| PUT | /api/Employee/{id} |
| DELETE | /api/Employee/{id} |

---

## Security

- JWT Authentication
- Refresh Token Rotation
- BCrypt Password Hashing
- Role-Based Authorization
- Secure API Endpoints

---

## Logging

The application uses **Serilog** for structured logging.

Logs include:

- User Login
- Registration
- API Requests
- Exceptions
- System Events

---

## API Documentation

Swagger UI is integrated for testing and documenting all REST API endpoints.

---

## Power BI Dashboard

Power BI will be integrated to visualize employee and leave management data.

Planned dashboards include:

- Employee Summary
- Department Analysis
- Salary Analysis
- Hiring Trends
- Employee Distribution

---

## Future Enhancements

- Leave Request Module
- Leave Approval Workflow
- Email Notifications
- Power Apps Integration
- Azure Deployment
- Docker Support
- CI/CD Pipeline using GitHub Actions

---

## Author

**Sunil Narayanareddy**

GitHub:
https://github.com/Sunil43-DA

LinkedIn:
(Add your LinkedIn profile link here)

---
