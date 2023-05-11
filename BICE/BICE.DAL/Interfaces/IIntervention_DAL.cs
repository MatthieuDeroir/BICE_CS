using BICE.BLL;
namespace BICE.DAL.Repositories;

public interface IIntervention_DAL
{
    /// <summary>
    /// Defines the data access layer representation of an intervention.
    /// </summary>
    int Id { get; set; }
    string Denomination { get; set; }
    string? Description { get; set; }
    DateTime StartDate { get; set; }
    DateTime? EndDate { get; set; }
        
    Intervention_BLL ToBLL();
}