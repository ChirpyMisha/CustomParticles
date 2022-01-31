using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomParticles.Utils
{
	public static class ImgUtils
	{
		public static readonly string DefaultPath = $@"{UnityGame.UserDataPath}\CustomParticles\";
		public static string FullPath(string fileName) => $"{DefaultPath}{fileName}.png";
		public static bool ImgExists(string fileName) => File.Exists(FullPath(fileName));

		public static Texture2D LoadTexture(string fileName)
		{
			// Load custom particle texture
			byte[] textureData = File.ReadAllBytes(FullPath(fileName));
			Texture2D newTexture = new Texture2D(2, 2);
			ImageConversion.LoadImage(newTexture, textureData);
			return newTexture;
		}

		internal static bool IsValidFile(string fileName)
		{
			return File.Exists(FullPath(fileName));
		}
	}
}
