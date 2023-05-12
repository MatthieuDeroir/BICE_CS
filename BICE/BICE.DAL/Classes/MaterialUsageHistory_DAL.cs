namespace BICE.DAL;

public class MaterialUsageHistory_DAL
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public int VehicleInterventionId { get; set; }
    public DateTime UsageDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsLost { get; set; }

    public MaterialUsageHistory_DAL()
    {
    }
    
    public MaterialUsageHistory_DAL(int materialId, int vehicleInterventionId, DateTime usageDate, bool isUsed, bool isLost)
    {
        MaterialId = materialId;
        VehicleInterventionId = vehicleInterventionId;
        UsageDate = usageDate;
        IsUsed = isUsed;
        IsLost = isLost;
    }

    public MaterialUsageHistory_DAL(int id, int materialId, int vehicleInterventionId, DateTime usageDate, bool isUsed,
        bool isLost)
        : this(materialId, vehicleInterventionId, usageDate, isUsed, isLost)
    {
        Id = id;
    }
}