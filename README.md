 ## MovieApp.Backend
 ### This application launches successfully but not all functionality is implemented(the project is in development and not finished).
 ### To run the MovieAppOnWeb SPA you need to run together with MovieApp.Fpontend.(https://github.com/AlexandrGoldin/MovieApp.Fpontend). 
 ### This is the final application developed according to the book "Architecting Modern Web Applications with ASP.NET Core and Azure". Updated to ASP.NET Core 8.0. Author: Steve “ardalis” Smith.
 ### Also used is the demo application eShopOnWeb from this book.
 ### You can also read the book in online pages at the .NET docs here: https://docs.microsoft.com/dotnet/architecture/modern-web-apps-azure/
 ### Developed in Visual Studio 2022.
 #### This is the backend of the app for the MovieAppOnWeb demo site.
 #### Uses Asp.Net Core 8 Minimal API/Controllers.
 ________
 ### This application launches successfully but not all functionality is implemented(the project is in development and not finished).
 ### At the moment, the functionality for the main page has been implemented. Authentication and authorization, sending and displaying a list of movies have been implemented. Search, sorting, filtering, pagination. Sending a list of genres and a list of countries. After authorization the method 'app.MapGet("/api/movies/{id}"...)' is available. and the method 'public async Task<IActionResult> Logout()'.
 ![main page](https://github.com/user-attachments/assets/bcb54aad-8377-4752-abbe-1923f147d6d3)
 __________
 ### To authorize use the method AuthEndpoints POST/api/authenticate. For User log in use "username": "demouser@microsoft.com", and"password": "Pass@word1".
 ### For admin log in use "username": "admin@microsoft.com", "password": "Pass@word1".
 ![AuthEndpoint](https://github.com/user-attachments/assets/6ea89c53-e75b-4df3-bca5-82a5902077cc)
 ____________
 ### Aviable authorizations
 ![Aviable authorizations](https://github.com/user-attachments/assets/153a2d59-bc18-493a-80c9-e2823b9fc434)
 ____________
 ### Method POST/api/authenticate return JWT token for authorization
 ![authenticate token](https://github.com/user-attachments/assets/8b7b7a64-2fb1-4686-ae05-9d279fd162d9)
 ____
 ### To access the method GET/api/movies/{id} use  "username": "demouser@microsoft.com", "password": "Pass@word1".
 ![Get api movies {id}](https://github.com/user-attachments/assets/7d822431-4add-4999-b70c-a31ebd630cfe)
 _______
 ### To access are methods POST, PUT, DELETE for movies use "username": "admin@microsoft.com", "password": "Pass@word1".
 ![POST PUT DELETE api movies](https://github.com/user-attachments/assets/4bccad50-30f7-41b0-b9eb-b8b62f0c6536)
 ______
 #### To run the SPA you need to run together with MovieApp.Frontend. https://github.com/AlexandrGoldin/MovieApp.Fpontend.
 #### When migrating a database, the MovieApp.Infrastructure project is specified in the Package Manager Console.
 #### The ApplicationContext : IdentityDbContext<ApplicationUser> is in the "MovieApp.Infrastructure" project.
 ___
### Stack:
* ASP.NET Core 8
* Minimal API and Controllers
* Authenticate end Authorization with JWT Bearer Token. The App uses packages Microsoft.AspNetCore.Authentication.JwtBearer, System.IdentityModel.Tokens.Jwt
* Ardalis.GuardClauses https://www.nuget.org/packages/Ardalis.GuardClauses
* Ardalis.Specification https://www.nuget.org/packages/Ardalis.Specification/
* Ardalis.Specification.EntityFrameworkCore https://www.nuget.org/packages/Ardalis.Specification.EntityFrameworkCore
* Ardalis.ApiEndpoints https://www.nuget.org/packages/Ardalis.ApiEndpoints/
* Global exception handler middleware
* CQRS MediatR
* FluentValidation
* Entity Framework Core 8
* MS SQL Server
* N-tier architecture
* The App implement a classApplicationContext : IdentityDbContext\<ApplicationUser> for interacting with database
* CORS
* Swashbuckle.AspNetCore.SwaggerUI
* Data Transfer Object
* AutoMapper
* Class PagedList for paging
___
 
