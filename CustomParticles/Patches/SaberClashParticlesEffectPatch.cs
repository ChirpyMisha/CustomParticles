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
			if (PluginConfig.Instance.IsEnabled(PartSysID.SaberClashGlow))
				ParticlesUtils.SetCustomParticles(Accessors.SaberClashGlowPS(ref __instance), PartSysID.SaberClashGlow);

			if (PluginConfig.Instance.IsEnabled(PartSysID.SaberClashSparkle))
				ParticlesUtils.SetCustomParticles(Accessors.SaberClashSparklePS(ref __instance), PartSysID.SaberClashSparkle);
		}
	}
}
