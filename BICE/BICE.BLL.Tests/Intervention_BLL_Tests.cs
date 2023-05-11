using BICE.BLL;

namespace BICE.Tests.BLLTests
{
    public class InterventionBLLTests
    {
        [Fact]
        public void InterventionBLL_CreateInstance_Success()
        {
            // Arrange
            string denomination = "Intervention A";
            string description = "This is a test intervention";
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(2);

            // Act
            var interventionBLL = new Intervention_BLL(denomination, description, startDate, endDate);

            // Assert
            Assert.NotNull(interventionBLL);
            Assert.Equal(denomination, interventionBLL.Denomination);
            Assert.Equal(description, interventionBLL.Description);
            Assert.Equal(startDate, interventionBLL.StartDate);
            Assert.Equal(endDate, interventionBLL.EndDate);
        }
    }
}