namespace BICE.DAL
{
    public interface IMaterial_DAL
    {
        /// <summary>
        /// Defines the data access layer representation of a material.
        /// </summary>
        int Id { get; set; }
        string Denomination { get; set; }
        string Barcode { get; set; }
        string Category { get; set; }
        int UsageCount { get; set; }
        int? MaxUsageCount { get; set; }
        DateTime? ExpirationDate { get; set; }
        DateTime? NextControlDate { get; set; }
        bool IsStored { get; set; }
        bool IsLost { get; set; }
        bool IsRemoved { get; set; }
        
        int? VehicleId { get; set; }
    }
}