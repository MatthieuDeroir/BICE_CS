using Xunit;
using BICE.BLL;

namespace BICE.Tests.BLLTests
{
    public class VehicleBLLTests
    {
        [Fact]
        public void VehicleBLL_CreateInstance_Success()
        {
            // Arrange
            int id = 1;
            string internalNumber = "A001";
            string denomination = "Vehicle A";
            string licensePlate = "AA-123-BB";
            bool isActive = true;

            // Act
            var vehicleBLL = new Vehicle_BLL(id, internalNumber, denomination, licensePlate, isActive);

            // Assert
            Assert.NotNull(vehicleBLL);
            Assert.Equal(id, vehicleBLL.Id);
            Assert.Equal(internalNumber, vehicleBLL.InternalNumber);
            Assert.Equal(denomination, vehicleBLL.Denomination);
            Assert.Equal(licensePlate, vehicleBLL.LicensePlate);
            Assert.Equal(isActive, vehicleBLL.IsActive);
        }
    }
}