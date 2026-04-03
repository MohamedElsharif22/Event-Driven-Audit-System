# Event-Driven Audit System

A .NET 10 event-driven application demonstrating automatic audit logging of enrollment activities using domain events, integration events, and background services.

## 📋 Quick Links
[Structure](#project-structure) | [Setup](#getting-started) | [Running](#running) | [API](#api-endpoints) | [Testing](#testing-with-scalar) | [Troubleshooting](#troubleshooting)
 
## 🎯 Key Features
- Event-driven architecture with automatic audit logging
- Asynchronous audit persistence via background worker
- Built-in Scalar API explorer
- Auto-migrations on startup
- Comprehensive structured logging

---

## 📂 Project Structure

```
DH.EventDrivenAuditSystem/
├── Domain/                    # Business entities & rules
│   ├── Common/               # BaseEntity, IRepository, IDomainEvent
│   ├── Entities/             # User, AuditLog
│   └── Courses/              # Course, Enrollment, CourseEnrolledEvent
├── Application/              # CQRS & business logic
│   ├── Features/             # Queries, Commands, Event handlers
│   ├── Behaviors/            # ValidationBehavior
│   └── DTOs/                 # Response objects
├── Infrastructure/           # Data access & background services
│   ├── Data/                 # DbContext, Configurations, Migrations
│   └── BackgroundServices/   # AuditChannel, AuditWorker
└── APIs/                     # REST endpoints & middleware
    ├── Controllers/          # Users, Courses, Enrollments, AuditLogs
    └── Middleware/           # Exception handling
```

**Tech Stack:** .NET 10 | C# 14 | SQLite | EF Core | MediatR | FluentValidation

---

## 🔄 How the Audit System Works

**Flow:**
1. POST request to create enrollment → 2. Command validated → 3. Enrollment entity created, Domain event raised
4. Transaction committed → 5. Integration event published → 6. Event handler writes to audit channel
7. Background worker persists audit log asynchronously

**Key Components:**
- **Domain Events**: Raised by entities (CourseEnrolledEvent) → used for domain consistency
- **Integration Events**: Published after commit (CourseEnrolledNotification) → triggers side effects
- **Audit Channel**: `System.Threading.Channels.Channel<T>` for producer-consumer pattern
- **AuditWorker**: Background service that persists events to database
- **AuditLog**: Captures UserId, Action, EntityName, EntityId, CreatedAt, Metadata

---

## 🚀 Getting Started

**Prerequisites:** .NET 10 SDK | Git | Visual Studio 2026 / VS Code / Rider

**Clone the Repository:**
```powershell
git clone https://github.com/MohamedElsharif22/Event-Driven-Audit-System.git
cd DH.EventDrivenAuditSystem
```

---

## ▶️ Running

**Option 1: Visual Studio**
- Open solution → Set `DH.EventDrivenAuditSystem.APIs` as startup project → Press F5

**Option 2: PowerShell**
```powershell
cd DH.EventDrivenAuditSystem.APIs
dotnet run
```

**Option 3: .NET CLI**
```powershell
dotnet run --project DH.EventDrivenAuditSystem.APIs/DH.EventDrivenAuditSystem.APIs.csproj
```

**Output:** Application available at `https://localhost:7075` | Database auto-migrates to `myDatabase.db`

---

## 🔌 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/users` | Get all users |
| GET | `/api/courses` | Get all courses |
| GET | `/api/enrollments` | Get all enrollments |
| POST | `/api/enrollments` | Create enrollment |
| GET | `/api/auditlogs` | Get audit logs |

**Create Enrollment Example:**
```json
POST /api/enrollments
{
  "userId": 1,
  "courseId": 1
}

// Success Response
{
  "data": "Enrollment created successfully.",
  "isSuccess": true
}
```

**Error Response:**
```json
{
  "data": null,
  "isSuccess": false,
  "errorMessage": "User already has an active enrollment in this course."
}
```

---

## 🧪 Testing with Scalar

**Access:** Navigate to `https://localhost:7075/scalar/v1`

**Workflow:**
1. GET `/api/users` → Copy a user ID
2. GET `/api/courses` → Copy a course ID
3. POST `/api/enrollments` with `{ "userId": 1, "courseId": 1 }`
4. GET `/api/enrollments` → Verify enrollment created
5. GET `/api/auditlogs` → Verify audit entry with "Enrolled" action

**Try duplicate enrollment:**
```json
POST /api/enrollments
{ "userId": 1, "courseId": 1 }
// Returns 400: "User already has an active enrollment in this course."
```

---

## 🗄️ Database & Migrations

**Auto Migration:** Runs on startup in `Program.cs` → Creates `myDatabase.db` (SQLite)

**Schema:**
- **Users** (Id, Name)
- **Courses** (Id, Title)
- **Enrollments** (Id, UserId, CourseId, EnrollmentDate, ExpirationDate - 30 days)
- **AuditLogs** (Id, UserId, Action, EntityName, EntityId, CreatedAt, Metadata)

**Connection String:** `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=myDatabase.db"
}
```

**Seed Data:** Migrations include sample Users and Courses for testing

---

## 📊 Logging

**Configuration** (`appsettings.json`):
```json
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}
```

**Key Events Logged:**
- Application startup
- Enrollment creation success/failure
- Domain events raised
- Audit log persistence
- Errors and validation failures

**View Logs:** Visual Studio Output window or console when running via `dotnet run`

---

## 🔍 Troubleshooting

| Issue | Solution |
|-------|----------|
| **Database not created** | Check file write permissions; verify connection string in `appsettings.json` |
| **Port already in use** | Change port in `launchSettings.json` or kill process: `netstat -ano \| findstr :5001` |
| **Enrollment creation fails** | Verify user/course exist via GET endpoints; check API error message |
| **Audit logs not appearing** | Ensure AuditWorker started (check logs); may take a moment due to async processing |
| **Migration errors** | Delete `myDatabase.db` and restart; auto-migration will recreate it |

---

## 📝 Quick Testing Scenario

```
1. dotnet run
2. Open https://localhost:7075/scalar/v1
3. GET /api/users → Copy userId
4. GET /api/courses → Copy courseId
5. POST /api/enrollments { "userId": 1, "courseId": 1 }
6. GET /api/enrollments → Confirm enrollment exists
7. GET /api/auditlogs → Verify "Enrolled" action logged
8. Try duplicate enrollment → Expect 400 error
9. Create enrollment with different course → Success
10. GET /api/enrollments → Confirm both enrollments exist
```

---

## ❓ FAQ

**Q: What is a Domain Event?**
A: Represents something that happened in the business domain (e.g., CourseEnrolledEvent). Raised by entities and stored until cleared, used for domain consistency.

**Q: What is an Integration Event?**
A: Published after transaction commit (e.g., CourseEnrolledNotification) to trigger side effects like audit logging, decoupling business logic from secondary concerns.

**Q: Why background service for audit logging?**
A: Asynchronous processing prevents audit failures from blocking enrollments, improving resilience via fire-and-forget pattern.

**Q: How to change database provider?**
A: Update connection string in `appsettings.json` and configure AppDbContext for your provider (SQL Server, PostgreSQL, etc.).

**Q: Can I extend the system?**
A: Yes - add new domain events, create event handlers, extend AuditLog properties, implement audit filtering/reporting.

---

## 👨‍💻 Author & Resources

**Created by:** Mohamed Elsharif  
**Repository:** [Event-Driven-Audit-System](https://github.com/MohamedElsharif22/Event-Driven-Audit-System)

**Learn More:**
- [.NET 10 Docs](https://learn.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [MediatR Pattern](https://github.com/jbogard/MediatR)
- [CQRS Pattern](https://www.microsoft.com/en-us/research/publication/cqrs-command-query-responsibility-segregation/)
- [Event-Driven Architecture](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/event-driven)

---

**Happy Coding! 🚀**
