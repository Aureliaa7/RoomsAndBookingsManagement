
# Hotel Room Availability And Bookings(HRAAB) Management App

This is a .NET 9 console application developed to manage hotel room availability and bookings using data from JSON files.
The app implements 2 commands:
1. Availability Command: 
 ```bash
Availability(hotelId, dateRange, roomTypeCode)
```

Example input: 
```bash
Availability(H101, 20240901, SGL) 
Availability(H101, 20240902-20240905, DBL)    
```

2. RoomTypes Command:
```bash
 RoomTypes(hotelId, dateRange, noPersons)
```

Example console input:  
```bash
RoomTypes(H101, 20240904, 3)  
RoomTypes(H101, 20240905-20240908, 4)  
```

## How to Run

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) *(Preview or Latest)*

- ### Build & Run

```bash
dotnet build
dotnet run  --hotels path\to\hotels.json --bookings path\to\bookings.json
