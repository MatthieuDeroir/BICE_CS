namespace BICE.DTO;

public class MaterialUsageHistoryDTO
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public int VehicleInterventionId { get; set; }
    public DateTime UsageDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsLost { get; set; }
}
