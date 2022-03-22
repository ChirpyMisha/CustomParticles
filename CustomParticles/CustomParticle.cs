using CustomParticles.Configuration;
using CustomParticles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace CustomParticles
{
	public class CustomParticle
	{
		public Texture Texture { get; private set; }
		public CustomParticleConfig Config { get; private set; }
		public string Name { get => Config.Name; }
		private DateTime textureLastWriteTime;
		public bool FileModifiedSinceLastLoad => textureLastWriteTime.CompareTo(ImgUtils.GetLastWriteTime(Name)) != 0;
		public bool IsValid => Texture != null;

		public CustomParticle(CustomParticleConfig config)
		{
			Config = config;
			textureLastWriteTime = ImgUtils.GetLastWriteTime(Name);

			LoadTexture();
			//Thread t = new Thread(LoadTexture);
			//t.Start();
		}
		public CustomParticle(Texture texture, CustomParticleConfig config)
		{
			Texture = texture;
			Config = config;
			textureLastWriteTime = ImgUtils.GetLastWriteTime(Name);
		}

		public bool ReloadTexture(bool reloadAsync = false)
		{
			bool isModified = FileModifiedSinceLastLoad;
			if (isModified)
			{
				if (reloadAsync)
				{
					Thread t = new Thread(LoadTexture);
					t.Start();
				}
				else
					LoadTexture();
			}
			return isModified;
		}

		private void LoadTexture()
		{
			if (ImgUtils.ImgExists(Name))
				Texture = ImgUtils.LoadTexture(Name);
			else
				Texture = ImgUtils.LoadParticleUnavailableTexture();
		}
	}
}
