FreakyFashion_EF_Core
Overview
FreakyFashion_EF_Core is a simple e-commerce web application focusing on a clothing store, developed using ASP.NET Core and Entity Framework Core. The project demonstrates basic CRUD operations, database handling, and MVC architecture for managing products and categories in the store.

Features
View list of clothing products with details (name, description, price, stock).

Add, update, and delete products.

Manage product categories.

Use of Entity Framework Core for data access and database migrations.

Simple and clean user interface based on ASP.NET Core MVC.

Basic validation on input forms.

Technologies Used
ASP.NET Core MVC

Entity Framework Core

C#

SQL Server / SQLite (depending on configuration)

Bootstrap for UI styling

Visual Studio 2022 / VS Code

Project Structure
pgsql
Kopiera
Redigera
FreakyFashion_EF_Core/
├── Controllers/
│   └── ProductsController.cs
├── Models/
│   ├── Product.cs
│   └── Category.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Views/
│   └── Products/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       └── Delete.cshtml
├── Migrations/
├── wwwroot/
├── appsettings.json
└── Program.cs / Startup.cs
How to Run
Clone the repository:

bash
Kopiera
Redigera
git clone https://github.com/KhattabAlshami90/FreakyFashion_EF_Core.git
Open the solution in Visual Studio.

Update your database connection string in appsettings.json.

Apply migrations to create the database:

powershell
Kopiera
Redigera
Update-Database
Run the project with Visual Studio or dotnet run.

Browse to the URL provided in the output (usually https://localhost:5001/).

Future Improvements
Add user authentication and authorization.

Implement a shopping cart and checkout process.

Enhance UI/UX design.

Add search and filtering options for products.

Include automated testing.

Author
Developed by Khattab Alshami as part of a learning and personal development project.

