using System.Linq;
using Xunit;
using BICE.SRV;

namespace BICE.Tests
{
    public class Vehicle_SRV_Tests
    {
        private readonly Vehicle_SRV _vehicleService;

        public Vehicle_SRV_Tests()
        {
            _vehicleService = new Vehicle_SRV();
        }

        [Fact]
        public void GetVehicle_ReturnsAllVehicles()
        {
            var result = _vehicleService.GetVehicle();
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void GetVehicleById_ReturnsCorrectVehicle()
        {
            var allVehicles = _vehicleService.GetVehicle();
            var vehicleId = allVehicles.First().Id;

            var result = _vehicleService.GetVehicleById(vehicleId);
            Assert.Equal(vehicleId, result.Id);
        }

        // Implement similar tests for other methods
        // Remember to handle cleanup for methods that change the database (AddVehicle, Update, Delete)
    }
}