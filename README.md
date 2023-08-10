![TeamLegend logo](/logos/teamlegend-logo.png)
---
## What is Team Legend?
Team Legend is a Football Competitions Information System. 
The goal of the application is to present data for different leagues, teams, players, managers, stadiums and fixtures.

## How to use:
- You will need Visual Studio and .NET Core SDK.
- The Visual Studio IDE and the latest SDK can be downloaded from https://dot.net/core.

Also you can run the project in Visual Studio Code (Windows, Linux or MacOS).
To know more about how to setup your environment visit the [Microsoft .NET Download Guide](https://www.microsoft.com/net/download)

## Technologies used:
- ASP&#46;NET Core 2.1
- ASP&#46;NET MVC Core
- ASP&#46;NET Identity Core
- Entity Framework Core
- MS SQL
- AutoMapper
- Cloudinary

## Architecture:
- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Client-Server
- Database
- Layer separation
- Repository

## Description
Team Legend is an application that gives its users the opportunity to follow information about how their favorite football team's season is going. To use all the features, users have to register in the system so they can check all the data about the competitions, teams, players, matches, managers and stadiums. Authenticated users can edit their personal information. The first registered user in the system is automatically given the role of Administrator. Everyone with this role can add, update and delete data from the database.

## How to Install:
1. Clone this project running `git clone https://github.com/alexdimitrov2000/Team-Legend.git` in your terminal. If you haven't git installed can simply download it and unzip it.
2. Go to the `TeamLegend/` folder by running the command `cd TeamLegend/`.
3. Open the project in Visual Studio.
4. Open a terminal in the IDE by selecting `View > Terminal`.
5. Run the command `dotnet build` or press `Ctrl+Shift+B` to compile the project.
6. Run the command `dotnet run` or press `Ctrl+F5` to start serving the project.
7. Your default browser will automatically open the application on `https://localhost:5001/`.

## License
TeamLegend is an application licensed under the [MIT license](LICENSE.txt).