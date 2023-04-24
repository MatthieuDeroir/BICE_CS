using System;
using BICE.DAL;

namespace BICE.SRV
{
	public class Material_SRV
	{
		private readonly Material_Repository _materialRepository;
		public Material_SRV()
		{
			_materialRepository = new Material_Repository();
		}
		
		
	}
}

