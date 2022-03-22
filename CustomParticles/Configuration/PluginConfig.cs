using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CustomParticles.Configuration.UI;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Utilities;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace CustomParticles.Configuration
{
    internal class PluginConfig
    {
		public static readonly string DefaultPath = $@"{UnityGame.UserDataPath}\CustomParticles\";
		//private ParticleSystemConfig Config(PartSysID partSysID) => GetParticleSystemConfig(partSysID);

		public static PluginConfig Instance { get; set; }

		[UseConverter, NonNullable]
		public List<CustomParticleConfig> CustomParticleConfigs { get; set; } = new List<CustomParticleConfig>();

		public virtual float GlobalDustSizeMinimum { get; set; } = 0.01f;
		public virtual float GlobalDustSizeMaximum { get; set; } = 0.01f;

		public virtual ParticleSystemConfig GlobalDustConfig { get; set; } = new ParticleSystemConfig();
		public virtual ParticleSystemConfig NoteCutSparkleConfig { get; set; } = new ParticleSystemConfig();
		public virtual ParticleSystemConfig NoteCutExplosionConfig { get; set; } = new ParticleSystemConfig();
		public virtual ParticleSystemConfig ObstacleSaberSparkleConfig { get; set; } = new ParticleSystemConfig();
		public virtual ParticleSystemConfig SaberClashSparkleConfig { get; set; } = new ParticleSystemConfig();
		public virtual ParticleSystemConfig SaberClashGlowConfig { get; set; } = new ParticleSystemConfig();

		public virtual ParticleSystemConfig GetParticleSystemConfig(PartSysID partSysID)
		{
			switch (partSysID)
			{
				case PartSysID.GlobalDust:
					return GlobalDustConfig;
				case PartSysID.NoteCutSparkle:
					return NoteCutSparkleConfig;
				case PartSysID.NoteCutExplosion:
					return NoteCutExplosionConfig;
				case PartSysID.SaberClashSparkle:
					return SaberClashSparkleConfig;
				case PartSysID.SaberClashGlow:
					return SaberClashGlowConfig;
				case PartSysID.ObstacleSparkle:
					return ObstacleSaberSparkleConfig;
				default:
					return new ParticleSystemConfig();
			}
		}

		//public bool IsEnabled(PartSysID partSysID) => Config(partSysID).IsEnabled;
		//public void SetEnabled(PartSysID partSysID, bool doEnable = true) => Config(partSysID).IsEnabled = doEnable;

		//public string GetCustomParticleName(PartSysID partSysID) => Config(partSysID).CustomParticleName;
		//public void SetCustomParticleName(PartSysID partSysID, string newName) => Config(partSysID).CustomParticleName = newName;
		public virtual bool IsEnabled(PartSysID partSysID) => GetParticleSystemConfig(partSysID).IsEnabled;
		public virtual void SetEnabled(PartSysID partSysID, bool doEnable = true) => GetParticleSystemConfig(partSysID).IsEnabled = doEnable;

		public virtual string GetCustomParticleName(PartSysID partSysID) => GetParticleSystemConfig(partSysID).CustomParticleName;
		public virtual void SetCustomParticleName(PartSysID partSysID, string newName) => GetParticleSystemConfig(partSysID).CustomParticleName = newName;


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

	public enum AnimationMode
	{
		Off,
		StaticRandom,
		AnimatedLifetime,
		AnimatedFPS
	}
}
