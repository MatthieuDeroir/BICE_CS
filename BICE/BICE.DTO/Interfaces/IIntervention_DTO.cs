using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO.Interfaces
{
    
    public interface IIntervention_DTO
    {
        String? Description { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
        Intervention_BLL ToBLL();
        Intervention_DAL ToDAL();
    }
}