using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomParticles.Configuration
{
	public class ParticleSystemConfig
	{
		//public PartSysID PartSysID { get; private set; }

		public string CustomParticleName { get; set; }
		//internal void SetCustomParticleName(string newCustomParticleID) => CustomParticleName = newCustomParticleID;

		public bool IsEnabled { get; set; }
		//internal void SetEnabled(bool doEnable) => IsEnabled = doEnable;

		public ParticleSystemConfig()
		{
			CustomParticleName = "";
			IsEnabled = false;
		}

	}

	public enum PartSysID
	{
		Invalid,
		GlobalDust,
		NoteCutSparkle,
		NoteCutExplosion,
		SaberClashSparkle,
		SaberClashGlow,
		ObstacleSparkle
	}
}
