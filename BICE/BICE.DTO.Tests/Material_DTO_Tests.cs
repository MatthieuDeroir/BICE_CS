namespace BICE.DTO.Tests;


public class Material_DTO_Tests
{
    [Fact]
    public void MaterialDtoConversionTest()
    {
        var materialDto = new Material_DTO
        (
            1,
            "Test Material",
            "Test Category",
            "BC001",
            2,
            10,
            DateTime.Now + TimeSpan.FromDays(10),
            DateTime.Now,
            true,
            false,
            false,
            1
        );

        var materialBll = materialDto.ToBLL();
        var materialDal = materialDto.ToDAL();

        Assert.Equal(materialDto.Denomination, materialBll.Denomination);
        Assert.Equal(materialDto.Denomination, materialDal.Denomination);
        Assert.Equal(materialDto.Barcode, materialBll.Barcode);
        Assert.Equal(materialDto.Barcode, materialDal.Barcode);
        Assert.Equal(materialDto.Category, materialBll.Category);
        Assert.Equal(materialDto.Category, materialDal.Category);
        Assert.Equal(materialDto.UsageCount, materialBll.UsageCount);
        Assert.Equal(materialDto.UsageCount, materialDal.UsageCount);
        Assert.Equal(materialDto.MaxUsageCount, materialBll.MaxUsageCount);
        Assert.Equal(materialDto.MaxUsageCount, materialDal.MaxUsageCount);
        Assert.Equal(materialDto.ExpirationDate, materialBll.ExpirationDate);
        Assert.Equal(materialDto.ExpirationDate, materialDal.ExpirationDate);
        Assert.Equal(materialDto.NextControlDate, materialBll.NextControlDate);
        Assert.Equal(materialDto.NextControlDate, materialDal.NextControlDate);
        Assert.Equal(materialDto.IsStored, materialBll.IsStored);
        Assert.Equal(materialDto.IsStored, materialDal.IsStored);
        Assert.Equal(materialDto.IsLost, materialBll.IsLost);
        Assert.Equal(materialDto.IsLost, materialDal.IsLost);
        Assert.Equal(materialDto.IsRemoved, materialBll.IsRemoved);
        Assert.Equal(materialDto.IsRemoved, materialDal.IsRemoved);
        Assert.Equal(materialDto.VehicleId, materialBll.VehicleId);
        Assert.Equal(materialDto.VehicleId, materialDal.VehicleId);
    }

}