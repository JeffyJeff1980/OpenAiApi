# Building a .NET 8 Web API Endpoint with OpenAI for Natural Language Database Queries

View the article here: https://medium.com/@JeffyJeff/experimenting-with-openais-api-for-natural-language-database-queries-bd5dfd13dd63

## Introduction
This repository contains C# .NET code examples for integrating with a database using Entity Framework and performing various SQL operations. The code demonstrates how to build a Web API for querying a database using natural language processing with the OpenAI API, and other database operations like finding the most ordered products, the least popular product, and identifying top customers.

## Requirements
- .NET 8 SDK
- Microsoft SQL Server
- Entity Framework Core
- An OpenAI API key (for natural language processing parts)

## Installation
1. Clone the repository:
2. Navigate to the project directory:
3. Restore the project dependencies:


## Usage
To run the examples, navigate to the specific project directory and use the `dotnet run` command. Ensure you have set up your database and updated the connection strings in the `appsettings.json` file.

## How to Use the Code
1. **OpenAI Integration**: Replace `YOUR_API_KEY` with your actual OpenAI API key in the `appsettings.json` file.
2. **Database Connection**: Update the connection string in the `appsettings.json` file with your database credentials.
3. **Entity Framework**: Ensure that the Entity Framework is properly configured for your database.

## Contributing
Contributions to this project are welcome! Please fork the repository and submit a pull request with your features or fixes.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

---

For more detailed information about each part of the code, refer to the comments within each file. This project is intended as a starting point for integrating C# .NET applications with databases and using AI for natural language processing.

