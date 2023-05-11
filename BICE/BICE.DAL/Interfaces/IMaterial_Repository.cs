namespace BICE.DAL.Repositories;

public interface IMaterial_Repository
{
    // GET
    public IEnumerable<Material_DAL> GetAll();
    
    public Material_DAL GetById(int id);

    public IEnumerable<Material_DAL> GetByVehicleId(int id);

    public IEnumerable<Material_DAL> GetMaterialsByVehicleId(int vehicleId);

    public Material_DAL GetByBarcode(string barcode);


    public IEnumerable<MaterialUsageHistory_DAL> GetMaterialUsageHistory(int materialId);

    public IEnumerable<Material_DAL> GetMaterialsByBarcodes(List<string> barcodes);
    // POST
    public Material_DAL Insert(Material_DAL material);

    public void AddUsageHistory(MaterialUsageHistory_DAL usageHistory);

    
    // PUT
    public Material_DAL Update(Material_DAL material);
    
    // DELETE
    public void Delete(Material_DAL material);
    
    
    
    

    

}