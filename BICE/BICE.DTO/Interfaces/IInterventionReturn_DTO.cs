namespace BICE.DTO.Interfaces
{
    public interface IInterventionReturn_DTO
    {
        List<string> UsedBarcodes { get; set; }
        List<string> UnusedBarcodes { get; set; }
    }
}