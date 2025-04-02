# Fraud Detection Rule Engine

This simple application is a rule-based fraud detection system built with C# and .NET. It reads bank transaction data from a CSV file, evaluates transactions using custom fraud rules (e.g., high relative transaction amount, excessive login attempts, and rapid IP address changes), and flags suspicious transactions.

## Technologies

- **.NET SDK:** .NET 8.0
- **Language:** C#
- **Libraries:**
  - [CsvHelper](https://github.com/JoshClose/CsvHelper) for CSV parsing
  - Microsoft.Extensions.DependencyInjection for dependency injection

## Setup Instructions

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/JustasDaugi/fraudulo.git
   cd fraudulo

2. Install .NET 8.0 SDK by opening this link and following the instructions:
   ```bash
    https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install?tabs=dotnet8&pivots=os-linux-ubuntu-2410

3. Build the Solution: From the app directory, run:
   ```bash
    dotnet build FraudDetectionSolution/FraudDetectionSolution.sln

4. Run the Application: Run the console project with:
   ```bash
    dotnet run --project src/FraudDetection.Console


## Project Structure

FraudDetection.Domain: Contains core business entities and repository interfaces.

FraudDetection.Application: Contains business logic and fraud evaluation utilities.

FraudDetection.Infrastructure: Contains implementations such as the CSV-based transaction repository.

FraudDetection.Console: The entry point of the application.