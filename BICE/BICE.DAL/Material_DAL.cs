using System;
using BICE.BLL;

namespace BICE.DAL
{
    public class Material_DAL
    {
        //TODO: Add IsLost property
        //TODO: Add IsUsable property
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Barcode { get; set; }
        public string Category { get; set; }
        public int UsageCount { get; set; }
        public int? MaxUsageCount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? NextControlDate { get; set; }
        public bool IsStored { get; set; }

        public Material_DAL(string denomination, string barcode, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored)
        {
            Denomination = denomination;
            Barcode = barcode;
            Category = category;
            UsageCount = usageCount;
            MaxUsageCount = maxUsageCount;
            ExpirationDate = expirationDate;
            NextControlDate = nextControlDate;
            IsStored = isStored;
        }

        public Material_DAL(int id, string denomination, string barcode, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored)
            : this(denomination, barcode, category, usageCount, maxUsageCount, expirationDate, nextControlDate, isStored)
        {
            Id = id;
        }
    }
}