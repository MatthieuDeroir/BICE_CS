using Xunit;
using Moq;
using System.Collections.Generic;
using BICE.DAL;
using BICE.DAL.Repositories;
using BICE.DTO;
using BICE.SRV;

public class Material_SRV_Tests
{
    [Fact]
    public void GetMaterial_ReturnsAllMaterials()
    {
        // Arrange
        var mockMaterialRepository = new Mock<Material_Repository>();
        var mockVehicleRepository = new Mock<Vehicle_Repository>();
        var mockInterventionRepository = new Mock<Intervention_Repository>();
        var service = new Material_SRV(mockInterventionRepository.Object, mockMaterialRepository.Object,
            mockVehicleRepository.Object);
        var materials = new List<Material_DAL>
        {
            new Material_DAL
            {
                // Fill with your material properties
            },
            new Material_DAL
            {
                // Fill with your material properties
            }
        };
        mockMaterialRepository.Setup(repo => repo.GetAll()).Returns(materials);

        // Act
        var result = service.GetMaterial();

        // Assert
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        // Additional assertions to verify the properties of the returned materials can also be done
    }

    [Fact]
    public void GetMaterialById_ReturnsCorrectMaterial()
    {
        // Arrange
        var mockMaterialRepository = new Mock<Material_Repository>();
        var mockVehicleRepository = new Mock<Vehicle_Repository>();
        var mockInterventionRepository = new Mock<Intervention_Repository>();
        var service = new Material_SRV(mockInterventionRepository.Object, mockMaterialRepository.Object,
            mockVehicleRepository.Object);
        var material = new Material_DAL
        {
            Id = 1,
            Barcode = "123456789",
            Denomination = "Material A",
            Category = "Material A Description",
            IsLost = false,
            IsRemoved = false,
        };
        mockMaterialRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(material);

        // Act
        var result = service.GetMaterialById(1);

        // Assert
        Assert.NotNull(result);
        // Additional assertions to verify the properties of the returned material can also be done
    }

    [Fact]
    public void InsertMaterial_InsertsMaterialAndReturnsInsertedMaterial()
    {
        // Arrange
        var mockMaterialRepository = new Mock<Material_Repository>();
        var mockVehicleRepository = new Mock<Vehicle_Repository>();
        var mockInterventionRepository = new Mock<Intervention_Repository>();
        var service = new Material_SRV(mockInterventionRepository.Object, mockMaterialRepository.Object,
            mockVehicleRepository.Object);
        var material = new Material_DAL
        {
            Id = 1,
            Barcode = "123456789",
            Denomination = "Material A",
            Category = "Material A Description",
            IsLost = false,
            IsRemoved = false,
        };
        mockMaterialRepository.Setup(repo => repo.Insert(It.IsAny<Material_DAL>())).Returns(material);

        // Act
        var result = service.AddMaterial(new Material_DTO(material));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(material.Id, result.Id);
        Assert.Equal(material.Barcode, result.Barcode);
        Assert.Equal(material.Denomination, result.Denomination);
        Assert.Equal(material.Category, result.Category);
        Assert.Equal(material.IsLost, result.IsLost);
        Assert.Equal(material.IsRemoved, result.IsRemoved);
        Assert.Equal(material.VehicleId, result.VehicleId);
        // Additional assertions to verify the properties of the returned material can also be done
    }
// }

}