using System;
using System.ComponentModel.DataAnnotations;

namespace BICE.BLL
{
    public class Intervention_BLL
    {
        [Required(ErrorMessage = "Denomination is required !")]
        [StringLength(255, ErrorMessage = "Denomination length cannot exceed 255 characters !")]
        public string Denomination { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "StartDate is required !")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required !")]
        public DateTime EndDate { get; set; }

        public Intervention_BLL(string denomination, string? description, DateTime startDate, DateTime endDate)
        {
            Denomination = denomination;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
