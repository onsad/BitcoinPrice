# Bitcoin Price Tracker

Simple ASP.NET Core application for retrieving and storing Bitcoin price data.

Requirements:
- .NET 10
- EF Core 10
- SQL Server LocalDB
- EF Migrations
- Serilog
- Bootstrap

Application automatically creates and migrates database on startup.

## API Endpoints

### Get Live Bitcoin Price

Returns current Bitcoin price in EUR together with EUR/CZK exchange rate.

**Request**

GET /api/liveBtcPrice

**Response**

```
{
  "priceEur": 65342.15,
  "eurToCzkRate": 24.71
}
```

### Save Bitcoin Price Data

Saves one or more Bitcoin price records into database.

**Request**

POST /api/save
Content-Type: application/json

**Response**

```
[
  {
    "priceEur": 65342.15,
    "eurToCzkRate": 24.71
  },
  {
    "priceEur": 65410.22,
    "eurToCzkRate": 24.68
  }
]
```