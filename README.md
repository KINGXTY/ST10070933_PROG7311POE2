# ST10070933_PROG7311POE2
Part 2 of Prog7311
LINK TO GITHUB REPOSITORY  "https://github.com/KINGXTY/ST10070933_PROG7311POE2"
Agri-Energy Connect is a web-based platform that connects sustainable agriculture with 
energy innovation. It allows farmers to manage their products and profiles, while employees 
can monitor and filter agricultural data to support planning and development.

SETUP PROCESS

1)Clone the Repository
  Run the following commands in your terminal:
  git clone "https://github.com/KINGXTY/ST10070933_PROG7311POE2"
  cd ST10070933PROG7311POE2
2)Open the Project in Visual Studio
  Open the ST10070933PROG7311.sln file using Visual Studio 2022 or later.
3)Verify the Database Connection
  The project uses a pre-attached LocalDB (.mdf) file located in the App_Data folder.
  Ensure your appsettings.json file includes the following connection string:
  "ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\mssqllocaldb;AttachDbFilename=|DataDirectory|\AgriDb.mdf;Trusted_Connection=True;"
  }
  Note: There is no need to run migrations. The database is already included.
4)Build and Run the Project
  In Visual Studio, press Ctrl+F5 to build and launch the application.

BUILDING AND RUNNING THE PROTOTYPE
 -Register a new user via the Register page.
 -Choose a role during registration: "Farmer" or "Employee".
 -After logging in, you will be redirected to a dashboard based on your role.
SYSTEM FUNCTIONALITIES
For Farmers
 -Automatically receive a Farmer profile after registration.
 -Add new products (e.g., crops, vegetables, fruits).
 -View all personal product entries.
For Employees
 -Add and manage farmer profiles (name, farm info, contact details).
 -View a full list of all registered farmers.
 -Filter products submitted by farmers using:
   -Category (e.g., "Fruit", "Grain")
   -Production Date Range
Security
 -Role-based access control using ASP.NET Core's [Authorize] attribute.
 -User roles are defined as "Farmer" and "Employee".
 -Anti-forgery tokens (@Html.AntiForgeryToken()) are used to protect all form submissions.
USER ROLES
"Farmer"
 -Add and view own products
 -Automatically linked to a farmer profile
"Employee"
 -Add and view all farmers
 -Filter and search all submitted products




