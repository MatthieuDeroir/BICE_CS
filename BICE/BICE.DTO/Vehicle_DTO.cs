using System;
using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO
{
    public class Vehicle_DTO : BaseNamedEntity_DTO
    {
        public String InternalNumber { get; set; }
        public String LicensePlate { get; set; }
        public Boolean IsActive { get; set; }

        public Vehicle_DTO()
        {
        }

        public Vehicle_DTO(Vehicle_BLL vehicle)
        {
            Denomination = vehicle.Denomination;
            InternalNumber = vehicle.InternalNumber;
            LicensePlate = vehicle.LicensePlate;
            IsActive = vehicle.IsActive;
        }

        public Vehicle_DTO(Vehicle_DAL vehicle)
        {
            Id = vehicle.Id;
            Denomination = vehicle.Denomination;
            InternalNumber = vehicle.InternalNumber;
            LicensePlate = vehicle.LicensePlate;
            IsActive = vehicle.IsActive;
        }

        public Vehicle_BLL ToBLL()
        {
            return new Vehicle_BLL(InternalNumber, Denomination, LicensePlate, IsActive);
        }

        public Vehicle_DAL ToDAL()
        {
            return new Vehicle_DAL(Id, Denomination, InternalNumber, LicensePlate, IsActive);
        }
    }
}