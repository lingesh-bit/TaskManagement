# Task Management API

This is a simple Task Management REST API built using **ASP.NET Core 8** and **Entity Framework Core**. The project allows users to manage tasks, authenticate using JWT, and automatically marks overdue tasks as expired using a background service.

---

## Features

- JWT-based user authentication
- Create, Read, Update, and Delete (CRUD) operations for tasks
- Pagination, filtering, and sorting
- Input validation with custom validation attributes
- Automatic expiration of overdue tasks
- Serilog logging
- Swagger API documentation
- SQL Server with Entity Framework Core

---

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog
- Swagger / OpenAPI

---

## Getting Started

### Clone the repository

```bash
git clone https://github.com/<your-username>/TaskManagement.git
cd TaskManagement
```

### Configure the database

Update the connection string in **appsettings.json**.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TaskManagement;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Apply the database

```bash
dotnet ef database update
```

### Run the project

```bash
dotnet run
```

Open Swagger:

```
https://localhost:<port>/swagger
```

---

## Authentication

Login using the following endpoint:

```
POST /api/auth/login
```

Sample Request

```json
{
  "username": "admin",
  "password": "admin123!"
}
```

Copy the returned JWT token and use it to authorize protected endpoints in Swagger.

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/login` | Login and generate JWT |
| POST | `/api/tasks` | Create a new task |
| GET | `/api/tasks` | Get all tasks |
| GET | `/api/tasks/{id}` | Get a task by ID |
| PUT | `/api/tasks/{id}` | Update a task |
| DELETE | `/api/tasks/{id}` | Delete a task |

The **Get All Tasks** endpoint also supports:

- Pagination
- Status filtering
- Sorting by due date

---

## Background Service

A hosted background service runs at regular intervals and checks for overdue tasks.

If a task is still **Pending** and its due date has passed, the service automatically changes its status to **Expired**.

This keeps task statuses up to date without requiring any manual action.

---

## Validation

The API validates user input before processing requests.

Some of the validations include:

- Title is required.
- Title cannot exceed 200 characters.
- Due date is required.
- Due date cannot be in the past.

---

## Logging

Serilog is used to record important application events, including:

- Task creation
- Task updates
- Task deletion
- Background service execution

Logs are stored in the **logs** folder.

---

## Project Structure

```
Controllers
Services
Repositories
Models
DTOs
Interfaces
Data
BackgroundJobs
Middleware
```

---

## Future Improvements

Some enhancements that could be added in the future:

- Refresh Tokens
- Role-Based Authorization
- Unit Tests
- Integration Tests
- Docker Support
- CI/CD Pipeline

---

## Author

**Lingesh**

Software Developer
