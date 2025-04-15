using HRAAB_Management.Business.Entities;
using HRAAB_Management.Business.Enums;

namespace HRAAB_Management.Tests
{
    internal static class MockData
    {
        public static IReadOnlyList<Hotel> GetHotels() => [
            new Hotel {
                Id = "Hotel 1",
                Name = "Hotel 1",
                Rooms = [
                    new Room {
                            RoomId = 1,
                            RoomType = RoomTypeCode.Single,
                    },
                    new Room {
                        RoomId = 2,
                        RoomType = RoomTypeCode.Double,
                    }],
                RoomTypes = [
                                new RoomType {
                    Code = RoomTypeCode.Single,
                    Size = 1,
                    Description = "Single room",
                    Amenities = ["WiFi", "TV"],
                    Features = ["Air conditioning"]
                    },
                    new RoomType {
                        Code = RoomTypeCode.Double,
                        Size = 2,
                        Description = "Double room",
                        Amenities = ["WiFi", "TV"],
                        Features = ["Air conditioning"]
                    }
                ]
            },
            new Hotel {
                Id = "Hotel 2",
                Name = "Hotel 2",
                Rooms = [
                    new Room {
                            RoomId = 1,
                            RoomType = RoomTypeCode.Single,
                    },
                    new Room {
                        RoomId = 2,
                        RoomType = RoomTypeCode.Double,
                    }],
                RoomTypes = [
                                new RoomType {
                    Code = RoomTypeCode.Single,
                    Size = 1,
                    Description = "Single room",
                    Amenities = ["WiFi", "TV"],
                    Features = ["Air conditioning", "Sea view", "Non-smoking"]
                    },
                    new RoomType {
                        Code = RoomTypeCode.Double,
                        Size = 2,
                        Description = "Double room",
                        Amenities = ["WiFi", "TV", "Minibar"],
                        Features = ["Air conditioning", "Non-smoking"]
                    }
                ]
            }
        ];

        public static IReadOnlyList<Booking> GetBookings() => [
            new Booking {
            HotelId = "Hotel 1",
            Arrival = new DateOnly(2025, 6, 1),
            Departure = new DateOnly(2025, 6, 5),
            RoomType = RoomTypeCode.Double,
            RoomRate = RoomRateType.Prepaid
        },
        new Booking {
            HotelId = "Hotel 1",
            Arrival = new DateOnly(2025, 7, 6),
            Departure = new DateOnly(2025, 7, 10),
            RoomType = RoomTypeCode.Single,
            RoomRate = RoomRateType.Standard
        },
        new Booking {
            HotelId = "Hotel 2",
            Arrival = new DateOnly(2025, 4, 17),
            Departure = new DateOnly(2025, 5, 05),
            RoomType = RoomTypeCode.Single,
            RoomRate = RoomRateType.Standard
        }];
    }
}
