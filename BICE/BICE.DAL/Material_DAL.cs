using System;
using BICE.BLL;

namespace BICE.DAL
{
    public class Material_DAL
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
        public bool IsUsable { get; set; }

        public Material_DAL(string denomination, string barcode, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored, bool isLost, bool isUsable)
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
            IsUsable = IsUsable;
        }

        public Material_DAL(int id, string denomination, string barcode, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored, bool isLost, bool isUsable)
            : this(denomination, barcode, category, usageCount, maxUsageCount, expirationDate, nextControlDate, isStored, isLost, isUsable)
        {
            Id = id;
        }
    }
}