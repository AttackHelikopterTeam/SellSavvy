# SellSavvy

## Project Description
This project is an API developed using Clean Architecture, incorporating various best practices and design patterns. It focuses on features like Identity Mechanism, JWT Token-based Authentication, CQRS Pattern with MediatR, Fluent Validation, Interception Mechanism, Entity Framework Core with Performance Optimization, Caching, and more.

## Added Features
- **Clean Architecture:** The project follows a clean and modular architecture for better maintainability and scalability.
- **Identity Mechanism and JWT Token Authentication:** Implements secure user authentication using JWT tokens.
- **CQRS Pattern with MediatR:** Utilizes the CQRS pattern for improved separation of concerns.
- **Fluent Validation:** Ensures robust input validation throughout the application.
- **Entity Framework Core and Performance Optimization:** Utilizes EF Core with tracing mechanisms, pagination, and performance optimization techniques.
- **Caching:** Implements caching for improved response times.
- **User Secrets:** Safely stores sensitive information using User Secrets.
- **Singleton Design Pattern:** Utilizes the Singleton design pattern for certain components.
- **Dependency Injection:** Incorporates dependency injection for all external libraries.
- **Sell Savvy Endpoints:**
  - **Authentication:**
    - [POST] `/api/Auth/Register`: User registration.
    - [POST] `/api/Auth/Login`: User login.
    - [GET] `/api/Auth/GetCountOfAccount`: Get the count of user accounts.
    - [GET] `/api/Auth/GetCountOfRequest`: Get the count of requests.

  - **Category:**
    - [GET] `/api/Category/All`: Get all categories.
    - [GET] `/api/Category/id:{Guid}`: Get a specific category by ID.
    - [POST] `/api/Category/Add`: Add a new category.
    - [PUT] `/api/Category/Update`: Update a category.

  - **Product:**
    - [GET] `/api/Product`: Get all products.
    - [GET] `/api/Product/{id}`: Get a specific product by ID.
    - [DELETE] `/api/Product/{id}`: Delete a product by ID.
    - [POST] `/api/Product/AddProduct`: Add a new product.
    - [PUT] `/api/Product/UpdateProduct`: Update a product.

## Detailed Features

### Clean Architecture
- The project is structured into separate layers (Presentation, Application, Domain, Infrastructure) following Clean Architecture principles.

### Identity Mechanism and JWT Token Authentication
- Implements user registration, login, and token generation.
- Uses JWT tokens for secure authentication.

### CQRS Pattern with MediatR
- Defines separate commands and queries using MediatR for better code organization.
- Implements handlers for each command/query.

### Fluent Validation
- Implements validators for input models and commands.

### Entity Framework Core and Performance Optimization
- Defines entities and configurations.
- Utilizes tracing mechanisms for database queries.
- Implements pagination for large datasets.
- Optimizes database queries for improved performance.

### Caching
- Implements caching for frequently requested data.

### User Secrets
- Safely stores sensitive information like connection strings using user secrets.

### Singleton Design Pattern
- Utilizes the Singleton design pattern for certain services.

### Dependency Injection
- Applies dependency injection for all external libraries.

## Task Distribution
- **Developer 1:** Implemented user authentication and JWT token generation.
- **Developer 2:** Implemented CQRS pattern with MediatR and fluent validation.
- **Developer 3:** Worked on Entity Framework Core and performance optimization.
- **Developer 4:** Implemented caching and user secrets.
- **Developer 5:** Added endpoints for Sell Savvy app - Authentication, Category, and Product.
- **Project Manager:** Oversaw project planning and coordination.

## Challenges Faced
During the development process, we encountered challenges in optimizing database queries for performance. We addressed this by implementing tracing mechanisms and pagination, resulting in improved query efficiency.

500 Intern Server Error: During the development process, we encountered a cancellation token problem, which was related to async orders. We couldn't figure out why it was causing an issue, so we decided to remove async completely. After doing so, it worked perfectly
