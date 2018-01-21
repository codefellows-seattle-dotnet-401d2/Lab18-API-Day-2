# Task API

**Author**: Ariel R. Pedraza <br />
**Version**: 1.0.0

## Overview
<b>Purpose:</b><br />
This application is a MVC ASP.NET API to test Get, Post, Put and Delete routes. 

<b>How to use:</b><br />
Use the following routes to access the Tasks API: 
```
https://taskapi1.azurewebsites.net/
[HttpGet] api/Tasks
[HttpGet] api/Tasks/{id}
[HttpPost] api/Tasks
[HttpPut] api/Tasks/{id}
[HttpDelete] api/Tasks/{id}
```

## Getting Started
The following is required to run the program locally.
1. Visual Studio 2017 
2. The .NET desktop development workload enabled
3. Ensure appsettings.json connection string is set to:
```
"ConnectionStrings": {

    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskDB;Trusted_Connection=True;MultipleActiveResultSets=true"
```
4. Install Entity Framework, and build database with the following commands in the Package Manager Console:
```
Install-Package Microsoft.EntityFrameworkCore.Tools
Add-Migration Initial
Update-Database
```

## Architecture
This application is created using ASP.NET Core 2.0 Web applications. <br />
*Language*: C# <br />
*Type of Applicaiton*: API <br />