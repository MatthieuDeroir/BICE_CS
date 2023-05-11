using Xunit;
using Moq;
using System.Data.Common;
using System.Data.SqlClient;
using BICE.DAL;
using System.Data;
public class MaterialRepositoryTest
{
    private readonly Material_Repository _materialRepository;

    public MaterialRepositoryTest()
    {
        // Using actual implementations instead of mock.
        _materialRepository = new Material_Repository();
    }

    [Fact]
    public void GetById_ReturnsCorrectMaterial()
    {
        // Setup: create a material in the database that you will fetch in your test.
        // You would do this with a SQL script or through a repository class.
        // Let's assume this material has ID 1.
        
        // Act
        var material = _materialRepository.GetById(1);

        // Assert
        Assert.NotNull(material);
        Assert.Equal(1, material.Id);
        // Assert other properties of the material as necessary.

        // Teardown: remove the material you created for your test.
        // Again, you would do this with a SQL script or through a repository class.
    }
}
