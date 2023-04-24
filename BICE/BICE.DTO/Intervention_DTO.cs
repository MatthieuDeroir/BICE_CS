using System;
namespace BICE.DTO
{
	public class Intervention_DTO : BaseNamedEntity_DTO
	{
		public String? Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}

