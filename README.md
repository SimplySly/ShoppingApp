README
======

Prerequisites
-------------

- Visual Studio 2026
- MSSQLSERVER (tested on SQL Server 2022)
- Postman

Setup
-----

1. Clone the repository:

   git clone https://github.com/SimplySly/ShoppingApp

2. Open the solution in Visual Studio 2026 (ShoppingApp.slnx).

3. Configure user secrets for the Web project:

   - In Solution Explorer, right-click the ShoppingApp.Web project and choose "Manage User Secrets".
   - Add the following secrets (example format)

   {
	 "ConnectionStrings": {
	   "ShoppingAppDb": "Server=.;Database=ShoppingAppDb;Trusted_Connection=True;"
	 },
	 "Jwt": {
	   "SecretKey": "your_jwt_secret_here"
	 }
   }

   - appsettings.json already contains placeholders. See ShoppingApp.Web/appsettings.json

4. Launch the application using the HTTPS profile for the Web project (select the ShoppingApp.Web HTTPS profile in Visual Studio and run). Ensure the HTTPS port matches the Jwt:Issuer if you changed it.

Testing
-------

1. In Postman import the provided collection and environment files:

   - Postman testing/ShoppingApp.postman_collection.json
   - Postman testing/ShoppingApp.postman_environment.json

2. Load the environment in Postman and select it. Do NOT change the names of the environment variables; the collection depends on them.

3. The collection includes two "happy flow" sequences:

   - Creating a command as a customer user (this flow will create a test user/token and set environment variables used by later requests).
   - Products CRUD functionality (the requests update environment variables so the happy flows can be executed in the provided order without manual edits).

4. Run the requests in the order provided by the collection. The requests will set and update environment variables during the flows so they execute without manual modification.

