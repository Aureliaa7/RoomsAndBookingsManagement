using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Services.Parsers;
using HRAAB_Management.Business.SettingsModels;
using Microsoft.Extensions.Options;
using Moq;

namespace HRAAB_Management.Tests
{
    public sealed class RoomTypesCommandParserTests
    {
        private readonly RoomTypesCommandParser parser;

        private readonly Mock<IOptions<RoomTypesCommandSettings>> commandSettingsMock;

        public RoomTypesCommandParserTests()
        {
            commandSettingsMock = new Mock<IOptions<RoomTypesCommandSettings>>();
            commandSettingsMock.Setup(x => x.Value).Returns(new RoomTypesCommandSettings
            {
                NoExpectedArguments = 3,
                CommandName = "RoomTypes",
                DateTimeFormat = "yyyyMMdd",
            });

            parser = new RoomTypesCommandParser(commandSettingsMock.Object);
        }

        [Fact]
        public void Parse_ValidCommand_ReturnsExpectedResult()
        {
            string command = "RoomTypes(H1, 20251001-20251002, 3)";

            ICommandData result = parser.Parse(command);

            var expectedResult = new RoomTypesCommandData
            {
                HotelId = "H1",
                Arrival = new DateOnly(2025, 10, 1),
                Departure = new DateOnly(2025, 10, 2),
                NoPersons = 3
            };

            Assert.NotNull(result);
            Assert.IsType<RoomTypesCommandData>(result);
            Assert.Equal(result.HotelId, expectedResult.HotelId);
            Assert.Equal(result.Arrival, expectedResult.Arrival);
            Assert.Equal(result.Departure, expectedResult.Departure);
            Assert.Equal(((RoomTypesCommandData)result).NoPersons, expectedResult.NoPersons);
        }

        [Fact]
        public void Parse_InvalidCommand_ShouldThrowException()
        {
            string command = "RoomTypes(H1, 20251001-20251002)";

            Exception result = Assert.ThrowsAny<ArgumentException>(() => parser.Parse(command));
            Assert.Contains("Invalid command format", result.Message);
        }
    }
}
