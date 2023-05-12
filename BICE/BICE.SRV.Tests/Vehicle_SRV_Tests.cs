using System.Linq;
using BICE.DAL;
using BICE.DTO;
using Xunit;
using BICE.SRV;
using Moq;

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
            // Arrange
            var mockRepository = new Mock<Vehicle_Repository>();
            var service = new Vehicle_SRV(mockRepository.Object);
            var vehicles = new List<Vehicle_DAL>
            {
                new Vehicle_DAL
                {
                    Id = 1,
                    Denomination = "Vehicle A",
                    InternalNumber = "A001",
                    LicensePlate = "AA-123-BB",
                    IsActive = true
                },
                new Vehicle_DAL
                {
                    Id = 1,
                    Denomination = "Vehicle A",
                    InternalNumber = "A001",
                    LicensePlate = "AA-123-BB",
                    IsActive = true
                }
            };
            mockRepository.Setup(repo => repo.GetAll()).Returns(vehicles);

            // Act
            var result = service.GetVehicle();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            // Additional assertions to verify the properties of the returned vehicles can also be done
        }

        [Fact]
        public void AddVehicle_InsertsVehicleAndReturnsInsertedVehicle()
        {
            // Arrange
            var mockRepository = new Mock<Vehicle_Repository>();
            var service = new Vehicle_SRV(mockRepository.Object);
            var vehicleDto = new Vehicle_DTO
            {
                Id = 1,
                Denomination = "Vehicle A",
                InternalNumber = "A001",
                LicensePlate = "AA-123-BB",
                IsActive = true
            };
            var vehicleDal = new Vehicle_DAL
            {
                Id = 1,
                Denomination = "Vehicle A",
                InternalNumber = "A001",
                LicensePlate = "AA-123-BB",
                IsActive = true
            };
            mockRepository.Setup(repo => repo.Insert(It.IsAny<Vehicle_DAL>())).Returns(vehicleDal);

            // Act
            var result = service.AddVehicle(vehicleDto);

            // Assert
            Assert.Equal(vehicleDal.Id, result.Id);
            // Additional assertions to verify the properties of the returned vehicle can also be done
        }

        [Fact]
        public void GetVehicleById_ReturnsVehicleWithMatchingId()
        {
            // Arrange
            var mockRepository = new Mock<Vehicle_Repository>();
            var service = new Vehicle_SRV(mockRepository.Object);
            var vehicleDal = new Vehicle_DAL
            {
                Id = 1,
                Denomination = "Vehicle A",
                InternalNumber = "A001",
                LicensePlate = "AA-123-BB",
                IsActive = true
            };
            mockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(vehicleDal);

            // Act
            var result = service.GetVehicleById(1);

            // Assert
            Assert.Equal(vehicleDal.Id, result.Id);
            // Additional assertions to verify the properties of the returned vehicle can also be done
        }
        
    }
}