namespace BICE.DTO.Tests;

public class Vehicle_DTO_Tests
{
    [Fact]
    public void VehicleDtoConversionTest()
    {
        var vehicleDto = new Vehicle_DTO
        {
            Id = 1,
            Denomination = "TestVehicle",
            InternalNumber = "VN1234",
            LicensePlate = "LP1234",
            IsActive = true
        };

        var vehicleBll = vehicleDto.ToBLL();
        var vehicleDal = vehicleDto.ToDAL();

        Assert.Equal(vehicleDto.Id, vehicleBll.Id);
        Assert.Equal(vehicleDto.Id, vehicleDal.Id);
        Assert.Equal(vehicleDto.Denomination, vehicleBll.Denomination);
        Assert.Equal(vehicleDto.Denomination, vehicleDal.Denomination);
        Assert.Equal(vehicleDto.InternalNumber, vehicleBll.InternalNumber);
        Assert.Equal(vehicleDto.InternalNumber, vehicleDal.InternalNumber);
        Assert.Equal(vehicleDto.LicensePlate, vehicleBll.LicensePlate);
        Assert.Equal(vehicleDto.LicensePlate, vehicleDal.LicensePlate);
        Assert.Equal(vehicleDto.IsActive, vehicleBll.IsActive);
        Assert.Equal(vehicleDto.IsActive, vehicleDal.IsActive);
    }
}