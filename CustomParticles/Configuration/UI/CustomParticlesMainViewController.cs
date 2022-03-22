using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using CustomParticles.Utils;
using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CustomParticles.Configuration.UI
{
	public class CustomParticlesMainViewController : BSMLResourceViewController
	{
		public override string ResourceName => "CustomParticles.Configuration.UI.Views.ParticleList.bsml";

		#region Particle System Selection Tabs
		public Action<PartSysID> SelectedParticleSystemChange;

		private PartSysID selectedParticleSystemID;

		private void InvokeParticleSystemChanged(PartSysID particleSystemID) => SelectedParticleSystemChange?.Invoke(particleSystemID);

		[UIAction("particle-tab-selected")]
		public void OnParticleTabSelected(SegmentedControl segControl, int i)
		{
			selectedParticleSystemID = GetPartSysIDFromTab(i);
			InvokeParticleSystemChanged(selectedParticleSystemID);
		}

		public PartSysID GetPartSysIDFromTab(int i)
		{
			switch (i)
			{
				case 0:
					return PartSysID.GlobalDust;
				case 1:
					return PartSysID.NoteCutSparkle;
				case 2:
					return PartSysID.NoteCutExplosion;
				case 3:
					return PartSysID.SaberClashSparkle;
				case 4:
					return PartSysID.SaberClashGlow;
				case 5:
					return PartSysID.ObstacleSparkle;
				default:
					return PartSysID.Invalid;
			}
		}
		#endregion


		#region Particle List
		[UIComponent("particleList")]
		CustomListTableData customListTableData = null;

		public Action<int> SelectedCustomParticleChange;
		public Action CustomParticlesListReload;

		private void InvokeSelectedCustomParticleChange(int i) => SelectedCustomParticleChange?.Invoke(i);
		private void InvokeCustomParticlesListReload() => CustomParticlesListReload?.Invoke();

		public void OnSelectedCustomParticleChange(CustomParticle particle, int index)
		{
			RefreshSelectedCell(index);
			RefreshSettings(particle);
		}

		public void OnCustomParticlesListChanged(List<CustomParticle> customParticles)
		{
			SetupList(customParticles);
		}

		public void RefreshSelectedCell(int index)
		{
			customListTableData.tableView.ScrollToCellWithIdx(index, TableView.ScrollPositionType.Center, true);
			customListTableData.tableView.SelectCellWithIdx(index);
		}


		[UIAction("particleSelect")]
		public void OnParticleSelect(TableView tableView, int i)
		{
			InvokeSelectedCustomParticleChange(i);
		}

		[UIAction("deleteParticle")]
		public void OnDeleteParticle()
		{

		}

		[UIAction("reloadParticles")]
		public void OnReloadParticles()
		{
			InvokeCustomParticlesListReload();
		}

		[UIAction("#post-parse")]
		public void OnPostParse()
		{
			InvokeCustomParticlesListReload();
		}

		public void SetupList(List<CustomParticle> customParticles)
		{
			Plugin.Log.Notice("CustomParticlesMainViewController: Setting up list");

			// Check if the list is available
			if (customListTableData == null)
			{
				Plugin.Log.Error("CustomParticlesMainViewController, SetupList: customListTableData == null! | Unable to Custom Particles setup list");
				return;
			}

			// Clear the list
			customListTableData.data.Clear();

			foreach (CustomParticle customParticle in customParticles)
			{
				Sprite sprite = Sprite.Create((Texture2D)customParticle.Texture, new Rect(0, 0, customParticle.Texture.width, customParticle.Texture.height), new Vector2(0.5f, 0.5f));
				CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(customParticle.Name, "", sprite);
				customListTableData.data.Add(customCellInfo);
			}

			// Reload tableView data
			customListTableData.tableView.ReloadData();
		}

		//internal void RefreshSelectedCell()
		//{
		//Plugin.Log.Notice("Refreshing selected cell");
		//if (selectedParticle == null)
		//{
		//	// ? ? ? SELECT CUSTOM PARTICLE FOR CURRENTLY SELECTED PARTICLE SYSTEM ? ? ?
		//	Plugin.Log.Notice("No selected particle. Selecting Global Dust.");
		//	selectedParticle = particleConfigManager.GetCustomParticle(PluginConfig.Instance.ID_GlobalDustParticles);
		//	if (selectedParticle == null)
		//		selectedParticle = particleConfigManager.GetCustomParticleFirstOrDefault();
		//}

		//Plugin.Log.Notice("Finding index of particle");
		//int indexSelectedParticle = particleConfigManager.IndexOf(selectedParticle.Name);
		//Plugin.Log.Notice("Index of particle is " + indexSelectedParticle);


		//customListTableData.tableView.ScrollToCellWithIdx(indexSelectedParticle, TableView.ScrollPositionType.Beginning, false);
		//customListTableData.tableView.SelectCellWithIdx(indexSelectedParticle);
		//}


		#endregion



		// 3D PRINT NOTITIES
		//  .stl
		// thingyverse
		// curamaker
		// tijdframe


		#region Settings

		private static string enabledTextColor = "#" + ColorUtility.ToHtmlStringRGB(Color.white);
		private static string disabledTextColor = "#" + ColorUtility.ToHtmlStringRGB(Color.grey);
		private static string invalidTextColor = "#" + ColorUtility.ToHtmlStringRGB(Color.red);

		private string GetInteractabilityColor(bool isEnabled) => isEnabled ? enabledTextColor : disabledTextColor;
		private CustomParticle selectedCustomParticle = new CustomParticle(new Texture2D(0,0), new CustomParticleConfig());

		private void RefreshSettings(CustomParticle particle)
		{
			selectedCustomParticle = particle;

			UpdateSettingsValues();
		}

		private void UpdateSettingsValues()
		{
			//particleSystemEnabledCached = PluginConfig.Instance.IsEnabled(selectedParticleSystemID);

			NotifyPropertyChanged(nameof(ParticleSystemEnabled));
			NotifyPropertyChanged(nameof(AnimationMode));
			NotifyPropertyChanged(nameof(ImgCountX));
			NotifyPropertyChanged(nameof(ImgCountY));
			NotifyPropertyChanged(nameof(CycleCount));
			NotifyPropertyChanged(nameof(FPS));

			UpdateInteractability();
		}
		private void UpdateInteractability()
		{
			NotifyPropertyChanged(nameof(IsAnimationEnabled));
			NotifyPropertyChanged(nameof(IsAnimationEnabledColor));

			NotifyPropertyChanged(nameof(IsAnimatedLifetimeEnabled));
			NotifyPropertyChanged(nameof(IsAnimatedLifetimeEnabledColor));

			NotifyPropertyChanged(nameof(IsAnimatedFPSEnabled));
			NotifyPropertyChanged(nameof(IsAnimatedFPSEnabledColor));
		}

		//bool particleSystemEnabledCached = false;
		[UIValue(nameof(ParticleSystemEnabled))]
		public virtual bool ParticleSystemEnabled
		{
			get { return PluginConfig.Instance.IsEnabled(selectedParticleSystemID); }
			set
			{
				PluginConfig.Instance.SetEnabled(selectedParticleSystemID, value);
				NotifyPropertyChanged(nameof(ParticleSystemEnabled));
				UpdateSettingsValues();
			}
		}

		[UIValue(nameof(AnimationMode))]
		public virtual AnimationMode AnimationMode
		{
			get { return selectedCustomParticle.Config.AnimationMode; }
			set
			{
				selectedCustomParticle.Config.AnimationMode = value;
				NotifyPropertyChanged(nameof(AnimationMode));
				UpdateSettingsValues();
			}
		}

		[UIValue(nameof(ImgCountX))]
		public virtual int ImgCountX
		{
			get => IsAnimationEnabled ? selectedCustomParticle.Config.ImgCountX : 0;
			set => selectedCustomParticle.Config.ImgCountX = value;
		}

		[UIValue(nameof(ImgCountY))]
		public virtual int ImgCountY
		{
			get => IsAnimationEnabled ? selectedCustomParticle.Config.ImgCountY : 0;
			set => selectedCustomParticle.Config.ImgCountY = value;
		}

		[UIValue(nameof(CycleCount))]
		public virtual int CycleCount
		{
			get => IsAnimatedLifetimeEnabled ? selectedCustomParticle.Config.CycleCount : 0;
			set => selectedCustomParticle.Config.CycleCount = value;
		}

		[UIValue(nameof(FPS))]
		public virtual int FPS
		{
			get => IsAnimatedFPSEnabled ? selectedCustomParticle.Config.FPS : 0;
			set => selectedCustomParticle.Config.FPS = value;
		}


		[UIValue("is-animation-enabled")]
		private bool IsAnimationEnabled => selectedCustomParticle.Config.AnimationMode != AnimationMode.Off;
		[UIValue("is-animation-enabled-color")]
		private string IsAnimationEnabledColor => GetInteractabilityColor(IsAnimationEnabled);

		[UIValue("is-animatedlifetime-enabled")]
		private bool IsAnimatedLifetimeEnabled => selectedCustomParticle.Config.AnimationMode == AnimationMode.AnimatedLifetime;
		[UIValue("is-animatedlifetime-enabled-color")]
		private string IsAnimatedLifetimeEnabledColor => GetInteractabilityColor(IsAnimatedLifetimeEnabled);
		
		[UIValue("is-animatedfps-enabled")]
		private bool IsAnimatedFPSEnabled => selectedCustomParticle.Config.AnimationMode == AnimationMode.AnimatedFPS;
		[UIValue("is-animatedfps-enabled-color")]
		private string IsAnimatedFPSEnabledColor => GetInteractabilityColor(IsAnimatedFPSEnabled);



		[UIValue(nameof(AnimationModeLabelOptionsList))]
		public List<object> AnimationModeLabelOptionsList => AnimationModeLabelOptionsNames.Keys.Cast<object>().ToList();
		[UIAction(nameof(AnimationModeLabelOptionsFormat))]
		public string AnimationModeLabelOptionsFormat(AnimationMode mode) => AnimationModeLabelOptionsNames[mode];

		private static Dictionary<AnimationMode, string> AnimationModeLabelOptionsNames = new Dictionary<AnimationMode, string>()
		{
			{ AnimationMode.Off, "Off" },
			{ AnimationMode.StaticRandom, "Static random" },
			{ AnimationMode.AnimatedLifetime, "Animated lifetime" },
			{ AnimationMode.AnimatedFPS, "Animated FPS" }
		};
		#endregion
	}
}
