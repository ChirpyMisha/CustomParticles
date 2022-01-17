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
		private static readonly string sparklesTexturePath = $@"NoteCutSparklesParticles.png";
		private static readonly string explosionTexturePath = $@"NoteCutExplosionParticles.png";

		internal static void Postfix(ref NoteCutParticlesEffect __instance)
		{
			ParticleSystem sparklesPS = Accessors.NoteCutSparklesPS(ref __instance);
			ParticleSystem explosionPS = Accessors.NoteCutExplosionPS(ref __instance);

			ParticlesUtils.SetCustomParticles(sparklesPS, sparklesTexturePath);
			ParticlesUtils.SetCustomParticles(explosionPS, explosionTexturePath);
		}
	}
}
