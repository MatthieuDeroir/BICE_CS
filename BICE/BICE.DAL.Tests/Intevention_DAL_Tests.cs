using Xunit;
using Moq;
using System.Data.Common;
using System.Data;
using System.Linq;
using BICE.DAL;
using System.Collections.Generic;
using System;
using BICE.DAL.Wrappers;

public class InterventionRepositoryTests
{
    private readonly Mock<IDbConnectionWrapper> _mockConnection;
    private readonly Mock<IDbCommandWrapper> _mockCommand;
    private readonly Mock<IDataReader> _mockReader;
    private readonly Intervention_Repository _repository;

    public InterventionRepositoryTests()
    {
        _mockConnection = new Mock<IDbConnectionWrapper>();
        _mockCommand = new Mock<IDbCommandWrapper>();
        _mockReader = new Mock<IDataReader>();
        _repository = new Intervention_Repository(_mockConnection.Object, _mockCommand.Object); // Pass Mock Connection and Command in constructor
    }

    [Fact]
    public void GetAll_ShouldReturnAllInterventions()
    {
        // Arrange
        var interventions = new Queue<Intervention_DAL>(new[]
        {
            new Intervention_DAL { Denomination = "Intervention 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now+TimeSpan.FromDays(1) },
            new Intervention_DAL { Denomination = "Intervention 2", Description = "Description 2", StartDate = DateTime.Now, EndDate = DateTime.Now+TimeSpan.FromDays(1)}
        });

        _mockReader.Setup(mock => mock.Read()).Returns(() => interventions.Count > 0);

        _mockReader.Setup(mock => mock["id"]).Returns(() => interventions.Peek().Id);
        _mockReader.Setup(mock => mock["denomination"]).Returns(() => interventions.Peek().Denomination);
        _mockReader.Setup(mock => mock["description"]).Returns(() => interventions.Peek().Description);
        _mockReader.Setup(mock => mock["startDate"]).Returns(() => interventions.Peek().StartDate);
        _mockReader.Setup(mock => mock["endDate"]).Returns(() => interventions.Peek().EndDate);

        _mockCommand.Setup(mock => mock.ExecuteReader()).Returns(_mockReader.Object);

        _mockConnection.Setup(mock => mock.CreateCommand()).Returns(_mockCommand.Object);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result.First().Id);
        Assert.Equal("Intervention 1", result.First().Denomination);
        Assert.Equal("Description 1", result.First().Description);
        Assert.Equal(interventions.First().StartDate, result.First().StartDate);
        Assert.Null(result.First().EndDate);
    }

}
