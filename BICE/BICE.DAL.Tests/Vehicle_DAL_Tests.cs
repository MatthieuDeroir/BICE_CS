using Xunit;
using BICE.DAL;
using BICE.DAL.Repositories;
using BICE.BLL;
using System.Linq;

namespace BICE.Tests.DALTests
{
    public class Vehicle_DAL_Tests
    {
        private readonly Vehicle_Repository _vehicleRepository;

        public Vehicle_DAL_Tests()
        {
            _vehicleRepository = new Vehicle_Repository(); 
        }

        [Fact]
        public void VehicleDAL_Insert_Success()
        {
            // Arrange
            var vehicleBLL = new Vehicle_BLL("A004", "Vehicle D", "AA-123-DD", true);
            var vehicleDAL = new Vehicle_DAL(vehicleBLL);

            // Act
            var insertedVehicle = _vehicleRepository.Insert(vehicleDAL);

            // Assert
            Assert.NotNull(insertedVehicle);
            Assert.Equal(vehicleBLL.InternalNumber, insertedVehicle.InternalNumber);
            Assert.Equal(vehicleBLL.Denomination, insertedVehicle.Denomination);
            Assert.Equal(vehicleBLL.LicensePlate, insertedVehicle.LicensePlate);
            Assert.Equal(vehicleBLL.IsActive, insertedVehicle.IsActive);
        }
    }
}