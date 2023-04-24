using System;
using BICE.BLL;

namespace BICE.DAL
{
    public class Vehicle_DAL
    {
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string InternalNumber { get; set; }
        public string LicensePlate { get; set; }
        public bool IsActive { get; set; }

        public Vehicle_DAL(string denomination, string internalNumber, string licensePlate, bool isActive)
        {
            Denomination = denomination;
            InternalNumber = internalNumber;
            LicensePlate = licensePlate;
            IsActive = isActive;
        }

        public Vehicle_DAL(int id, string denomination, string internalNumber, string licensePlate, bool isActive)
            : this(denomination, internalNumber, licensePlate, isActive)
        {
            Id = id;
        }

        public Vehicle_DAL(Vehicle_BLL vehicle)
            : this(vehicle.Denomination, vehicle.InternalNumber, vehicle.LicensePlate, vehicle.IsActive)
        {
        }

        public Vehicle_DAL(int id, Vehicle_BLL vehicle)
            : this(id, vehicle.Denomination, vehicle.InternalNumber, vehicle.LicensePlate, vehicle.IsActive)
        {
        }

        public Vehicle_BLL ToBLL()
        {
            return new Vehicle_BLL(Denomination, InternalNumber, LicensePlate, IsActive);
        }
    }

}