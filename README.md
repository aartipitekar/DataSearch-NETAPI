# DataSearch

DataSearch is a simple .NET application that allows searching for users based on a provided search term. It uses a dummy dataset loaded from a JSON file and provides a search API.

## Getting Started

1. Clone this repository to your local machine.
2. Open the `DataSearch` folder in your preferred code editor.
3. Make sure you have the .NET SDK installed on your machine.
4. Restore dependencies by running `dotnet restore` in the terminal.
5. Build the application using `dotnet build`.
6. Run the application with `dotnet run`.

## Usage

- The application exposes a simple API for searching users.
- Use the `/api/search` endpoint with the `searchTerm` query parameter to perform searches.

# DataSearch.Tests

DataSearch.Tests contains unit tests for the DataSearch application.

## Run Tests

1. Open the `DataSearch.Tests` folder.
2. Restore dependencies by running `dotnet restore` in the terminal.
3. Run tests using `dotnet test`.

## Test Cases

- `SearchCase1`: Tests searching for users with the search term "James".
- `SearchCase2`: Tests searching for users with the search term "jam".
- `SearchCase3`: Tests searching for a specific user "Katey Soltan".
- `SearchCase4`: Tests searching for a non-existing user "Jasmine Duncan".
- `SearchCase5`: Tests searching with an empty search term.



