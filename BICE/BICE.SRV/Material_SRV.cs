using System;
using System.Collections;
using BICE.DAL;
using BICE.DTO;
using BICE.BLL;

namespace BICE.SRV
{
	public class Material_SRV
	{
		private readonly Material_Repository _materialRepository;
		public Material_SRV()
		{
			_materialRepository = new Material_Repository();
		}
		
public IEnumerable<Material_DTO> GetMaterial()
		{
			IEnumerable<Material_DAL> materialDal = _materialRepository.GetAll();
			List<Material_DTO> materialDto = new List<Material_DTO>();
			foreach (Material_DAL material in materialDal)
			{
				materialDto.Add(new Material_DTO(material));
			}
			return materialDto;
		}

		public Material_DTO GetMaterialById(int id)
		{
			Material_DAL materialDal = _materialRepository.GetById(id);
			return new Material_DTO(materialDal);
		}

		public IEnumerable<Material_DTO> GetMaterialByVehicleId(int id)
		{
			IEnumerable<Material_DAL> materialDal = _materialRepository.GetByVehicleId(id);
			List<Material_DTO> materialDto = new List<Material_DTO>();
			foreach (Material_DAL material in materialDal)
			{
				materialDto.Add(new Material_DTO(material));
			}
			return materialDto;
		}
		
		public IEnumerable<Material_DTO> GetMaterialsByBarcodes(List<string> barcodes)
		{
			IEnumerable<Material_DAL> materialDals = _materialRepository.GetMaterialsByBarcodes(barcodes);
			List<Material_DTO> materialDto = new List<Material_DTO>();
			foreach (Material_DAL material in materialDals)
			{
				materialDto.Add(new Material_DTO(material));
			}
			return materialDto;
		}
		
		public Material_DTO AddMaterial(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			Material_DAL insertedMaterial = _materialRepository.Insert(materialDal);
			return new Material_DTO(insertedMaterial);
		}

		public IEnumerable<Material_DTO> AddMaterials(IEnumerable<Material_DTO> materialDtos)
		{
			List<Material_DTO> insertedMaterials = new List<Material_DTO>();
			foreach (Material_DTO materialDto in materialDtos)
			{
				Material_BLL materialBll = materialDto.ToBLL();
				Material_DAL materialDal = new Material_DAL(materialBll);
				Material_DAL insertedMaterial = _materialRepository.Insert(materialDal);
				insertedMaterials.Add(new Material_DTO(insertedMaterial));
			}
			return insertedMaterials;
		}
		
		public Material_DTO Update(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			Material_DAL updatedMaterial = _materialRepository.Update(materialDal);
			return new Material_DTO(updatedMaterial);
		}
		
		public void Delete(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			_materialRepository.Delete(materialDal);
		}

		public IEnumerable<Material_DTO> PrepareVehicle(int vehicleId, List<string> barcodes)
		{
			IEnumerable<Material_DAL> materialsToStore = _materialRepository.GetMaterialsByVehicleId(vehicleId);
			foreach (Material_DAL material in materialsToStore)
			{
				Material_DTO materialDto = new Material_DTO(material);
				Material_BLL materialBll = materialDto.ToBLL();
				materialBll.PutInStorage();
				Material_DAL materialDal = new Material_DAL(materialBll);
				_materialRepository.Update(materialDal);
			}
    
			IEnumerable<Material_DAL> materialsToPrepare = _materialRepository.GetMaterialsByBarcodes(barcodes);
			List<Material_DTO> materialDtos = new List<Material_DTO>();
			foreach (Material_DAL material in materialsToPrepare)
			{
				Material_DTO materialDto = new Material_DTO(material);
				Material_BLL materialBll = materialDto.ToBLL();
				materialBll.PutInVehicle(vehicleId);
				Material_DAL materialDal = new Material_DAL(materialBll);
				Material_DAL updatedMaterial = _materialRepository.Update(materialDal);
				materialDtos.Add(new Material_DTO(updatedMaterial));
			}
			
			return materialDtos;
		}
	}
}

