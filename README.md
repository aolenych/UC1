# .NET WebAPI Application README

## Description

This .NET WebAPI application interfaces with a public API, facilitating country data retrieval in a JSON format. The app stands out with its user-centric design, incorporating features like filtering by names, setting a maximum population, and pagination. Such functionalities ensure users can efficiently obtain the specific data they require. Additionally, the built-in sorting mechanism streamlines data consumption, enabling users to get insights effectively.

## Running the Application Locally

1. Clone the repository:
git clone https://github.com/aolenych/UC1

2. Navigate to the application directory:
cd <application-directory>

3. Restore the NuGet packages:
dotnet restore

4. Build the application:
dotnet build


5. Run the application:
dotnet run

Upon starting, the application will be accessible via `http://localhost:<specified-port>`. The endpoint details and usage can be inspected via Swagger.

## Endpoint Usage Examples

Given that we have only one endpoint, the variations arise from the different query parameters that can be used.

1. Fetch all data without filters:
GET /api/Country

2. Filter data by name:
GET /api/Country?filter=st

3. Filter by a maximum population of 100 millions:
GET /api/Country?populationFilter=100

4. Sort data in ascending order:
GET //api/Country?sortOrder=ascend

5. Sort data in descending order:
GET /api/Country?order=descend

6. Filter data by name "Uk" with a maximum population of 50 millions and sort by descending order:
GET /api/Country?filter=Uk&populationFilter=50&sortOrder=descend

For a detailed overview of the API, navigate to the Swagger UI once the application is running.
