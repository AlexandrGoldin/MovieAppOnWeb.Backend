 ## MovieApp.Backend
 ### The project is in development and not finished.
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
 
