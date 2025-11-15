# Employee Management System (CRUD Application)

A simple and clean Employee Management Web Application built with ASP.NET Core MVC, ASP.NET Core Web API, and SQL Server, fully containerized using Docker Compose.
This project demonstrates how to build a beginner-friendly full-stack CRUD system with a minimal UI and clear architecture.


## Features

Add, view, update, and delete employee records

Minimal and clean UI using grey / white / black contrast

Backend API and frontend MVC separated into individual projects

SQL Server database container managed through Docker

Fully automated startup using Docker Compose

Entity Framework Core integrated for database operations

## Tech Stack

- ASP.NET Core MVC (Frontend)

- ASP.NET Core Web API (Backend)

- Entity Framework Core

- SQL Server (2022) running in Docker

- Docker + Docker Compose

- C# / .NET 9


## Project Structure

EmployeeSolution/
│
├── EmployeeApi/        # Backend Web API (CRUD logic)
├── EmployeeMvc/        # Frontend MVC application (UI)
├── docker-compose.yml  # Multi-container orchestration
└── README.md

## Running with Docker Compose
### Prerequisites
- Docker Desktop
- (Optional) Visual Studio 2022

### Run the application
From the project root:
docker compose up -d --build

## Access the apps
Frontend (MVC) -	http://localhost:8082
Backend (API) -	http://localhost:8081
SQL Server -	localhost,1433

## Docker Environment Variables (Simplified)

### Backend container:
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080
ConnectionStrings__DefaultConnection=Server=sqlserver;Database=EmployeeDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;

### Frontend container:
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:3000
BACKEND_URL=http://employeeapi:8080

## API Endpoints
GET /api/employees
GET /api/employees/{id}
POST /api/employees
PUT /api/employees/{id}
DELETE /api/employees/{id}


## UI Styling
Clean left-aligned layout
Minimal form design
Buttons styled using black, grey, and white theme
“Add New” button positioned inside the main container at the bottom-left

## How to Run Locally (without Docker)
- Install SQL Server or use SQL Server in Docker
- Update connection string in appsettings.Development.json
- Start EmployeeApi
- Start EmployeeMvc
