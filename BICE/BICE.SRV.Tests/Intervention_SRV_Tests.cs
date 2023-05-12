using Xunit;
using Moq;
using BICE.SRV;
using BICE.DAL;
using BICE.DTO;
using System.Collections.Generic;
using System.Linq;

public class InterventionSrvTests
{
    [Fact]
    public void GetIntervention_GetExpectedIntervention()
    {
        // Arrange
        var mockInterventionRepo = new Mock<Intervention_Repository>();
        
        var interventions = new List<Intervention_DAL>
        {
            new Intervention_DAL
            {
                Id = 1,
                Denomination = "Test1",
                Description = "",
                StartDate = DateTime.Today
            },
            new Intervention_DAL
            {
                Id = 2,
                Denomination = "Test2",
                Description = "",
                StartDate = DateTime.Today
            }
        };
        
        mockInterventionRepo.Setup(repo => repo.GetAll()).Returns(interventions);
        
        var srv = new Intervention_SRV(mockInterventionRepo.Object);
        
        // Act
        
        var result = srv.GetIntervention();
        
        // Assert
        
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Equal(1, resultList[0].Id);
        Assert.Equal("Test1", resultList[0].Denomination);
    }

    [Fact]
    public void AddIntervention_AddsNewIntervention()
    {
        // Arrange
        var mockInterventionRepo = new Mock<Intervention_Repository>();

        var intervention = new Intervention_DAL { Id = 3, Denomination = "Test3", Description = "", StartDate = DateTime.Today };

        mockInterventionRepo.Setup(repo => repo.Insert(It.IsAny<Intervention_DAL>())).Returns(intervention);

        var srv = new Intervention_SRV(mockInterventionRepo.Object);

        var newIntervention = new Intervention_DTO { Id = 3, Denomination = "Test3", Description = "", StartDate = DateTime.Today };

        // Act
        var result = srv.AddIntervention(newIntervention);

        // Assert
        Assert.Equal(3, result.Id);
        Assert.Equal("Test3", result.Denomination);
    }
    
    [Fact]
    public void UpdateIntervention_UpdatesIntervention()
    {
        // Arrange
        var mockInterventionRepo = new Mock<Intervention_Repository>();

        var intervention = new Intervention_DAL { Id = 3, Denomination = "Test3", Description = "", StartDate = DateTime.Today };

        mockInterventionRepo.Setup(repo => repo.Update(It.IsAny<Intervention_DAL>())).Returns(intervention);

        var srv = new Intervention_SRV(mockInterventionRepo.Object);

        var newIntervention = new Intervention_DTO { Id = 3, Denomination = "Test3", Description = "", StartDate = DateTime.Today };

        // Act
        var result = srv.Update(newIntervention);

        // Assert
        Assert.Equal(3, result.Id);
        Assert.Equal("Test3", result.Denomination);
    }
}