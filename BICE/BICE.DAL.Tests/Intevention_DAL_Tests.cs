using BICE.DAL;

namespace BICE.Tests.DALTests
{
    public class Intervention_DAL_Tests
    {
        private readonly Intervention_Repository _repository;

        [Fact]
        public void InterventionDAL_CreateInstance_Success()
        {
            // Arrange
            string denomination = "Intervention A";
            string description = "Description A";
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(7);

            // Act
            var interventionDAL = new Intervention_DAL(denomination, description, startDate, endDate);

            // Assert
            Assert.NotNull(interventionDAL);
            Assert.Equal(denomination, interventionDAL.Denomination);
            Assert.Equal(description, interventionDAL.Description);
            Assert.Equal(startDate, interventionDAL.StartDate);
            Assert.Equal(endDate, interventionDAL.EndDate);
        }
        [Fact]
        public void GetAll_ReturnsAllInterventions()
        {
            // Arrange
            string denomination = "Intervention AB";
            string description = "Description A";
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(7);

            // Act
            var interventionDAL = new Intervention_DAL(denomination, description, startDate, endDate);
            _repository.Insert(interventionDAL);
            var interventionsList = _repository.GetAll().ToList();

            // Assert
            Assert.NotEmpty(interventionsList);
        }
    }
}