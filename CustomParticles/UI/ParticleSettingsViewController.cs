using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using CustomParticles.Utils;
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
		private static string invalidTextColor = "#" + ColorUtility.ToHtmlStringRGB(Color.red);
		public override string ResourceName => "CustomParticles.UI.Views.ParticleSettings.bsml";

		internal Action<ParticleSettings> selectedParticleSystemChanged;
		internal Action selectedImageChanged;
		private void InvokeParticleSystemChanged() => selectedParticleSystemChanged?.Invoke(settings);
		private void InvokeImageChanged() => selectedImageChanged?.Invoke();

		private PluginConfig config;
		private ParticleSettings settings;

		private string GetInteractabilityColor(bool isEnabled) => isEnabled ? enabledTextColor : disabledTextColor;
		private string GetValidityColor(bool isValid) => isValid ? enabledTextColor : invalidTextColor;

		//private float tempSpriteRangeBegin = -1;
		//private float tempSpriteRangeEnd = -1;
		private string selectedTabName = "Global Dust";

		[Inject]
		public void Construct(PluginConfig config)
		{
			this.config = config;
			settings = config.GlobalDustParticles;
		}

		private void UpdateSettingsProperties()
		{
			UpdateSpriteSheetEnabledProperty();

			NotifyPropertyChanged(nameof(FileName));
			NotifyPropertyChanged(nameof(IsFileNameValidColor));
			NotifyPropertyChanged(nameof(FileNameSettingString));
			NotifyPropertyChanged(nameof(EnableSpriteSheet));
			NotifyPropertyChanged(nameof(AnimationTimeMode));
			NotifyPropertyChanged(nameof(ImgCountX));
			NotifyPropertyChanged(nameof(ImgCountY));
			//NotifyPropertyChanged(nameof(SpriteRangeBegin));
			//NotifyPropertyChanged(nameof(SpriteRangeEnd));
			//NotifyPropertyChanged(nameof(CycleCount));
			NotifyPropertyChanged(nameof(FPS));
		}

		private void UpdateSpriteSheetEnabledProperty()
		{
			NotifyPropertyChanged(nameof(IsSpriteSheetEnabled));
			NotifyPropertyChanged(nameof(IsSpriteSheetEnabledColor));
			NotifyPropertyChanged(nameof(ImgCountX));
			NotifyPropertyChanged(nameof(ImgCountY));
			UpdateAnimationProperties();
		}
		private void UpdateAnimationProperties()
		{
			//NotifyPropertyChanged(nameof(IsLifetimeEnabled));
			NotifyPropertyChanged(nameof(IsFPSEnabled));
			//NotifyPropertyChanged(nameof(IsLifetimeEnabledColor));
			NotifyPropertyChanged(nameof(IsFPSEnabledColor));

			//NotifyPropertyChanged(nameof(SpriteRangeBegin));
			//NotifyPropertyChanged(nameof(SpriteRangeEnd));
			NotifyPropertyChanged(nameof(FPS));
		}

		


		[UIValue("is-sprite-sheet-enabled")]
		private bool IsSpriteSheetEnabled => settings.isSpriteSheetEnabled;
		//[UIValue("is-lifetime-enabled")]
		//private bool IsLifetimeEnabled => IsSpriteSheetEnabled && settings.mode == ParticleSystemAnimationTimeMode.Lifetime;
		[UIValue("is-fps-enabled")]
		private bool IsFPSEnabled => IsSpriteSheetEnabled && settings.mode == ParticleSystemAnimationTimeMode.FPS;

		[UIValue("is-sprite-sheet-enabled-color")]
		private string IsSpriteSheetEnabledColor => GetInteractabilityColor(IsSpriteSheetEnabled);
		//[UIValue("is-lifetime-enabled-color")]
		//private string IsLifetimeEnabledColor => GetInteractabilityColor(IsLifetimeEnabled);
		[UIValue("is-fps-enabled-color")]
		private string IsFPSEnabledColor => GetInteractabilityColor(IsFPSEnabled);

		[UIValue("is-file-name-valid-color")]
		private string IsFileNameValidColor => GetValidityColor(ImgUtils.IsValidFile(settings.fileName));
		[UIValue("file-name-setting-string")]
		private string FileNameSettingString => $"{selectedTabName} File Name";

		[UIValue("FileName")]
		public virtual string FileName
		{
			get => settings.fileName;
			set
			{
				settings.fileName = value;
				NotifyPropertyChanged(nameof(IsFileNameValidColor));
				InvokeImageChanged();
			}
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
				UpdateAnimationProperties();
			}
		}

		[UIValue("ImgCountX")]
		public virtual int ImgCountX
		{
			get => IsSpriteSheetEnabled ? settings.imgCountX : 0;
			set
			{
				settings.imgCountX = value;
				//if (ImgCountX * ImgCountY < SpriteRangeEnd)
				//{
				//	tempSpriteRangeEnd = ImgCountX * ImgCountY;
				//	if (tempSpriteRangeEnd < SpriteRangeBegin)
				//		tempSpriteRangeBegin = ImgCountX * ImgCountY;
				//}
			}
		}

		[UIValue("ImgCountY")]
		public virtual int ImgCountY
		{
			get => IsSpriteSheetEnabled ? settings.imgCountY : 0;
			set
			{
				settings.imgCountY = value;
			}
		}

		//[UIValue("SpriteRangeBegin")]
		//public virtual float SpriteRangeBegin
		//{
		//	get => IsLifetimeEnabled ? settings.spriteRangeBegin : 0;
		//	set
		//	{
		//		settings.spriteRangeBegin = value;
		//		if (value > settings.spriteRangeEnd) 
		//			SpriteRangeEnd = value;
		//		NotifyPropertyChanged(nameof(SpriteRangeEnd));
		//		NotifyPropertyChanged(nameof(SpriteRangeBegin));
		//	}
		//}

		//[UIValue("SpriteRangeEnd")]
		//public virtual float SpriteRangeEnd
		//{
		//	get => IsLifetimeEnabled ? settings.spriteRangeEnd : 0;
		//	set
		//	{
		//		settings.spriteRangeEnd = value;
		//		if (value < settings.spriteRangeBegin) 
		//			SpriteRangeBegin = value;
		//		NotifyPropertyChanged(nameof(SpriteRangeBegin));
		//		NotifyPropertyChanged(nameof(SpriteRangeEnd));
		//	}
		//}

		//[UIValue("CycleCount")]
		//public virtual int CycleCount
		//{
		//	get => settings.cycleCount;
		//	set => settings.cycleCount = value;
		//}

		[UIValue("FPS")]
		public virtual int FPS
		{
			get => IsFPSEnabled ? settings.fps : 0;
			set => settings.fps = value;
		}

		[UIAction("particle-tab-selected")]
		public void OnParticleTabSelected(SegmentedControl segControl, int i)
		{
			//Plugin.Log.Notice("== Tab Selected ==");
			//Plugin.Log.Notice($"segControl: Name={segControl.name}, selectedCellNumber={segControl.selectedCellNumber} tag={segControl.tag}");

			switch (i)
			{
				case 0:
					settings = config.GlobalDustParticles;
					selectedTabName = "Global Dust";
					break;
				case 1:
					settings = config.NoteCutSparkleParticles;
					selectedTabName = "NoteCut Sparkle";
					break;
				case 2:
					settings = config.NoteCutExplosionParticles;
					selectedTabName = "NoteCut Explosion";
					break;
				case 3:
					settings = config.SaberClashSparkleParticles;
					selectedTabName = "SaberClash Sparkle";
					break;
				case 4:
					settings = config.SaberClashGlowParticles;
					selectedTabName = "SaberClash Glow";
					break;
				case 5:
					settings = config.ObstacleSaberSparkleParticles;
					selectedTabName = "Obstacle Sparkle";
					break;
			}

			UpdateSettingsProperties();
			selectedParticleSystemChanged?.Invoke(settings);
		} 



		[UIValue(nameof(AnimationTimeModeLabelOptionsList))]
		public List<object> AnimationTimeModeLabelOptionsList => AnimationTimeModeLabelOptionsToNames.Keys.Cast<object>().ToList();

		[UIAction(nameof(AnimationTimeModeLabelOptionsFormat))]
		public string AnimationTimeModeLabelOptionsFormat(ParticleSystemAnimationTimeMode mode) => AnimationTimeModeLabelOptionsToNames[mode];

		private static Dictionary<ParticleSystemAnimationTimeMode, string> AnimationTimeModeLabelOptionsToNames = new Dictionary<ParticleSystemAnimationTimeMode, string>()
		{
			{ ParticleSystemAnimationTimeMode.Lifetime, "Random Static" },
			{ ParticleSystemAnimationTimeMode.FPS, "Animated" }
		};
	}
}
