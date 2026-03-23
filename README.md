# 📋 Notice Board Application (Test Task)

**Live Demo (Web UI):** [https://noticeboardweb-bge3a0fjd4bvhwg4.polandcentral-01.azurewebsites.net/](https://noticeboardweb-bge3a0fjd4bvhwg4.polandcentral-01.azurewebsites.net/)

## 🎯 Overview
This project is a scalable, cloud-ready Notice Board application developed as a technical assessment. It strictly follows the requirement of separating the backend RESTful API from the frontend application, ensuring a clear separation of concerns. 

The application allows authenticated users to create, update, and delete announcements, while guests can browse and filter the active listings.

## 🏗️ Architecture & Tech Stack

### Backend (NoticeBoard.Api, NoticeBoard.Core, NoticeBoard.Infrastructure)
* **Framework:** ASP.NET Core Web API (.NET 10)
* **Architecture:** Clean/N-Tier Architecture (Core, Infrastructure, API)
* **Data Access:** **Dapper** with the Repository Pattern. All CRUD operations are executed exclusively via **Stored Procedures** in MS SQL Server, ensuring high performance and compliance with requirements.
* **Authentication:** JWT Bearer tokens issued by Google Identity.

### Frontend (NoticeBoard.Web)
* **Framework:** ASP.NET Core MVC (.NET 10)
* **API Integration:** `IHttpClientFactory` (Typed Clients) to prevent socket exhaustion and manage HTTP request lifecycles.
* **Authentication:** OAuth 2.0 / OpenID Connect via Google Identity (Cookie-based session storing the token for API calls).
* **UI/UX:** Bootstrap 5, cascading dropdowns via vanilla JavaScript, client-side & server-side validation.

### Cloud & Deployment
* **Hosting:** Deployed as two independent Azure App Services on Linux (one for API, one for MVC).
* **Database:** Azure SQL Database.
* **Security:** All sensitive data (Connection Strings, OAuth Secrets, Base URLs) are managed securely via Azure Environment Variables, keeping the source code clean and secure.

## 🚀 How to Run Locally

1. **Database Setup:**
   * Execute the SQL scripts located in the `docs/sql-scripts/` folder on your local MS SQL Server to create the necessary tables and Stored Procedures.
2. **Configuration (`appsettings.json` & User Secrets):**
   * Update the `ConnectionStrings:DefaultConnection` in the API project.
   * Add your Google OAuth `ClientId` and `ClientSecret` to the User Secrets of both the `NoticeBoard.Api` and `NoticeBoard.Web` projects.
   * Ensure `ApiSettings:BaseUrl` in the Web project points to your local API instance (e.g., `https://localhost:7235/`).
3. **Run:**
   * Set **Multiple Startup Projects** in Visual Studio (both `NoticeBoard.Api` and `NoticeBoard.Web`).
   * Run the solution. The MVC app will automatically connect to the local API instance.
