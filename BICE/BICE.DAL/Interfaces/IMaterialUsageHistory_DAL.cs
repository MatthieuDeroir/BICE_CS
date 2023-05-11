namespace BICE.DAL
{
    public interface IMaterialUsageHistory_DAL
    {
        /// <summary>
        /// Defines the data access layer representation of a material usage history record.
        /// </summary>
        int Id { get; set; }
        int MaterialId { get; set; }
        int VehicleInterventionId { get; set; }
        DateTime UsageDate { get; set; }
        bool IsUsed { get; set; }
        bool IsLost { get; set; }
    }
}