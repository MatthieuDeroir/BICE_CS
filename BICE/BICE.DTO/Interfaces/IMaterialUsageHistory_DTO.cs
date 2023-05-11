namespace BICE.DTO.Interfaces;

public interface IMaterialUsageHistory_DTO
{
    int Id { get; set; }
    int MaterialId { get; set; }
    int VehicleInterventionId { get; set; }
    DateTime UsageDate { get; set; }
    bool IsUsed { get; set; }
    bool IsLost { get; set; }
}