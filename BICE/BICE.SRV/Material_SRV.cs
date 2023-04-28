using System;
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
		
		public Material_DTO AddMaterial(Material_DTO materialDto)
		{
			Material_BLL materialBll = materialDto.ToBLL();
			Material_DAL materialDal = new Material_DAL(materialBll);
			Material_DAL insertedMaterial = _materialRepository.Insert(materialDal);
			return new Material_DTO(insertedMaterial);
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
	}
}

