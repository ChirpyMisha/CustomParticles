using CustomParticles.Utils;
using HarmonyLib;
using IPA.Utilities;
using System;
using System.IO;
using UnityEngine;

namespace CustomParticles
{
	[HarmonyPatch(typeof(NoteCutParticlesEffect))]
	[HarmonyPatch("Awake")]
	class NoteCutParticlesEffectPatch
	{
		internal static void Postfix(ref NoteCutParticlesEffect __instance)
		{
			ParticleSystem explosionPS = Accessors.ExplosionPS(ref __instance);
			ParticleSystem sparklesPS = Accessors.SparklesPS(ref __instance);
			//ParticleSystem corePS = Accessors.CorePS(ref __instance);

			ParticleSystemRenderer sparklesPSR = sparklesPS.gameObject.GetComponent<ParticleSystemRenderer>();
			ParticleSystemRenderer explosionPSR = explosionPS.gameObject.GetComponent<ParticleSystemRenderer>();
			if (sparklesPSR != null && explosionPSR != null)
			{
				// Check if custom particle texture exists
				string sparkleParticleTexPath = $@"{UnityGame.UserDataPath}\CustomParticleTextures\CustomParticle.png";
				string explosionParticleTexPath = $@"{UnityGame.UserDataPath}\CustomParticleTextures\CustomParticle2.png";
				if (File.Exists(sparkleParticleTexPath))
				{
					Texture2D sparkleParticleTex = LoadTexture(sparkleParticleTexPath);
					Texture2D explosionParticleTex = LoadTexture(explosionParticleTexPath);

					// Set new material
					Material sparklesPSRMat = sparklesPSR.material;
					Material explosionPSRMat = explosionPSR.material;
					sparklesPSRMat.mainTexture = sparkleParticleTex;
					explosionPSRMat.mainTexture = explosionParticleTex;
				}
				else
					Plugin.Log.Error("No file exists for particleTextPath");
			}
			else
				Plugin.Log.Error("sparklesPSR == null || explosionPSR == null");
		}

		private static Texture2D LoadTexture(string path)
		{
			// Load custom particle texture
			byte[] textureData = File.ReadAllBytes(path);
			Texture2D newTexture = new Texture2D(2, 2);
			ImageConversion.LoadImage(newTexture, textureData);
			return newTexture;
		}
	}
}
