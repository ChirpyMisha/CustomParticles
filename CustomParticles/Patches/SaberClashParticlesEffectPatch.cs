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
	[HarmonyPatch(typeof(SaberClashEffect))]
	[HarmonyPatch("Start")]
	class SaberClashParticlesEffectPatch
	{
		internal static void Postfix(ref NoteCutParticlesEffect __instance)
		{
			ParticleSystem glowPS = Accessors.SaberClashGlowPS(ref __instance);
			ParticleSystem sparklePS = Accessors.SaberClashSparklePS(ref __instance);

			ParticlesUtils.SetCustomParticles(glowPS, Config.Instance.SaberClashGlowParticles);
			ParticlesUtils.SetCustomParticles(sparklePS, Config.Instance.SaberClashSparkleParticles);
		}
	}
}
