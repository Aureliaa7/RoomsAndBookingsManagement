using FluentAssertions;
using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Services.Commands;
using Moq;

namespace HRAAB_Management.Tests
{
    public sealed class AvailabilityCommandTests
    {
        private readonly Mock<IDataStore> dataStore;

        public AvailabilityCommandTests()
        {
            dataStore = new Mock<IDataStore>();
            SetupDataStore();
        }

        [Fact]
        public async Task Execute_WithValidData_ShoulReturnNumberOfRoomsAvailable()
        {
            // arrange
            AvailabilityCommandData commandData = new AvailabilityCommandData
            {
                HotelId = "Hotel 1",
                Arrival = new DateOnly(2025, 10, 1),
                Departure = new DateOnly(2025, 10, 5),
                RoomType = RoomTypeCode.Single
            };

            AvailabilityCommand availabilityCommand = new AvailabilityCommand(commandData, dataStore.Object);

            // act
            string result = await availabilityCommand.ExecuteAsync();

            //assert
            result.Should().NotBeNull();
            result.Should().Be("1");
        }

        [Fact]
        public async Task Execute_HotelWithNoRoomsAvailable_ShouldReturnZero()
        {
            AvailabilityCommandData commandData = new AvailabilityCommandData
            {
                HotelId = "Hotel 1",
                Arrival = new DateOnly(2025, 6, 1),
                Departure = new DateOnly(2025, 6, 3),
                RoomType = RoomTypeCode.Double
            };

            AvailabilityCommand availabilityCommand = new AvailabilityCommand(commandData, dataStore.Object);
            string result = await availabilityCommand.ExecuteAsync();

            result.Should().NotBeNull();
            result.Should().Be("0");
        }

        [Fact]
        public async Task Execute_UnexistingHotel_ShouldThrowException()
        {
            AvailabilityCommandData commandData = new AvailabilityCommandData
            {
                HotelId = "Hotel 1000",
                Arrival = new DateOnly(2025, 11, 1),
                Departure = new DateOnly(2025, 11, 5),
                RoomType = RoomTypeCode.Single
            };

            AvailabilityCommand availabilityCommand = new AvailabilityCommand(commandData, dataStore.Object);

            await Assert.ThrowsAnyAsync<Exception>(() => availabilityCommand.ExecuteAsync());
        }

        [Fact]
        public void Execute_WrongCommandDataType_ShouldThrowException()
        {
            RoomTypesCommandData commandData = new RoomTypesCommandData
            {
                HotelId = "Hotel 101",
                Arrival = new DateOnly(2025, 11, 1),
                Departure = new DateOnly(2025, 11, 5),
                NoPersons = 5
            };

            ArgumentException result = Assert.ThrowsAny<ArgumentException>(() => new AvailabilityCommand(commandData, dataStore.Object));
            Assert.Contains("Invalid command data type", result.Message);
        }

        private void SetupDataStore()
        {
            dataStore.Setup(x => x.GetHotels())
                .Returns(MockData.GetHotels());

            dataStore.Setup(x => x.GetBookings())
              .Returns(MockData.GetBookings());
        }
    }
}
