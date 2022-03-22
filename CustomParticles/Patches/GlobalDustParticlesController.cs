using CustomParticles.Configuration;
using CustomParticles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace CustomParticles.Patches
{
	// Not a Harmony patch, but I didn't know where else to put it ¯\_(ツ)_/¯
	static class GlobalDustParticlesController
	{
		public static void LoadCustomParticles()
		{
			// WARNING: This won't be called yet when the GlobalDustParticles get enabled in the settings!
			if (PluginConfig.Instance.IsEnabled(PartSysID.ObstacleSparkle))
			{
				foreach (var ps in Resources.FindObjectsOfTypeAll<ParticleSystem>())
					if (ps.name == "DustPS") SetCustomDustParticles(ps);
			}
		}

		private static void SetCustomDustParticles(ParticleSystem dustPS)
		{
			if (dustPS != null)
			{
				MainModule main = dustPS.main;
				main.startSize = new MinMaxCurve(PluginConfig.Instance.GlobalDustSizeMinimum, PluginConfig.Instance.GlobalDustSizeMaximum);
				ParticlesUtils.SetCustomParticles(dustPS, PartSysID.GlobalDust);
			}
			else
				Plugin.Log.Error("DustPS couldn't be found. Global particles cannot be changed.");
		}
	}
}
