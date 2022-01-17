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
		public static readonly int SPRITE_DIMENSIONS = 128;
		public static readonly string DefaultPath = $@"{UnityGame.UserDataPath}\CustomParticles\";

		public static bool IsTextureAtlas(Texture texture) => (NumberOfSpritesX(texture) > 1 || NumberOfSpritesY(texture) > 1);
		public static int NumberOfSpritesX(Texture texture) => texture.width / SPRITE_DIMENSIONS;
		public static int NumberOfSpritesY(Texture texture) => texture.height / SPRITE_DIMENSIONS;

		public static Texture2D LoadTexture(string path)
		{
			// Load custom particle texture
			byte[] textureData = File.ReadAllBytes(path);
			Texture2D newTexture = new Texture2D(2, 2);
			ImageConversion.LoadImage(newTexture, textureData);
			return newTexture;
		}
	}
}
