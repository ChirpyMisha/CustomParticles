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
		public static bool HasTextureAtlas(ParticleSystem ps) => ImgUtils.IsTextureAtlas(GetMainTexture(ps));
		public static int NumberOfSpritesX(ParticleSystem ps) => ImgUtils.NumberOfSpritesX(GetMainTexture(ps));
		public static int NumberOfSpritesY(ParticleSystem ps) => ImgUtils.NumberOfSpritesY(GetMainTexture(ps));
		public static Texture GetMainTexture(ParticleSystem ps) => GetMaterial(ps).mainTexture;
		public static Material GetMaterial(ParticleSystem ps) => ps.gameObject.GetComponent<ParticleSystemRenderer>()?.material;

		public static void SetCustomParticles(ParticleSystem ps, string imgPath)
		{
			// Load and set texture
			LoadAndSetParticleImage(ps, imgPath);

			// Enable random textures
			if (HasTextureAtlas(ps))
				EnableAndSetTextureSheetAnimation(ps);
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

		public static void EnableAndSetTextureSheetAnimation(ParticleSystem partSys)
		{
			Plugin.Log.Notice("Enabling texture sheet animation");
			// Enable texture sheet animation module and set its parameters
			TextureSheetAnimationModule texSheetAnimation = partSys.textureSheetAnimation;
			texSheetAnimation.enabled = true;
			texSheetAnimation.mode = ParticleSystemAnimationMode.Grid;
			texSheetAnimation.timeMode = ParticleSystemAnimationTimeMode.Lifetime;
			texSheetAnimation.numTilesX = NumberOfSpritesX(partSys);
			texSheetAnimation.numTilesY = NumberOfSpritesY(partSys);
			texSheetAnimation.cycleCount = 0;
			int frames = texSheetAnimation.numTilesX * texSheetAnimation.numTilesY;
			texSheetAnimation.startFrame = new MinMaxCurve(0, frames);
		}
	}
}
