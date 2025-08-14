# Vehicle Rental Management

A small system for managing vehicle rental bookings

This system provides a REST API for booking management

Tech Stack:

- C# .NET 8
- PostgreSQL + Dapper

---

## How to Run the API

1. **Restore NuGet packages**  
   Run `dotnet restore` in the solution directory.

2. **Configure the database connection**  
   Edit `src\Production\VehicleRental.Web\appsettings.json` and set your PostgreSQL connection string under `ConnectionStrings:VehicleRentalConnectionString`.

3. **Create tables and populate initial data**

   - Run the SQL scripts in `src\SQL\create_tables.sql` in your database client to create the tables.
   - Run `src\SQL\insert_data.sql` to insert sample data.

   You can use a tool like PgAdmin execute these scripts.

4. **Run the API**

   - From the `src\Production\VehicleRental.Web` directory, run:
     ```
     dotnet run
     ```
   - The API will be available at `http://localhost:5047` (or as configured).

5. **API Documentation**
   - Swagger UI is available at `/swagger` when running in development mode.

---

## Dependencies

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- NuGet packages:
  - AutoMapper
  - Swashbuckle.AspNetCore
  - Dapper
  - Npgsql
  - Microsoft.Extensions.Configuration.Abstractions

---

## SQL Scripts

- **Table creation:**  
  See [`src\SQL\create_tables.sql`](src\SQL\create_tables.sql)
- **Sample data:**  
  See [`src\SQL\insert_data.sql`](src\SQL\insert_data.sql)

---

## Sample Requests & Responses

### Create a Booking

**Request**

```http
POST /api/v1/bookings
Content-Type: application/json

{
  "CustomerName": "John Doe",
  "CustomerPhoneNumber": "1234567890",
  "VehicleModel": "Toyota Corolla",
  "PickupDate": "2024-07-01T10:00:00",
  "ReturnDate": "2024-07-05T10:00:00",
  "DailyRate": 29.99
}
```

**Response**

```http
201 Created
"Booking created successfully. Booking ID: 12"
```

---

### Get All Bookings

**Request**

```http
GET /api/v1/bookings
Accept: application/json
```

**Response**

```http
200 OK
[
  {
    "bookingId": "1",
    "customerName": "John Doe",
    "customerPhoneNumber": "1234567890",
    "vehicleRegistrationNumber": "ABC123",
    "vehicleModel": "Toyota Corolla",
    "pickupDate": "2023-08-01T00:00:00",
    "returnDate": "2023-08-10T00:00:00",
    "dailyRate": 29.99,
    "bookingStatus": "Booked"
  }
  // ...more bookings
]
```

---

### Get Booking By Id

**Request**

```http
GET /api/v1/bookings/1
Accept: application/json
```

**Response (Success)**

```http
200 OK
{
  "bookingId": "1",
  "customerName": "John Doe",
  "customerPhoneNumber": "1234567890",
  "vehicleRegistrationNumber": "ABC123",
  "vehicleModel": "Toyota Corolla",
  "pickupDate": "2023-08-01T00:00:00",
  "returnDate": "2023-08-10T00:00:00",
  "dailyRate": 29.99,
  "bookingStatus": "Booked"
}
```

**Response (Not Found)**

```http
404 Not Found
```

**Response (Bad Request, e.g. invalid id)**

```http
400 Bad Request
{
  "id": [
    "Booking ID must be a numeric value"
  ]
}
```

---

### Cancel Booking

**Request**

```http
DELETE /api/v1/bookings/1
Accept: application/json
```

**Response (Success)**

```http
204 No Content
```

**Response (Bad Request, e.g. invalid id or unable to cancel)**

```http
400 Bad Request
"Unable to cancel booking"
```

**Response (Not Found)**

```http
404 Not Found
```

---

### Get All Vehicles

**Request**

```http
GET /api/v1/vehicle
Accept: application/json
```

**Response (Success)**

```http
200 OK
[
  {
    "id": 1,
    "model": "Toyota Corolla",
    "registrationNumber": "ABC123"
  },
  {
    "id": 2,
    "model": "Honda Civic",
    "registrationNumber": "XYZ456"
  }
  // ...more vehicles
]
```

**Response (Not Found)**

```http
404 Not Found
"No vehicles found."
```

---

### Get All Customers

**Request**

```http
GET /api/v1/customer
Accept: application/json
```

**Response (Success)**

```http
200 OK
[
  {
    "id": 1,
    "customerName": "John Doe",
    "customerPhoneNumber": "1234567890"
  },
  {
    "id": 2,
    "customerName": "Jane Smith",
    "customerPhoneNumber": "9876543210"
  }
  // ...more customers
]
```

**Response (Not Found)**

```http
404 Not Found
"No customers found."
```

---

### Get Location History for a Booking

**Request**

```http
GET /api/v1/bookings/1/location/history
Accept: application/json
```

**Response**

```http
200 OK
[
  {
    "latitude": 40.712776,
    "longitude": -74.005974
  },
  {
    "latitude": 40.713000,
    "longitude": -74.006000
  }
  // ...more location records
]
```

---

## Notes

- Ensure PostgreSQL is running and accessible.
- The API uses Dapper for data access.
- All configuration is in `appsettings.json`.
