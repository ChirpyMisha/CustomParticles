using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomParticles.Configuration
{
	//[Serializable]
	public class CustomParticleConfig
	{
		public string Name { get; set; }
		public AnimationMode AnimationMode { get; set; } = AnimationMode.Off;
		public int ImgCountX { get; set; } = 1;
		public int ImgCountY { get; set; } = 1;
		public int CycleCount { get; set; } = 0;
		public int FPS { get; set; } = 0;

		public CustomParticleConfig() 
		{
			Name = "placeholder";
		}
	}
}
