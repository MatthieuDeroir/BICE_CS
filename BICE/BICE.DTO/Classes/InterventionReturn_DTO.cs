using BICE.DTO.Interfaces;

namespace BICE.DTO;

public class InterventionReturn_DTO : IInterventionReturn_DTO
{
    public List<string> UsedBarcodes { get; set; }
    public List<string> UnusedBarcodes { get; set; }
}