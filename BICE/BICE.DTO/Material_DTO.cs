using System;
using System.Xml.Linq;

namespace BICE.DTO
{
	public class Material_DTO : BaseNamedEntity_DTO
    {
	    //TODO: Add IsLost property
	    //TODO: Add IsUsable property
		public String Barcode { get; set; }
		public String Category { get; set; }
		public int UsageCount { get; set; }
		public int? MaxUsageCount { get; set; }
		public DateTime? ExpirationDate { get; set; }
		public DateTime? NextControlDate { get; set; }
		public Boolean IsStored { get; set; }
		
	}
}

