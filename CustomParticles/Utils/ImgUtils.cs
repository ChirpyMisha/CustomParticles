using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomParticles.Utils
{
	public static class ImgUtils
	{
		private static readonly string ParticleUnavailablePath = "CustomParticles.Resources.ParticleUnavailable.png";
		public static readonly string DefaultPath = $@"{UnityGame.UserDataPath}\CustomParticles\";
		public static string FullPath(string fileName) => $"{DefaultPath}{fileName}.png";
		public static bool ImgExists(string fileName) => File.Exists(FullPath(fileName));

		public static DateTime GetLastWriteTime(string fileName, string targetDirectory = "")
		{
			if (string.IsNullOrEmpty(targetDirectory))
				targetDirectory = DefaultPath;

			string filePath = targetDirectory + fileName;
			return File.GetLastWriteTimeUtc(filePath);
		}

		public static Texture2D LoadTexture(string fileName)
		{
			// Load custom particle texture
			byte[] textureData = File.ReadAllBytes(FullPath(fileName));
			Texture2D newTexture = new Texture2D(2, 2);
			ImageConversion.LoadImage(newTexture, textureData);
			return newTexture;
		}

		public static Texture2D LoadParticleUnavailableTexture()
		{
			// This doesn't properly work yet. But it results in a question mark instead of a cross which serves its purpose I guess xD
			var thisAssembly = Assembly.GetExecutingAssembly();
			using (var stream = thisAssembly.GetManifestResourceStream(ParticleUnavailablePath))
			{
				using (var reader = new StreamReader(stream))
				{
					string textureDataStr = reader.ReadToEnd();
					byte[] textureData = reader.CurrentEncoding.GetBytes(textureDataStr);
					Texture2D newTexture = new Texture2D(2, 2);
					ImageConversion.LoadImage(newTexture, textureData);
					return newTexture;
				}
			}
		}

		internal static bool IsValidFile(string fileName)
		{
			return File.Exists(FullPath(fileName));
		}

		public static List<Texture> LoadTexturesFromFolder(string targetDirectory = "")
		{
			if (string.IsNullOrEmpty(targetDirectory))
				targetDirectory = DefaultPath;

			// Process the list of files found in the directory.
			string[] fileEntries = Directory.GetFiles(targetDirectory);
			List<Texture> particleTextures = new List<Texture>();
			foreach (string filePath in fileEntries)
			{
				string fileName = Path.GetFileName(filePath);
				if (!fileName.EndsWith(".png"))
				{
					Plugin.Log.Warn($"ParticlesUtils, LoadParticlesFromFolder - Cannot load file \"{fileName}\". Reason: File type not supported.");
					continue;
				}

				fileName = fileName.Replace(".png", "");

				Texture newTexture = LoadTexture(fileName);
				newTexture.name = fileName;
				particleTextures.Add(newTexture);

			}

			return particleTextures;
		}
	}
}
