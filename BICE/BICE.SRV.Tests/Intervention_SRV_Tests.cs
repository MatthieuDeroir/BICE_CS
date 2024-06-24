using Xunit;
using BICE.SRV;
using BICE.DTO;
using System.Linq;

public class Intervention_SRV_Tests
{
    private readonly Intervention_SRV _interventionService;

    public Intervention_SRV_Tests()
    {
        _interventionService = new Intervention_SRV();
    }

    [Fact]
    public void GetAllInterventions_ReturnsNonEmptyList()
    {
        var result = _interventionService.GetIntervention();
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetInterventionById_ReturnsCorrectIntervention()
    {
        var allInterventions = _interventionService.GetIntervention();
        var interventionId = allInterventions.First().Id;

        var result = _interventionService.GetInterventionById(interventionId);
        Assert.Equal(interventionId, result.Id);
    }
    // Write more tests here for other methods
    // For methods that update the database (Insert, Update, Delete), 
    // you'll need to handle cleanup to maintain a consistent state for testing.
}