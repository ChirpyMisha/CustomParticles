using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomParticles.Configuration
{
    internal class Config
    {
        public static Config Instance { get; set; }
		private static readonly ParticleSettings defaultSettings = new ParticleSettings("", ParticleSystemAnimationTimeMode.Lifetime, 1, 1, 1, 0, 0);

		public virtual float GlobalDustParticleSizeMinimum { get; set; } = 0.01f;
		public virtual float GlobalDustParticleSizeMaximum { get; set; } = 0.01f;
		public virtual ParticleSettings GlobalDustParticles { get; set; } = defaultSettings;
		public virtual ParticleSettings NoteCutSparkleParticles { get; set; } = defaultSettings;
		public virtual ParticleSettings NoteCutExplosionParticles { get; set; } = defaultSettings;
		public virtual ParticleSettings ObstacleSaberSparkleParticles { get; set; } = defaultSettings;
		public virtual ParticleSettings SaberClashSparkleParticles { get; set; } = defaultSettings;
		public virtual ParticleSettings SaberClashGlowParticles { get; set; } = defaultSettings;

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(Config other)
        {
            // This instance's members populated from other
        }
    }

	public struct ParticleSettings
	{
		public string fileName { get; set; }
		public ParticleSystemAnimationTimeMode mode { get; set; }
		public int imgCountX { get; set; }
		public int imgCountY { get; set; }
		public int frameCount { get; set; }
		public int cycleCount { get; set; }
		public int fps { get; set; }

		//public ParticleSettings(ParticleSystemAnimationTimeMode mode, int imgCountX, int imgCountY, int frameCount)
		//{
		//	ParticleSettings(mode, imgCountX, imgCountY, frameCount, 0, 0);
		//	//this.mode = mode;
		//	//this.imgCountX = imgCountX;
		//	//this.imgCountY = imgCountY;
		//	//this.frameCount = frameCount;
		//	//this.cycleCount = 0;
		//	//this.fps = 0;
		//}
		public ParticleSettings(string fileName, ParticleSystemAnimationTimeMode mode, int imgCountX, int imgCountY, int frameCount, int fps = 0, int cycleCount = 0)
		{
			this.fileName = fileName;
			this.mode = mode;
			this.imgCountX = imgCountX;
			this.imgCountY = imgCountY;
			this.frameCount = frameCount;
			this.cycleCount = cycleCount;
			this.fps = fps;
		}
	}
}
