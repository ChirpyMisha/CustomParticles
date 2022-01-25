using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using HMUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CustomParticles.UI
{
	internal class ParticleSettingsViewController : BSMLResourceViewController
	{
		private static string enabledTextColor = "#" + ColorUtility.ToHtmlStringRGB(Color.white);
		private static string disabledTextColor = "#" + ColorUtility.ToHtmlStringRGB(Color.grey);

		public override string ResourceName => "CustomParticles.UI.Views.ParticleSettings.bsml";
		private PluginConfig config;
		private ParticleSettings settings;

		private string GetInteractabilityColor(bool isEnabled) => isEnabled ? enabledTextColor : disabledTextColor;

		[Inject]
		public void Construct(PluginConfig config)
		{
			this.config = config;
			settings = config.GlobalDustParticles;
		}

		private void UpdateSettingsProperties()
		{
			NotifyPropertyChanged(nameof(IsSpriteSheetEnabled));
			NotifyPropertyChanged(nameof(IsSpriteSheetEnabledColor));
			NotifyPropertyChanged(nameof(IsAnimationEnabled));
			NotifyPropertyChanged(nameof(IsAnimationEnabledColor));

			NotifyPropertyChanged(nameof(FileName));
			NotifyPropertyChanged(nameof(EnableSpriteSheet));
			NotifyPropertyChanged(nameof(AnimationTimeMode));
			NotifyPropertyChanged(nameof(ImgCountX));
			NotifyPropertyChanged(nameof(ImgCountY));
			NotifyPropertyChanged(nameof(FrameCount));
			NotifyPropertyChanged(nameof(CycleCount));
			NotifyPropertyChanged(nameof(FPS));
		}

		private void UpdateSpriteSheetEnabledProperty()
		{
			NotifyPropertyChanged(nameof(IsSpriteSheetEnabled));
			NotifyPropertyChanged(nameof(IsSpriteSheetEnabledColor));
			UpdateAnimationEnabledProperty();
		}
		private void UpdateAnimationEnabledProperty()
		{
			NotifyPropertyChanged(nameof(IsAnimationEnabled));
			NotifyPropertyChanged(nameof(IsAnimationEnabledColor));
		}

		[UIValue("is-sprite-sheet-enabled")]
		private bool IsSpriteSheetEnabled => settings.isSpriteSheetEnabled;
		[UIValue("is-animation-enabled")]
		private bool IsAnimationEnabled => IsSpriteSheetEnabled && settings.mode == ParticleSystemAnimationTimeMode.FPS;

		[UIValue("is-sprite-sheet-enabled-color")]
		private string IsSpriteSheetEnabledColor => GetInteractabilityColor(IsSpriteSheetEnabled);
		[UIValue("is-animation-enabled-color")]
		private string IsAnimationEnabledColor => GetInteractabilityColor(IsAnimationEnabled);

		[UIValue("FileName")]
		public virtual string FileName
		{
			get => settings.fileName;
			set => settings.fileName = value;
		}

		[UIValue("EnableSpriteSheet")]
		public virtual bool EnableSpriteSheet
		{
			get { return settings.isSpriteSheetEnabled; }
			set
			{
				settings.isSpriteSheetEnabled = value;
				NotifyPropertyChanged(nameof(EnableSpriteSheet));
				UpdateSpriteSheetEnabledProperty();
			}
		}

		[UIValue("AnimationTimeMode")]
		public virtual ParticleSystemAnimationTimeMode AnimationTimeMode
		{
			get { return settings.mode; }
			set
			{
				settings.mode = value;
				NotifyPropertyChanged(nameof(AnimationTimeMode));
				UpdateAnimationEnabledProperty();
			}
		}

		[UIValue("ImgCountX")]
		public virtual int ImgCountX
		{
			get => settings.imgCountX;
			set => settings.imgCountX = value;
		}

		[UIValue("ImgCountY")]
		public virtual int ImgCountY
		{
			get => settings.imgCountY;
			set => settings.imgCountY = value;
		}

		[UIValue("FrameCount")]
		public virtual int FrameCount
		{
			get => settings.frameCount;
			set => settings.frameCount = value;
		}

		[UIValue("CycleCount")]
		public virtual int CycleCount
		{
			get => settings.cycleCount;
			set => settings.cycleCount = value;
		}

		[UIValue("FPS")]
		public virtual int FPS
		{
			get => settings.fps;
			set => settings.fps = value;
		}

		[UIAction("particle-tab-selected")]
		public void OnParticleTabSelected(SegmentedControl segControl, int i)
		{
			Plugin.Log.Notice("== Tab Selected ==");
			Plugin.Log.Notice($"segControl: Name={segControl.name}, selectedCellNumber={segControl.selectedCellNumber} tag={segControl.tag}");

			switch (i)
			{
				case 0:
					settings = config.GlobalDustParticles;
					break;
				case 1:
					settings = config.NoteCutSparkleParticles;
					break;
				case 2:
					settings = config.NoteCutExplosionParticles;
					break;
				case 3:
					settings = config.SaberClashSparkleParticles;
					break;
				case 4:
					settings = config.SaberClashGlowParticles;
					break;
				case 5:
					settings = config.ObstacleSaberSparkleParticles;
					break;
			}

			UpdateSettingsProperties();
		} 



		[UIValue(nameof(AnimationTimeModeLabelOptionsList))]
		public List<object> AnimationTimeModeLabelOptionsList => AnimationTimeModeLabelOptionsToNames.Keys.Cast<object>().ToList();

		[UIAction(nameof(AnimationTimeModeLabelOptionsFormat))]
		public string AnimationTimeModeLabelOptionsFormat(ParticleSystemAnimationTimeMode mode) => AnimationTimeModeLabelOptionsToNames[mode];

		private static Dictionary<ParticleSystemAnimationTimeMode, string> AnimationTimeModeLabelOptionsToNames = new Dictionary<ParticleSystemAnimationTimeMode, string>()
		{
			{ ParticleSystemAnimationTimeMode.Lifetime, "Lifetime" },
			{ ParticleSystemAnimationTimeMode.FPS, "FPS" }
		};
	}
}
