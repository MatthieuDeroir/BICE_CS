using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using BICE.BLL;
using BICE.DAL;


namespace BICE.DTO
{
	public class Material_DTO : BaseNamedEntity_DTO
    {
		public String Denomination { get; set; }   
		public String Barcode { get; set; }
		public String Category { get; set; }
		public int UsageCount { get; set; }
		public int? MaxUsageCount { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public DateTime? NextControlDate { get; set; }
		public Boolean IsStored { get; set; }
		public Boolean IsLost { get; set; }
		public Boolean IsRemoved { get; set; }
		public int? VehicleId { get; set; }

		//DTO when is created for the WPF to API tranfer! Initializing the values to avoid null exceptions
		[JsonConstructor]
		public Material_DTO(string barcode, string denomination, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate)
		{
			Barcode = barcode;
			Denomination = denomination;
			Category = category;
			UsageCount = usageCount;
			MaxUsageCount = maxUsageCount;
			ExpirationDate = expirationDate;
			NextControlDate = nextControlDate;
			IsStored = true;
			IsLost = false;
			IsRemoved = false;
			VehicleId = null;
		}
		
		public Material_DTO(string barcode, string denomination, string category, int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, bool isStored, bool isLost, bool isRemoved, int? vehicleId)
		{
			Barcode = barcode;
			Denomination = denomination;
			Category = category;
			UsageCount = usageCount;
			MaxUsageCount = maxUsageCount;
			ExpirationDate = expirationDate;
			NextControlDate = nextControlDate;
			IsStored = isStored;
			IsLost = isLost;
			IsRemoved = isRemoved;
			VehicleId = vehicleId;
		}

		public Material_DTO(Material_BLL materialBll)
		{
			Denomination = materialBll.Denomination;
			Barcode = materialBll.Barcode;
			Category = materialBll.Category;
			UsageCount = materialBll.UsageCount;
			MaxUsageCount = materialBll.MaxUsageCount;
			ExpirationDate = materialBll.ExpirationDate;
			NextControlDate = materialBll.NextControlDate;
			IsStored = materialBll.IsStored;
			IsLost = materialBll.IsLost;
			IsRemoved = materialBll.IsRemoved;
			VehicleId = materialBll.VehicleId;
		}

		public Material_DTO(Material_DAL materialDal)
		{
			Denomination = materialDal.Denomination;
			Barcode = materialDal.Barcode;
			Category = materialDal.Category;
			UsageCount = materialDal.UsageCount;
			MaxUsageCount = materialDal.MaxUsageCount;
			ExpirationDate = materialDal.ExpirationDate;
			NextControlDate = materialDal.NextControlDate;
			IsStored = materialDal.IsStored;
			IsLost = materialDal.IsLost;
			IsRemoved = materialDal.IsRemoved;
			VehicleId = materialDal.VehicleId;
		}
		
		public Material_BLL ToBLL()
		{
			return new Material_BLL(Denomination, Barcode, Category, UsageCount, MaxUsageCount, ExpirationDate, NextControlDate, IsStored, IsLost, IsRemoved, VehicleId);
		}

		public Material_DAL ToDAL()
		{
			return new Material_DAL(Denomination, Barcode, Category, UsageCount, MaxUsageCount, ExpirationDate, NextControlDate, IsStored, IsLost, IsRemoved, VehicleId);
		}
    }
}

