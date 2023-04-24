using BICE.BLL;
using System;

namespace BICE.DAL
{
    public class Intervention_DAL
    {
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public Intervention_DAL(string denomination, string? description, DateTime startDate, DateTime endDate)
        {
            Denomination = denomination;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
        
        public Intervention_DAL(int id, string denomination, string? description, DateTime startDate, DateTime endDate)
            : this(denomination, description, startDate, endDate)
        {
            Id = id;
        }
        
        public Intervention_DAL(Intervention_BLL intervention)
            : this(intervention.Denomination, intervention.Description, intervention.StartDate, intervention.EndDate)
        {
        }
        
        public Intervention_DAL(int id, Intervention_BLL intervention)
            : this(id, intervention.Denomination, intervention.Description, intervention.StartDate, intervention.EndDate)
        {
        }
        
        public Intervention_BLL ToBLL()
        {
            return new Intervention_BLL(Denomination, Description, StartDate, EndDate);
        }
    }
}