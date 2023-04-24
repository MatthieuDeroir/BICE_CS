using System;
using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO
{
	public class Intervention_DTO : BaseNamedEntity_DTO
	{
		public String? Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		
		public Intervention_DTO()
		{
		}
		
		public Intervention_DTO(Intervention_BLL intervention)
		{
			Denomination = intervention.Denomination;
			Description = intervention.Description;
			StartDate = intervention.StartDate;
			EndDate = intervention.EndDate;
		}
		
		public Intervention_DTO(Intervention_DAL intervention)
		{
			Id = intervention.Id;
			Denomination = intervention.Denomination;
			Description = intervention.Description;
			StartDate = intervention.StartDate;
			EndDate = intervention.EndDate;
		}
		
		public Intervention_BLL ToBLL()
		{
			return new Intervention_BLL(Denomination, Description, StartDate, EndDate);
		}
		
		public Intervention_DAL ToDAL()
		{
			return new Intervention_DAL(Id, Denomination, Description, StartDate, EndDate);
		}
		
		public override string ToString()
		{
			return Denomination;
		}
		
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (obj.GetType() != typeof(Intervention_DTO))
				return false;
			return ((Intervention_DTO)obj).Id == Id;
		}
	}
}

