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
			if (PluginConfig.Instance.IsEnabled(PartSysID.NoteCutSparkle))
				ParticlesUtils.SetCustomParticles(Accessors.NoteCutSparklePS(ref __instance), PartSysID.NoteCutSparkle);

			if (PluginConfig.Instance.IsEnabled(PartSysID.NoteCutExplosion))
				ParticlesUtils.SetCustomParticles(Accessors.NoteCutExplosionPS(ref __instance), PartSysID.NoteCutExplosion);
		}
	}
}
