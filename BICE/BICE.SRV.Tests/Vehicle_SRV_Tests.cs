using BICE.DAL;
using BICE.DTO;
using BICE.BLL;
using BICE.SRV;
using BICE.DAL.Interfaces;
using Moq;
using Xunit;

namespace BICE.Tests.SRVTests
{
    public class VehicleServiceTests
    {
        [Fact]
        public void AddVehicle_ValidVehicle_Success()
        {
            // Arrange
            var vehicleRepositoryMock = new Mock<IRepository_DAL>();
            var vehicleDto = new Vehicle_DTO
            {
                InternalNumber = "ABC123",
                Denomination = "Truck 1",
                LicensePlate = "AA-123-BB",
                IsActive = true
            };
            var vehicleDal = new Vehicle_DAL(new Vehicle_BLL(vehicleDto.InternalNumber, vehicleDto.Denomination, vehicleDto.LicensePlate, vehicleDto.IsActive));
            var addedVehicleDal = new Vehicle_DAL
            (
                1,
                "ABC123",
                "Truck 1",
                "AA-123-BB",
                true
            );
            vehicleRepositoryMock.Setup(repo => repo.Insert(vehicleDal)).Returns(addedVehicleDal);

            var vehicleService = new VehicleService(vehicleRepositoryMock.Object);

            // Act
            var result = vehicleService.AddVehicle(vehicleDto);

            // Assert
            Assert.Equal(addedVehicleDal.Id, result.Id);
            Assert.Equal(addedVehicleDal.InternalNumber, result.InternalNumber);
            Assert.Equal(addedVehicleDal.Denomination, result.Denomination);
            Assert.Equal(addedVehicleDal.LicensePlate, result.LicensePlate);
            Assert.Equal(addedVehicleDal.IsActive, result.IsActive);
        }
    }
}