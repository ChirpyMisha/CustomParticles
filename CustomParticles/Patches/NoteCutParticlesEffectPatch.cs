using CustomParticles.Configuration;
using CustomParticles.Utils;
using HarmonyLib;
using IPA.Utilities;
using System;
using System.IO;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace CustomParticles.Patches
{
	[HarmonyPatch(typeof(NoteCutParticlesEffect))]
	[HarmonyPatch("Awake")]
	class NoteCutParticlesEffectPatch
	{
		internal static void Postfix(ref NoteCutParticlesEffect __instance)
		{
			ParticleSystem sparklePS = Accessors.NoteCutSparklePS(ref __instance);
			ParticleSystem explosionPS = Accessors.NoteCutExplosionPS(ref __instance);

			ParticlesUtils.SetCustomParticles(sparklePS, Config.Instance.NoteCutSparkleParticles);
			ParticlesUtils.SetCustomParticles(explosionPS, Config.Instance.NoteCutExplosionParticles);
		}
	}
}
