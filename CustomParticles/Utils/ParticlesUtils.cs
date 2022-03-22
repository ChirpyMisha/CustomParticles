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

		public static void SetCustomParticles(ParticleSystem ps, PartSysID partSysID)
		{
			CustomParticle customParticle = CustomParticleConfigManager.Instance.GetCustomParticle(partSysID);
			if (customParticle == null)
			{
				Plugin.Log.Warn($"Something went wrong while setting custom particles for particle system \"{partSysID}\". CustomParticle cannot be found.");
				return;
			}
			if (!customParticle.IsValid)
			{
				Plugin.Log.Warn($"Something went wrong while setting custom particles for particle system \"{partSysID}\". CustomParticle.Texture has not been loaded properly.");
				return;
			}

			// set texture
			Material material = GetMaterial(ps);
			material.mainTexture = customParticle.Texture;

			// Enable random textures
			if (customParticle.Config.AnimationMode != AnimationMode.Off)
				EnableAndSetTextureSheetAnimation(ps, customParticle);
		}

		public static void LoadAndSetParticleImage(ParticleSystem ps, string fileName)
		{
			Material material = GetMaterial(ps);
			material.mainTexture = ImgUtils.LoadTexture(fileName);
		}

		public static void EnableAndSetTextureSheetAnimation(ParticleSystem partSys, CustomParticle customParticle)
		{
			CustomParticleConfig config = customParticle.Config;

			// Enable texture sheet animation module and set its parameters
			TextureSheetAnimationModule texSheetAnimation = partSys.textureSheetAnimation;
			texSheetAnimation.enabled = true;
			texSheetAnimation.mode = ParticleSystemAnimationMode.Grid;
			texSheetAnimation.numTilesX = config.ImgCountX;
			texSheetAnimation.numTilesY = config.ImgCountY;
			if (config.AnimationMode == AnimationMode.StaticRandom)
			{
				texSheetAnimation.timeMode = ParticleSystemAnimationTimeMode.Lifetime;
				texSheetAnimation.startFrame = new MinMaxCurve(0, config.ImgCountX * config.ImgCountY);
				texSheetAnimation.fps = 0;
				texSheetAnimation.cycleCount = 0;
			}
			else if (config.AnimationMode == AnimationMode.AnimatedFPS)
			{
				texSheetAnimation.timeMode = ParticleSystemAnimationTimeMode.FPS;
				texSheetAnimation.startFrame = new MinMaxCurve(0, 0);
				texSheetAnimation.fps = config.FPS;
			}
			else if (config.AnimationMode == AnimationMode.AnimatedLifetime)
			{
				texSheetAnimation.timeMode = ParticleSystemAnimationTimeMode.Lifetime;
				texSheetAnimation.startFrame = new MinMaxCurve(0, config.ImgCountX * config.ImgCountY);
				texSheetAnimation.cycleCount = 1;
			}
		}

		//public static void SaveTexture(ParticleSystem partSys, string fileName)
		//{
		//	Material material = GetMaterial(partSys);
		//	SaveTextureAsPNG((Texture2D)material.mainTexture, ImgUtils.DefaultPath + fileName);
		//}
		//public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
		//{
		//	byte[] _bytes = createReadabeTexture2D(_texture).EncodeToPNG();
		//	System.IO.File.WriteAllBytes(_fullPath, _bytes);
		//	Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
		//}
		//public static Texture2D createReadabeTexture2D(Texture2D texture2d)
		//{
		//	RenderTexture renderTexture = RenderTexture.GetTemporary(
		//				texture2d.width,
		//				texture2d.height,
		//				0,
		//				RenderTextureFormat.Default,
		//				RenderTextureReadWrite.Linear);
		//	Graphics.Blit(texture2d, renderTexture);
		//	RenderTexture previous = RenderTexture.active;
		//	RenderTexture.active = renderTexture;
		//	Texture2D readableTextur2D = new Texture2D(texture2d.width, texture2d.height);
		//	readableTextur2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		//	readableTextur2D.Apply();
		//	RenderTexture.active = previous;
		//	RenderTexture.ReleaseTemporary(renderTexture);
		//	return readableTextur2D;
		//}
	}
}
