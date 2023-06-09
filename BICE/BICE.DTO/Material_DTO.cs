﻿using System;
using System.Xml.Linq;
using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO
{
	public class Material_DTO : BaseNamedEntity_DTO
    {
	    
		public String Barcode { get; set; }
		public String Category { get; set; }
		public int UsageCount { get; set; }
		public int? MaxUsageCount { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public DateTime? NextControlDate { get; set; }
		public Boolean IsStored { get; set; }
		public Boolean IsLost { get; set; }
		public Boolean IsUsable { get; set; }

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
			IsUsable = materialBll.IsUsable;
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
			IsUsable = materialDal.IsUsable;
		}
		
		public Material_BLL ToBLL()
		{
			return new Material_BLL(Denomination, Barcode, Category, UsageCount, MaxUsageCount, ExpirationDate, NextControlDate, IsStored, IsLost, IsUsable);
		}
		
		
		
		
		
		
		
		
	}
}

