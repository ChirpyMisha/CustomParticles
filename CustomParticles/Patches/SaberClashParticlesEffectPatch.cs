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
		private static readonly string glowTexturePath = $@"SaberClashGlowParticles.png";
		private static readonly string sparkleTexturePath = $@"SaberClashSparkleParticles.png";

		internal static void Postfix(ref NoteCutParticlesEffect __instance)
		{
			ParticleSystem glowPS = Accessors.SaberClashGlowPS(ref __instance);
			ParticleSystem sparklePS = Accessors.SaberClashSparklePS(ref __instance);

			ParticlesUtils.SetCustomParticles(glowPS, glowTexturePath);
			ParticlesUtils.SetCustomParticles(sparklePS, sparkleTexturePath);
		}
	}
}
