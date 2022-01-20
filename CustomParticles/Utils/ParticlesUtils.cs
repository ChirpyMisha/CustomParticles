using CustomParticles.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace CustomParticles.Utils
{
	public static class ParticlesUtils
	{
		public static Texture GetMainTexture(ParticleSystem ps) => GetMaterial(ps).mainTexture;
		public static Material GetMaterial(ParticleSystem ps) => ps.gameObject.GetComponent<ParticleSystemRenderer>()?.material;

		public static void SetCustomParticles(ParticleSystem ps, ParticleSettings settings)
		{
			// Load and set texture
			LoadAndSetParticleImage(ps, settings.fileName);

			// Enable random textures
			if (settings.frameCount > 1)
				EnableAndSetTextureSheetAnimation(ps, settings);
		}

		public static void LoadAndSetParticleImage(ParticleSystem ps, string imgPath)
		{
			string fullPath = ImgUtils.DefaultPath + imgPath;

			// If the texture exists, change the main texture of the particle system.
			if (File.Exists(fullPath))
			{
				Material material = GetMaterial(ps);
				material.mainTexture = ImgUtils.LoadTexture(fullPath);
			}
			else
				Plugin.Log.Warn($"Failed to load image from: {fullPath}");
		}

		public static void EnableAndSetTextureSheetAnimation(ParticleSystem partSys, ParticleSettings settings)
		{
			Plugin.Log.Notice("Enabling texture sheet animation");
			// Enable texture sheet animation module and set its parameters
			TextureSheetAnimationModule texSheetAnimation = partSys.textureSheetAnimation;
			texSheetAnimation.enabled = true;
			texSheetAnimation.mode = ParticleSystemAnimationMode.Grid;
			texSheetAnimation.numTilesX = settings.imgCountX;
			texSheetAnimation.numTilesY = settings.imgCountY;
			texSheetAnimation.timeMode = settings.mode;
			int frameCount = settings.frameCount > 0 ? settings.frameCount : settings.imgCountX * settings.imgCountY;
			texSheetAnimation.startFrame = new MinMaxCurve(0, frameCount);
			texSheetAnimation.fps = settings.fps;
			texSheetAnimation.cycleCount = settings.cycleCount;
			// Show your accuracy as a percentage without drops from misses or a lowered combo multiplier. (To use the counter in-game requires Counters+ to be installed).
		}
	}
}
