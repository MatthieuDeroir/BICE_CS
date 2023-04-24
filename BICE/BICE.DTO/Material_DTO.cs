using System;
using System.Xml.Linq;

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
		
		
		
		
		
		
		
	}
}

