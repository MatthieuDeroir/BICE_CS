using BICE.DAL;
using BICE.DAL.Repositories;
using BICE.DTO;

namespace BICE.SRV.Tests;

public class MaterialServiceTests
{
    private Material_SRV _materialService;
    private Material_Repository _materialRepository;
    private Vehicle_Repository _vehicleRepository;
    private Intervention_Repository _interventionRepository;

    public MaterialServiceTests()
    {
        // Initialize the repositories and service with real instances
        _materialRepository = new Material_Repository();
        _vehicleRepository = new Vehicle_Repository();
        _interventionRepository = new Intervention_Repository();

        _materialService = new Material_SRV();
    }

    [Fact]
    public void HandleInterventionReturn_ShouldUpdateMaterials()
    {
        // Arrange
        int vehicleId = 1; // replace with an actual vehicle ID from your database
        InterventionReturn_DTO interventionReturnDto = new InterventionReturn_DTO()
        {
            // populate this DTO with actual data
            // Example:
            UsedBarcodes = new List<string> { "BC001"},
            UnusedBarcodes = new List<string> { "BC002" }
        };

        // Act
        var updatedMaterials = _materialService.HandleInterventionReturn(vehicleId, interventionReturnDto);

        // Assert
        Assert.NotNull(updatedMaterials);
        Assert.NotEmpty(updatedMaterials);
        Assert.Equal(2, updatedMaterials.Count());
        
        // This will depend on the actual data you used in your test
        // For example, you might want to check that the usage count was incremented for used materials
    }
}
