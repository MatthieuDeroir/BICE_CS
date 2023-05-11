namespace BICE.BLL.Interfaces
{
    public interface IMaterial_BLL
    {
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

        void UpdateUsageCount();
        void PutInStorage();
        void PutInVehicle(int vehicleId);
        void HasBeenLost();
        void HasBeenRemoved();
        void HasBeenRestored();
        void Validate();
    }
}

