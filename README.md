# DiscBud v1.0

## Overview
Disc golf, much like traditional golf, can be a challenging sport to master, especially for beginners. 
The game requires a variety of discs, each designed for specific wind conditions and distances. 
From putters and midrange discs to fairway drivers and distance drivers, each type has unique flight characteristics.
As players develop their skills, they often accumulate a large collection of discs, even finding that discs of the same type can behave differently. 
This abundance of options can make selecting the right disc a confusing task.
This app is here to simplify the selection process. Whether you're a novice or a seasoned player, our tool will help you choose the perfect disc for every shot. 
Say goodbye to the confusion and enjoy a more streamlined disc golf experience with our app.

This project was developed as part of an advanced postgraduate training course in Information Technology at Lexicon.

A big thank you to [Maged.H](https://github.com/MG777777) for guiding me through this project, helping me with databases, explaining, resolving issiues and making it possible for me to successfully complete the project.

## Features

- **Register**: Create user on the website.
- **Login**: Login to the website.
- **Add Discs**: Add Discs to the inventory and bag.
- **Remove Discs**: Remove Discs from both inventory and Bag.
- **Search**: Search for Discs by: Stats, Characteristics, Type, Stability + toggle filters by Disc type.
- **Details**: See Details about discs.

## TechStack
- EntityFrameworkCore
- SQL Server
- ASP.Net
- MVC
- C#
- jQuery
- Bootstrap
- HTML & CSS

## Installation

1. **Clone the Repository**
   ```sh
   git clone https://github.com/Htmil/DiscBudV1.git
2. **Install NuGet Packages**
  Install the required NuGet packages using the .NET CLI
  ```sh
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  ```
3. **Setup Database**
  Update the connection string in MyDbContext.cs to point to your database.

5. **Run the Application**
  ```sh
  dotnet run
  ```
