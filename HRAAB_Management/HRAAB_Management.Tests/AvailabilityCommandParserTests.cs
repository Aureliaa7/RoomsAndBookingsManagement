using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Services.Parsers;
using HRAAB_Management.Business.SettingsModels;
using Microsoft.Extensions.Options;
using Moq;

namespace HRAAB_Management.Tests
{
    public sealed class AvailabilityCommandParserTests
    {
        private readonly AvailabilityCommandParser parser;

        private readonly Mock<IOptions<AvailabilityCommandSettings>> commandSettingsMock;

        public AvailabilityCommandParserTests()
        {
            commandSettingsMock = new Mock<IOptions<AvailabilityCommandSettings>>();
            commandSettingsMock.Setup(x => x.Value).Returns(new AvailabilityCommandSettings
            {
                NoExpectedArguments = 3,
                CommandName = "Availability",
                DateTimeFormat = "yyyyMMdd",
            });

            parser = new AvailabilityCommandParser(commandSettingsMock.Object);
        }

        [Fact]
        public void Parse_ValidCommand_ReturnsExpectedResult()
        {
            // Arrange
            string command = "Availability(H1, 20251001-20251002, SGL)";

            ICommandData result = parser.Parse(command);

            var expectedResult = new AvailabilityCommandData
            {
                HotelId = "H1",
                Arrival = new DateOnly(2025, 10, 1),
                Departure = new DateOnly(2025, 10, 2),
                RoomType = RoomTypeCode.Single
            };

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AvailabilityCommandData>(result);
            Assert.Equal(result.HotelId, expectedResult.HotelId);
            Assert.Equal(result.Arrival, expectedResult.Arrival);
            Assert.Equal(result.Departure, expectedResult.Departure);
            Assert.Equal(((AvailabilityCommandData)result).RoomType, expectedResult.RoomType);
        }

        [Fact]
        public void Parse_InvalidCommand_ShouldThrowException()
        {
            string command = "Availability(20251001-20251002, SGL)";

            Exception result = Assert.ThrowsAny<ArgumentException>(() => parser.Parse(command));
            Assert.Contains("Invalid command format", result.Message);
        }
    }
}
