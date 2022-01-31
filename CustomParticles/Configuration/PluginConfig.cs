using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomParticles.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
		private static readonly ParticleSettings defaultSettings = new ParticleSettings("", false, ParticleSystemAnimationTimeMode.Lifetime, 1, 1, 1, 0, 0);

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
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }

	public class ParticleSettings
	{
		public string fileName { get; set; }
		public bool isSpriteSheetEnabled { get; set; }
		public ParticleSystemAnimationTimeMode mode { get; set; }
		public int imgCountX { get; set; }
		public int imgCountY { get; set; }
		//internal float spriteRangeBegin { get; set; }
		//internal float spriteRangeEnd { get; set; }
		//public int cycleCount { get; set; }
		public int fps { get; set; }

		public ParticleSettings()
		{
			new ParticleSettings("defaultSprite.png", false, ParticleSystemAnimationTimeMode.Lifetime, 1, 1, 1, 0, 1);
		}
		public ParticleSettings(string fileName, bool isSpriteSheetEnabled, ParticleSystemAnimationTimeMode mode, int imgCountX, int imgCountY, float spriteRangeBegin, float spriteRangeEnd, int fps = 0, int cycleCount = 0)
		{
			this.fileName = fileName;
			this.isSpriteSheetEnabled = isSpriteSheetEnabled;
			this.mode = mode;
			this.imgCountX = imgCountX;
			this.imgCountY = imgCountY;
			//this.spriteRangeBegin = spriteRangeBegin;
			//this.spriteRangeEnd = spriteRangeEnd;
			//this.cycleCount = cycleCount;
			this.fps = fps;
		}
	}
}
