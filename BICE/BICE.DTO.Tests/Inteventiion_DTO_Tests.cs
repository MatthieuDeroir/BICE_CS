namespace BICE.DTO.Tests;

public class Inteventiion_DTO_Tests
{
    [Fact]
    public void InterventionDtoConversionTest()
    {
        var interventionDto = new Intervention_DTO
        {
            Denomination = "TestIntervention",
            Description = "This is a test intervention",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now
        };

        var interventionBll = interventionDto.ToBLL();
        var interventionDal = interventionDto.ToDAL();

        Assert.Equal(interventionDto.Denomination, interventionBll.Denomination);
        Assert.Equal(interventionDto.Denomination, interventionDal.Denomination);
        Assert.Equal(interventionDto.Description, interventionBll.Description);
        Assert.Equal(interventionDto.Description, interventionDal.Description);
        Assert.Equal(interventionDto.StartDate, interventionBll.StartDate);
        Assert.Equal(interventionDto.StartDate, interventionDal.StartDate);
        Assert.Equal(interventionDto.EndDate, interventionBll.EndDate);
        Assert.Equal(interventionDto.EndDate, interventionDal.EndDate);
    }
}