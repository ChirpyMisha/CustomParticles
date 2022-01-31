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
			// If the texture exists, change the main texture of the particle system.
			if (ImgUtils.ImgExists(settings.fileName))
			{
				// Load and set texture
				LoadAndSetParticleImage(ps, settings.fileName);

				// Enable random textures
				if (settings.imgCountX > 1 || settings.imgCountY > 1)
					EnableAndSetTextureSheetAnimation(ps, settings);
			}
			else
				Plugin.Log.Warn($"Failed to load image from: {ImgUtils.FullPath(settings.fileName)}");
		}

		public static void LoadAndSetParticleImage(ParticleSystem ps, string fileName)
		{
			Material material = GetMaterial(ps);
			material.mainTexture = ImgUtils.LoadTexture(fileName);
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
			if (settings.mode == ParticleSystemAnimationTimeMode.Lifetime)
			{
				texSheetAnimation.startFrame = new MinMaxCurve(0, settings.imgCountX*settings.imgCountY);
				texSheetAnimation.fps = 0;
				texSheetAnimation.cycleCount = 0;
			}
			else if (settings.mode == ParticleSystemAnimationTimeMode.FPS)
			{
				
				if (settings.fps == 0)
				{
					texSheetAnimation.timeMode = ParticleSystemAnimationTimeMode.Lifetime;
					texSheetAnimation.startFrame = new MinMaxCurve(0, settings.imgCountX * settings.imgCountY);
					texSheetAnimation.cycleCount = 1;
				}
				else
				{
					texSheetAnimation.startFrame = new MinMaxCurve(0, 0);
					texSheetAnimation.fps = settings.fps;
				}
			}
			// Show your accuracy as a percentage without drops from misses or a lowered combo multiplier. (To use the counter in-game requires Counters+ to be installed).
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
