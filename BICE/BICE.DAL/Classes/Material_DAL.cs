using System;
using BICE.BLL;

namespace BICE.DAL
{
    public class Material_DAL : IMaterial_DAL
    {
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Barcode { get; set; }
        public string Category { get; set; }
        public int UsageCount { get; set; }
        public int? MaxUsageCount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? NextControlDate { get; set; }
        public bool IsStored { get; set; }
        public bool IsLost { get; set; }
        public bool IsRemoved { get; set; }
        
        public int? VehicleId { get; set; }
        
        public Material_DAL()
        {
        }

        public Material_DAL(string denomination, string barcode, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored, bool isLost, bool isRemoved, int? vehicleId)
        {
            Denomination = denomination;
            Barcode = barcode;
            Category = category;
            UsageCount = usageCount;
            MaxUsageCount = maxUsageCount;
            ExpirationDate = expirationDate;
            NextControlDate = nextControlDate;
            IsStored = isStored;
            IsLost = IsLost;
            IsRemoved = IsRemoved;
            VehicleId = vehicleId;
        }
        
        public Material_DAL(int id, string denomination, string barcode, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored, bool isLost, bool isRemoved, int? vehicleId)
            : this(denomination, barcode, category, usageCount, maxUsageCount, expirationDate, nextControlDate, isStored, isLost, isRemoved, vehicleId)
        {
            Id = id;
        }

        public Material_DAL(Material_BLL materialBll)
            : this(materialBll.Id, materialBll.Denomination, materialBll.Barcode, materialBll.Category, materialBll.UsageCount, materialBll.MaxUsageCount, materialBll.ExpirationDate, materialBll.NextControlDate, materialBll.IsStored, materialBll.IsLost, materialBll.IsRemoved, materialBll.VehicleId)
        {
            
        }
    }
}