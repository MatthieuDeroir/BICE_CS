using System;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;


namespace BICE.BLL
{
	public class Material_BLL
	{
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
        
        public Boolean IsLost { get; set; }
        
        public Boolean IsRemoved { get; set; }
        
        public int? VehicleId { get; set; }

		public Material_BLL(String denomination, String barcode, String category,
            int usageCount, int? maxUsageCount, DateTime? expirationDate, DateTime? nextControlDate, Boolean isStored, Boolean isLost, Boolean isRemoved, int? vehicleId)
		{
            Denomination = denomination;
            Barcode = barcode;
            Category = category;
            UsageCount = usageCount;
            MaxUsageCount = maxUsageCount;
            ExpirationDate = expirationDate;
            NextControlDate = nextControlDate;
            IsStored = isStored;
            IsLost = isLost;
            IsRemoved = isRemoved;
            VehicleId = vehicleId;
            Validate();
        }
        
        public void UpdateUsageCount()
        {
            UsageCount++;
        }
        
        public void PutInStorage()
        {
            VehicleId = null;
            IsStored = true;
        }
        
        public void PutInVehicle(int vehicleId)
        {
            VehicleId = vehicleId;
            IsStored = false;
        }
        
        public void HasBeenLost()
        {
            IsLost = true;
            VehicleId = null;
            IsStored = false;
        }

        public void HasBeenRemoved()
        {
            IsRemoved = true;
            VehicleId = null;
        }
        
        public void HasBeenRestored()
        {
            IsRemoved = false;
        }

        public void Validate()
        {
            ValidateUsageCount();
            ValidateMaxUsageCount();
            ValidateDates();
            ValidateUsability();
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
        
        public void ValidateUsability()
        {
            if (UsageCount < MaxUsageCount && ExpirationDate > DateTime.Now && NextControlDate > DateTime.Now && IsStored == true)
            {
                IsRemoved = true;
                
            }
            else
            {
                IsRemoved = false;
            }
        }
    }
}

