using BICE.BLL;

namespace BICE.Tests.BLLTests
{
    public class MaterialBLLTests
    {
        [Fact]
        public void MaterialBLL_CreateInstance_Success()
        {
            // Arrange
            string denomination = "Material A";
            string barcode = "BAR001";
            string category = "Category A";
            int usageCount = 0;
            int? maxUsageCount = 10;
            DateTime? expirationDate = DateTime.Now.AddMonths(1);
            DateTime? nextControlDate = DateTime.Now.AddDays(15);
            bool isStored = true;
            bool isLost = false;
            bool isRemoved = false;
            int? vehicleId = null;

            // Act
            var materialBLL = new Material_BLL(denomination, barcode, category, usageCount, maxUsageCount, expirationDate, nextControlDate, isStored, isLost, isRemoved, vehicleId);

            // Assert
            Assert.NotNull(materialBLL);
            Assert.Equal(denomination, materialBLL.Denomination);
            Assert.Equal(barcode, materialBLL.Barcode);
            Assert.Equal(category, materialBLL.Category);
            Assert.Equal(usageCount, materialBLL.UsageCount);
            Assert.Equal(maxUsageCount, materialBLL.MaxUsageCount);
            Assert.Equal(expirationDate, materialBLL.ExpirationDate);
            Assert.Equal(nextControlDate, materialBLL.NextControlDate);
            Assert.Equal(isStored, materialBLL.IsStored);
            Assert.Equal(isLost, materialBLL.IsLost);
            
            Assert.Equal(isRemoved, materialBLL.IsRemoved);
            
            Assert.Equal(vehicleId, materialBLL.VehicleId);
        }
        
        [Fact]
        public void MaterialBLL_UpdateUsageCount_IncrementsUsageCount()
        {
            // Arrange
            string denomination = "Material A";
            string barcode = "BAR001";
            string category = "Category A";
            int usageCount = 0;
            int? maxUsageCount = 10;
            DateTime? expirationDate = DateTime.Now.AddMonths(1);
            DateTime? nextControlDate = DateTime.Now.AddDays(15);
            bool isStored = true;
            bool isLost = false;
            bool isRemoved = false;
            int? vehicleId = null;

            var materialBLL = new Material_BLL(denomination, barcode, category, usageCount, maxUsageCount, expirationDate, nextControlDate, isStored, isLost, isRemoved, vehicleId);

            // Act
            materialBLL.UpdateUsageCount();

            // Assert
            Assert.Equal(1, materialBLL.UsageCount);
        }
    }
}