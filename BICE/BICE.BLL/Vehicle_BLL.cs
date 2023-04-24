using System;
using System.ComponentModel.DataAnnotations;

namespace BICE.BLL
{
    public class Vehicle_BLL
    {
        [Required(ErrorMessage = "Denomination is required")]
        [StringLength(255, ErrorMessage = "Denomination cannot exceed 255 characters")]
        public string Denomination { get; private set; }

        [Required(ErrorMessage = "Internal number is required")]
        [StringLength(50, ErrorMessage = "Internal number cannot exceed 50 characters")]
        public string InternalNumber { get; private set; }

        [Required(ErrorMessage = "License plate is required")]
        [StringLength(20, ErrorMessage = "License plate cannot exceed 20 characters")]
        public string LicensePlate { get; private set; }

        public bool IsActive { get; private set; }

        public Vehicle_BLL(string internalNumber, string denomination,  string licensePlate, bool isActive)
        {
            Denomination = denomination;
            InternalNumber = internalNumber;
            LicensePlate = licensePlate;
            IsActive = isActive;
        }
    }
}
