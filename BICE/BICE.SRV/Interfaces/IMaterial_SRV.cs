namespace BICE.SRV.Interfaces;
using BICE.DTO;

public interface IMaterial_SRV
{
    //GET
    IEnumerable<Material_DTO> GetMaterial();
    
    Material_DTO GetMaterialById(int id);
    
    IEnumerable<Material_DTO> GetMaterialByVehicleId(int vehicleId);
    
    Material_DTO GetMaterialByBarcode(string barcode);
    
    //POST
    
    Material_DTO AddMaterial(Material_DTO materialDto);
    
    IEnumerable<Material_DTO> AddMaterials(IEnumerable<Material_DTO> materialDto);
    
    //PUT
    
    IEnumerable<Material_DTO> PrepareVehicle(int vehicleId, List<string> barcodes);

    Material_DTO Update(Material_DTO materialDto);
    
    //DELETE
    void Delete(Material_DTO materialDto);
    
    
    //OTHERS
    public IEnumerable<Material_DTO> HandleInterventionReturn(int interventionId, int vehicleId,
        InterventionReturn_DTO interventionReturnDto);



}