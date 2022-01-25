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
			ParticleSystem sparklePS = Accessors.ObstacleSaberSparklePS(ref __instance);

			ParticlesUtils.SetCustomParticles(sparklePS, PluginConfig.Instance.ObstacleSaberSparkleParticles);
		}
	}
}
