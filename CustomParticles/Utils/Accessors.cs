using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomParticles.Utils
{
	/// <summary>
	/// Class that contains various <see cref="FieldAccessor{T, U}"/>s that see use within CustomParticles.
	/// </summary>
	static class Accessors
	{
		public static FieldAccessor<NoteCutCoreEffectsSpawner, NoteCutParticlesEffect>.Accessor NoteCutParticlesEffect = FieldAccessor<NoteCutCoreEffectsSpawner, NoteCutParticlesEffect>.GetAccessor("_noteCutParticlesEffect");

		public static FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.Accessor NoteCutSparklePS = FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.GetAccessor("_sparklesPS");
		public static FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.Accessor NoteCutExplosionPS = FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.GetAccessor("_explosionPS");
		//public static FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.Accessor CorePS = FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.GetAccessor("_corePS");

		public static FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.Accessor SaberClashGlowPS = FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.GetAccessor("_explosionPS");
		public static FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.Accessor SaberClashSparklePS = FieldAccessor<NoteCutParticlesEffect, ParticleSystem>.GetAccessor("_sparklesPS");

		public static FieldAccessor<ObstacleSaberSparkleEffect, ParticleSystem>.Accessor ObstacleSaberSparklePS = FieldAccessor<ObstacleSaberSparkleEffect, ParticleSystem>.GetAccessor("_sparkleParticleSystem");
	}
}
