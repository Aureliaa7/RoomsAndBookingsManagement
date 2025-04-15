using FluentAssertions;
using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Exceptions;
using HRAAB_Management.Business.Services.Commands;
using Moq;

namespace HRAAB_Management.Tests
{
    public sealed class RoomTypesCommandTests
    {
        private readonly Mock<IDataStore> dataStore;

        public RoomTypesCommandTests()
        {
            dataStore = new Mock<IDataStore>();
            SetupDataStore();
        }

        [Fact]
        public async Task Execute_WithValidData_ShoulReturnNumberOfRoomsAvailable()
        {
            RoomTypesCommandData commandData = new RoomTypesCommandData
            {
                HotelId = "Hotel 1",
                Arrival = new DateOnly(2025, 10, 1),
                Departure = new DateOnly(2025, 10, 5),
                NoPersons = 3
            };

            RoomTypesCommand roomTypesCommand = new RoomTypesCommand(commandData, dataStore.Object);

            string result = await roomTypesCommand.ExecuteAsync();
            string expectedResult = "Hotel 1: DBL, SGL";

            result.Should().NotBeNull();
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task Execute_UnexistingHotel_ShouldThrowException()
        {
            RoomTypesCommandData commandData = new RoomTypesCommandData
            {
                HotelId = "Hotel 101",
                Arrival = new DateOnly(2025, 11, 1),
                Departure = new DateOnly(2025, 11, 5),
                NoPersons = 1
            };

            RoomTypesCommand roomTypesCommand = new RoomTypesCommand(commandData, dataStore.Object);

            await Assert.ThrowsAnyAsync<Exception>(() => roomTypesCommand.ExecuteAsync());
        }

        [Fact]
        public void Execute_WrongCommandDataType_ShouldThrowException()
        {
            AvailabilityCommandData commandData = new AvailabilityCommandData
            {
                HotelId = "Hotel 101",
                Arrival = new DateOnly(2025, 11, 1),
                Departure = new DateOnly(2025, 11, 5),
                RoomType = RoomTypeCode.Single
            };

            ArgumentException result = Assert.ThrowsAny<ArgumentException>(() => new RoomTypesCommand(commandData, dataStore.Object));
            Assert.Contains("Invalid command data type", result.Message);
        }

        [Fact]
        public async Task Execute_WithNotEnoughRoomsAvailable_ShouldThrowException()
        {
            RoomTypesCommandData commandData = new RoomTypesCommandData
            {
                HotelId = "Hotel 1",
                Arrival = new DateOnly(2025, 6, 1),
                Departure = new DateOnly(2025, 6, 3),
                NoPersons = 6
            };

            RoomTypesCommand roomTypesCommand = new RoomTypesCommand(commandData, dataStore.Object);

            NotEnoughRoomsException result = await Assert.ThrowsAnyAsync<NotEnoughRoomsException>(() => roomTypesCommand.ExecuteAsync());
            Assert.Contains("Could not allocate enough rooms", result.Message);
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
