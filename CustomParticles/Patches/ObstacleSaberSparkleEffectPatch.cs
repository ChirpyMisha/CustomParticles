using CustomParticles.Configuration;
using CustomParticles.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomParticles.Patches
{
	[HarmonyPatch(typeof(ObstacleSaberSparkleEffect))]
	[HarmonyPatch("Awake")]
	class ObstacleSaberSparkleEffectPatch
	{
		internal static void Postfix(ref ObstacleSaberSparkleEffect __instance)
		{
			if (PluginConfig.Instance.IsEnabled(PartSysID.ObstacleSparkle))
				ParticlesUtils.SetCustomParticles(Accessors.ObstacleSaberSparklePS(ref __instance), PartSysID.ObstacleSparkle);
		}
	}
}
