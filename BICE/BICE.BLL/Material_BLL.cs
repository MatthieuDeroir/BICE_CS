using System;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;


namespace BICE.BLL
{
	public class Material_BLL
	{
        //TODO: Add IsLost property
        //TODO: Add IsUsable property
        //TODO: Add IsUsable logic (usageCount < maxUsageCount && expirationDate > DateTime.Now && nextControlDate > DateTime.Now && isStored == true)
        [Required(ErrorMessage = "Denomination is required !")]
        [StringLength(255,ErrorMessage = "Denomination cannot exceed 255 characters !")]
        public String Denomination { get; set; }
        
        [Required(ErrorMessage = "Barcode is required !")]
        [StringLength(50, ErrorMessage = "Barcode cannot exceed 50 characters !")]
        public String Barcode { get; set; }

        [Required(ErrorMessage = "Category is required !")]
        [StringLength(255, ErrorMessage = "Category cannot exceed 255 characters !")]
        public String Category { get; set; }

        [Required(ErrorMessage = "UsageCount is required !")]
        public int UsageCount { get; set; }

        public int? MaxUsageCount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? NextControlDate { get; set; }

        [Required(ErrorMessage = "Denomination is required !")]
        public Boolean IsStored { get; set; }

		public Material_BLL(String denomination, String barcode, String category,
            int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, Boolean isStored)
		{
            Denomination = denomination;
            Barcode = barcode;
            Category = category;
            UsageCount = usageCount;
            MaxUsageCount = maxUsageCount;
            ExpirationDate = expirationDate;
            NextControlDate = nextControlDate;
            IsStored = isStored;
            ValidateDates();
            ValidateUsageCount();
            ValidateMaxUsageCount();
		}

        private void ValidateDates()
        {
            if (ExpirationDate.HasValue && NextControlDate.HasValue && ExpirationDate > NextControlDate)
            {
                throw new ArgumentException("Expiration date cannot be after next control date!");
            }
        }

        private void ValidateUsageCount()
        {
            if (UsageCount < 0)
            {
                throw new ArgumentException("Usage Count cannot be inferior to 0!");
            } else if (UsageCount > MaxUsageCount)
            {
                throw new ArgumentException("Usage Count cannot be superior to Max Usage Count!");
            }
        }

        private void ValidateMaxUsageCount()
        {
            if (MaxUsageCount.HasValue && MaxUsageCount < 1)
            {
                throw new ArgumentException("Max Usage Count cannot be equal or inferior to 0!");
            } 
        }
    }
}

