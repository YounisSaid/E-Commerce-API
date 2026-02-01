### 🛒 E-Commerce API Solution 🚀

A professional, enterprise-grade E-Commerce backend built with **ASP.NET Core 8** 💎, featuring a robust **Stripe** payment integration 💳, **Onion Architecture** 🧅, and **Redis** for high-performance distributed caching ⚡.

---

### 📋 Table of Contents

* [✨ Key Features](https://www.google.com/search?q=%23-key-features)
* [🏗️ Architecture Overview](https://www.google.com/search?q=%23%EF%B8%8F-architecture-overview)
* [🛠️ Technologies & Tools](https://www.google.com/search?q=%23%EF%B8%8F-technologies--tools)
* [📂 Project Structure](https://www.google.com/search?q=%23-project-structure)
* [⚡ Caching & Performance](https://www.google.com/search?q=%23-caching--performance)
* [💳 Stripe Payment Integration](https://www.google.com/search?q=%23-stripe-payment-integration)
* [🚦 Getting Started](https://www.google.com/search?q=%23-getting-started)
* [📜 License](https://www.google.com/search?q=%23-license)

---

### ✨ Key Features

* **🔐 Secure Payments**: Full **Stripe** integration supporting Payment Intents, secure client-side secrets, and automated webhook handling.
* **📦 Product Catalog**: Advanced product management with support for brands, types, and automated data seeding.
* **🧅 Onion Architecture**: Strict separation of concerns ensuring business logic is independent of external frameworks.
* **🛒 Redis-Backed Basket**: High-speed shopping basket persistence using Redis for optimal performance and scalability.
* **🔍 Specification Pattern**: Flexible querying, filtering, sorting, and pagination for product resources.
* **🆔 Identity & Security**: Secure authentication and authorization via **ASP.NET Identity** and **JWT Bearer Tokens**.
* **🛠️ Global Error Handling**: Custom middleware for centralized exception management and consistent API responses.

---

### 🏗️ Architecture Overview

The system is built using **Onion Architecture**, which places the Domain model at the core. 🎯

#### 1. Core (Domain) 🌳

Contains enterprise-wide logic, Entities (Product, Order, Basket), and Contracts (Interfaces). It has zero dependencies on other layers.

#### 2. Service (Application) ⚙️

Implements business rules, AutoMapper profiles, and the **Specification Pattern** to encapsulate complex query logic.

#### 3. Infrastructure (Persistence) 🗄️

Handles data access using **Entity Framework Core** and **Unit of Work**. This layer also manages **Redis** connections for caching and the basket.

#### 4. Presentation (Web API) 🌐

The entry point of the application, containing Controllers, custom Attributes (like `[Cache]`), and Middleware.

---

### 🛠️ Technologies & Tools

* **💳 Payments**: Stripe SDK & Webhooks
* **🚀 Framework**: ASP.NET Core 8.0
* **💾 ORM**: Entity Framework Core (SQL Server)
* **⚡ Caching**: Redis (StackExchange.Redis)
* **🔄 Mapping**: AutoMapper
* **🛡️ Security**: ASP.NET Core Identity & JWT
* **📑 Documentation**: Swagger 

---

### 📂 Project Structure

```text
├── E-commerce.Domain           # 🧩 Entities, Repository Interfaces, Domain Exceptions
├── E-Commerce.Serviece.Abs      # 📜 API Abstractions & Service Interfaces
├── E-Commerce.Service          # ⚙️ Business Logic, Mapping, Specification Logic
├── E-Commerce.Persistence      # 🗄️ SQL Server Context, Migrations, Repository Impls
├── E-Commerce.Shared           # 🤝 DTOs, Error Models, Common Utilities
└── E-Commerce-Web              # 🌐 Controllers, Middlewares, Dependency Injection

```

---

### ⚡ Caching & Performance

This project implements a sophisticated caching strategy using **Redis**:

#### 🚀 Response Caching

A custom `[Cache]` attribute is implemented to intercept `GET` requests. It generates a unique cache key based on the request path and query parameters, serving the response from Redis if available, which drastically reduces database load. 📉

#### 🛒 Basket Management

The shopping basket is stored as a distributed cache in Redis. This ensures that even if the API scales horizontally, the user's basket remains consistent and highly available across all instances. 🔄

---

### 💳 Stripe Payment Integration

The API provides a secure, end-to-end payment workflow:

1. **🎟️ Payment Intent**: When a user initiates checkout, the `PaymentService` creates a Stripe Payment Intent. This calculates the total (including shipping) on the server to prevent price tampering.
2. **🔑 Client Secret**: The API returns a `ClientSecret` to the client, allowing for secure, PCI-compliant card processing directly through Stripe's elements.
3. **🤖 Automated Webhooks**: The system includes a dedicated Webhook controller that listens for Stripe events:
* `payment_intent.succeeded` ✅: Automatically updates the order status to `Delivered/Paid`.
* `payment_intent.payment_failed` ❌: Marks the order as `Failed` for user notification.



---

### 🚦 Getting Started

#### 📋 Prerequisites

* **🖥️ [.NET 8.0 SDK**](https://www.google.com/search?q=https://dotnet.microsoft.com/download/dotnet/8.0)
* **🗃️ [SQL Server**](https://www.google.com/search?q=https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* **⚡ [Redis**](https://www.google.com/search?q=https://redis.io/download/)
* **💳 [Stripe Account**](https://www.google.com/search?q=https://dashboard.stripe.com/register) (for API keys)

#### 🛠️ Setup

1. **Clone the repository**:
```bash
git clone https://github.com/YounisSaid/E-Commerce-API.git

```


2. **Configure appsettings.json** 📝:
Update `ConnectionStrings`, `JwtOptions`, and `StripeOptions` (SecretKey and EndpointSecret) in the `E-Commerce-Web` project.
3. **Run Migrations** 🏗️:
```bash
dotnet ef database update --project E-Commerce.Persistence --startup-project E-Commerce-Web

```


4. **Run the Application** ▶️:
```bash
dotnet run --project E-Commerce-Web

```



#### 📖 API Documentation

Once running, visit the interactive Swagger UI at:
`https://localhost:7285/swagger/index.html` 🎨

---

### 📜 License

Distributed under the MIT License. See `LICENSE` for more information. 📄
