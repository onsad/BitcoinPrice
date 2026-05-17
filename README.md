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

### MVC Endpoints

## Example cURL Requests

**Get Saved Prices Page**   

```
curl -X GET http://localhost:5000/savedPrices
```

**Save Changes**   

```
curl -X POST https://localhost:5001/BitcoinPrice/SaveChanges \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "SavedBitcoinPrice[0].Id=1" \
  -d "SavedBitcoinPrice[0].Selected=true" \
  -d "SavedBitcoinPrice[0].Note=Updated note" \
  -d "SavedBitcoinPrice[0].RowVersion=AAAAAAAAB9E="
```

**Delete Selected**

```
curl -X POST https://localhost:5001/BitcoinPrice/DeleteSelected \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "SavedBitcoinPrice[0].Id=1" \
  -d "SavedBitcoinPrice[0].Selected=true"
  ```