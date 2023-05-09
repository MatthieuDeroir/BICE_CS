using BICE.DAL;
using BICE.DTO;
using System.Collections.Generic;
using BICE.BLL;

namespace BICE.SRV;

public class VehicleMaterial_SRV
{
    private readonly VehicleMaterial_Repository _vehicleMaterialRepository;
    
    public VehicleMaterial_SRV()
    {
        _vehicleMaterialRepository = new VehicleMaterial_Repository();
    }
    
    public IEnumerable<VehicleMaterial_DTO> GetVehicleMaterial()
    {
        IEnumerable<VehicleMaterial_DAL> vehicleMaterialDal = _vehicleMaterialRepository.GetAll();
        List<VehicleMaterial_DTO> vehicleMaterialDto = new List<VehicleMaterial_DTO>();
        foreach (VehicleMaterial_DAL vehicleMaterial in vehicleMaterialDal)
        {
            vehicleMaterialDto.Add(new VehicleMaterial_DTO(vehicleMaterial));
        }
        return vehicleMaterialDto;
    }

    public VehicleMaterial_DTO GetVehicleMaterialById(int id)
    {
        VehicleMaterial_DAL vehicleMaterialDal = _vehicleMaterialRepository.GetById(id);
        return new VehicleMaterial_DTO(vehicleMaterialDal);
    }



    public VehicleMaterial_DTO GetByVehicleId(int id)
    {
        VehicleMaterial_DAL vehicleMaterialDal = _vehicleMaterialRepository.GetByVehicleId(id);
        return new VehicleMaterial_DTO(vehicleMaterialDal);
    }
    
    public VehicleMaterial_DTO AddVehicleMaterial(VehicleMaterial_DTO vehicleMaterialDto)
    {
        VehicleMaterial_BLL vehicleMaterialBll = vehicleMaterialDto.ToBLL();
        VehicleMaterial_DAL vehicleMaterialDal = new VehicleMaterial_DAL(vehicleMaterialBll);
        VehicleMaterial_DAL insertedVehicleMaterial = _vehicleMaterialRepository.Insert(vehicleMaterialDal);
        return new VehicleMaterial_DTO(insertedVehicleMaterial);
    }
    
    public VehicleMaterial_DTO Update(VehicleMaterial_DTO vehicleMaterialDto)
    {
        VehicleMaterial_BLL vehicleMaterialBll = vehicleMaterialDto.ToBLL();
        VehicleMaterial_DAL vehicleMaterialDal = new VehicleMaterial_DAL(vehicleMaterialBll);
        VehicleMaterial_DAL updatedVehicleMaterial = _vehicleMaterialRepository.Update(vehicleMaterialDal);
        return new VehicleMaterial_DTO(updatedVehicleMaterial);
    }
    
    public void Delete(VehicleMaterial_DTO vehicleMaterialDto)
    {
        VehicleMaterial_DAL vehicleMaterialDal = vehicleMaterialDto.ToDAL();
        _vehicleMaterialRepository.Delete(vehicleMaterialDal);
    }
}