# ST10070933_PROG7311POE2
Part 2 of Prog7311

Agri-Energy Connect is a web-based platform that connects sustainable agriculture with 
energy innovation. It allows farmers to manage their products and profiles, while employees 
can monitor and filter agricultural data to support planning and development.

SETUP PROCESS

1) Clone the Repository
   Run the following in your terminal
   [git clone "<repo-url>"]
   [cd AgriEnergyConnect]
   
2) Open the project
   
3) Configure the Database Connection
   Make sure your "appsettings.json" includes a connection string like this:
   "ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriDb;Trusted_Connection=True;" }

4) Apply Migrations 
   Open the Package Manager Console and run:
   "Update-Database"
   
5) Build and Run the Project


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
-Anti-forgery tokens (@Html.AntiForgeryToken()) protect all form submissions.

USER ROLE EXPLAINED 
"Farmer" - Add/View own Products
         -Automatically linked to a farmer profile

"Employee" - Add/view all farmers
           -Filter/search all submitted products



