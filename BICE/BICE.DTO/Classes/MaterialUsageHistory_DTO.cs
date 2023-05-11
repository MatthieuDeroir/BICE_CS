using BICE.DAL;
using BICE.DTO.Interfaces;

namespace BICE.DTO;

public class MaterialUsageHistory_DTO : BaseEntity_DTO, IMaterialUsageHistory_DTO
{
    public int MaterialId { get; set; }
    public int VehicleInterventionId { get; set; }
    public DateTime UsageDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsLost { get; set; }
    
    public MaterialUsageHistory_DTO(int id, int materialId, int vehicleInterventionId, DateTime usageDate, bool isUsed, bool isLost)
    {
        Id = id;
        MaterialId = materialId;
        VehicleInterventionId = vehicleInterventionId;
        UsageDate = usageDate;
        IsUsed = isUsed;
        IsLost = isLost;
    }
    
    public MaterialUsageHistory_DTO(int materialId, int vehicleInterventionId, DateTime usageDate, bool isUsed, bool isLost)
    {
        MaterialId = materialId;
        VehicleInterventionId = vehicleInterventionId;
        UsageDate = usageDate;
        IsUsed = isUsed;
        IsLost = isLost;
    }


    public MaterialUsageHistory_DTO(MaterialUsageHistory_DAL materialUsageHistoryDal)
    {
        Id = materialUsageHistoryDal.Id;
        MaterialId = materialUsageHistoryDal.MaterialId;
        VehicleInterventionId = materialUsageHistoryDal.VehicleInterventionId;
        UsageDate = materialUsageHistoryDal.UsageDate;
        IsUsed = materialUsageHistoryDal.IsUsed;
    }




}
