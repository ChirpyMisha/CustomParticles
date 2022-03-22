using CustomParticles.Configuration.UI;
using CustomParticles.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CustomParticles.Configuration
{
	public class CustomParticleConfigManager : IInitializable, IDisposable
	{
		public static CustomParticleConfigManager Instance { get; set; }

		public Action<List<CustomParticle>> CustomParticlesListChanged;
		public Action<CustomParticle, int> SelectedCustomParticleChanged;

		private void InvokeCustomParticlesListChanged() => CustomParticlesListChanged?.Invoke(customParticles);
		private void InvokeSelectedCustomParticleChanged() => SelectedCustomParticleChanged?.Invoke(customParticles[selectedCustomParticleIndex], selectedCustomParticleIndex);

		private List<CustomParticle> customParticles = new List<CustomParticle>();
		private int selectedCustomParticleIndex = 0;
		private PartSysID selectedParticleSystem = PartSysID.GlobalDust;

		private List<CustomParticle> GetCustomParticlesListSorted() => customParticles.OrderBy(customParticle => customParticle.Name).ToList();
		private CustomParticle GetCustomParticle(int i) => customParticles[i];
		private CustomParticle GetCustomParticle(string name) => customParticles.Where(customParticle => customParticle.Name == name)?.FirstOrDefault();
		public CustomParticle GetCustomParticle(PartSysID partSysID) => GetCustomParticle(PluginConfig.Instance.GetCustomParticleName(partSysID));
		private int GetCustomParticleIndex(string name) => customParticles.FindIndex(customParticle => customParticle.Name == name);
		private int GetCustomParticleIndex(PartSysID partSysID) => GetCustomParticleIndex(PluginConfig.Instance.GetCustomParticleName(partSysID));

		public void Initialize()
		{
			if (Instance == null)
				Instance = this;

			LoadCustomParticles();
		}

		internal void OnCustomParticleListReload()
		{
			LoadCustomParticles();
		}

		public void Dispose()
		{
			Instance = null;
		}

		public void OnSelectedParticleSystemChange(PartSysID partSysID)
		{
			selectedParticleSystem = partSysID;
			selectedCustomParticleIndex = GetCustomParticleIndex(partSysID);
			if (selectedCustomParticleIndex < 0)
			{
				selectedCustomParticleIndex = 0;

				if (PluginConfig.Instance.GetCustomParticleName(selectedParticleSystem) == "")
				{
					string newName = customParticles[selectedCustomParticleIndex].Name;
					PluginConfig.Instance.SetCustomParticleName(selectedParticleSystem, newName);
				}
			}

			InvokeSelectedCustomParticleChanged();
		}

		public void OnSelectedCustomParticleChange(int index)
		{
			selectedCustomParticleIndex = index;
			SaveCustomParticleSelection();
			InvokeSelectedCustomParticleChanged();
		}

		public void LoadCustomParticles()
		{
			Plugin.Log.Notice("CustomParticleConfigManager, LoadCustomParticles: Loading Custom Particles...");
			customParticles = new List<CustomParticle>();

			// Find the names of the available Custom Particles.
			List<string> customParticleNames = FindNamesAvailableTextures();
			Plugin.Log.Notice($"Found {customParticleNames.Count} custom particle files.");

			// Load previously saved custom particle configs	(note: PluginConfig.Instance.CustomParticleConfigs should never be null).
			List<CustomParticleConfig> customParticleConfigs = (PluginConfig.Instance.CustomParticleConfigs != null)
				? PluginConfig.Instance.CustomParticleConfigs
				: new List<CustomParticleConfig>();
			Plugin.Log.Notice($"Loaded {customParticleConfigs.Count} particle configs.");

			// Create CustomParticle objects for the files that were not yet loaded before.
			foreach (CustomParticleConfig config in customParticleConfigs)
			{
				if (GetCustomParticleIndex(config.Name) < 0)
					customParticles.Add(new CustomParticle(config));

				customParticleNames.Remove(config.Name);
			}

			//// For each config, get its texture (if possible) and add it to the customParticles list.
			//foreach (CustomParticleConfig config in particleConfigs)
			//{
			//	// Get the texture for each config
			//	Texture texture = particleTextures.Where(t => t.name == config.Name).FirstOrDefault();
			//	particleTextures.Remove(texture);

			//	customParticles.Add(new CustomParticle(texture, config));
			//}


			// For each texture without a config, create a new config and add the particle.
			foreach (string name in customParticleNames)
			{
				CustomParticleConfig newConfig = new CustomParticleConfig();
				newConfig.Name = name;
				customParticles.Add(new CustomParticle(newConfig));
			}
			Plugin.Log.Notice($"Created configs for {customParticleNames.Count} new textures.");

			// Sort the particles by name.
			customParticles = GetCustomParticlesListSorted();

			if (customParticleNames.Count > 0)
				SaveCustomParticles();

			if (customParticles.Count == 0)
				Plugin.Log.Warn("No Custom Particles in the list. Please add particle textures to the UserData/CustomParticles folder");

			InvokeCustomParticlesListChanged();

			ValidateSelectedCustomParticleIndex();
		}

		private void ValidateSelectedCustomParticleIndex()
		{
			int newSelectedCustomParticleIndex = GetCustomParticleIndex(selectedParticleSystem);

			if (newSelectedCustomParticleIndex < 0)
			{
				Plugin.Log.Warn("Couldn't validate selected CustomParticle index. Couldn't find CustomParticle for selectedParticleSystem");
				return;
			}

			if (newSelectedCustomParticleIndex != selectedCustomParticleIndex)
			{
				selectedCustomParticleIndex = newSelectedCustomParticleIndex;
				InvokeSelectedCustomParticleChanged();
			}
		}

		/// <summary>
		/// Loads particle textures from the CustomParticles folder in the UserData folder.
		/// </summary>
		/// <returns>A list of all textures in the CustomParticles folder</returns>
		private List<string> FindNamesAvailableTextures()
		{
			List<string> namesAvailableTextures = new List<string>();

			// Process the list of files found in the directory
			string[] fileEntries = Directory.GetFiles(PluginConfig.DefaultPath);
			foreach (string filePath in fileEntries)
			{
				// Check if the file is a PNG
				if (!filePath.EndsWith(".png"))
				{
					Plugin.Log.Warn($"ParticlesUtils, FindNamesAvailableTextures: Failed to process \"{filePath}\". Reason: File type not supported.");
					continue;
				}

				// Get the name of the file and discard the path and filetype
				string fileName = Path.GetFileName(filePath).Replace(".png", "");
				namesAvailableTextures.Add(fileName);
			}

			return namesAvailableTextures;
		}
		#region Deprecated: LoadParticleTextures() { }
		//private List<Texture> LoadParticleTextures()
		//{
		//	// Create empty list of particle textures
		//	List<Texture> particleTextures = new List<Texture>();

		//	// Process the list of files found in the directory
		//	string[] fileEntries = Directory.GetFiles(PluginConfig.DefaultPath);
		//	foreach (string filePath in fileEntries)
		//	{
		//		// Get the name of the file and discard the path
		//		string fileName = Path.GetFileName(filePath);

		//		// Check if the file is a PNG
		//		if (!fileName.EndsWith(".png"))
		//		{
		//			Plugin.Log.Warn($"ParticlesUtils, LoadParticlesFromFolder - Cannot load file \"{fileName}\". Reason: File type not supported.");
		//			continue;
		//		}

		//		fileName = fileName.Replace(".png", "");

		//		CustomParticle customParticle = customParticles.Where(t => t.Name == fileName)?.FirstOrDefault();
		//		if (customParticle != null && customParticle.FileModifiedSinceLastLoad)
		//		{
		//			Texture newTexture = ImgUtils.LoadTexture(fileName);
		//			newTexture.name = fileName;
		//			particleTextures.Add(newTexture);
		//		}
		//	}

		//	return particleTextures;
		//}
		#endregion

		public void SaveCustomParticles()
		{
			Plugin.Log.Notice($"saving {customParticles.Count} custom particle configs");
			PluginConfig.Instance.CustomParticleConfigs = new List<CustomParticleConfig>();
			foreach (CustomParticle customParticle in customParticles)
				PluginConfig.Instance.CustomParticleConfigs.Add(customParticle.Config);
			PluginConfig.Instance.Changed();
		}

		public void SaveCustomParticleSelection()
		{
			string newName = customParticles[selectedCustomParticleIndex].Name;
			PluginConfig.Instance.SetCustomParticleName(selectedParticleSystem, newName);
		}

		internal List<CustomParticle> GetCustomParticles()
		{
			return customParticles;
		}

		public CustomParticle GetCustomParticleFirstOrDefault()
		{
			if (customParticles.Count > 0)
				return customParticles[0];

			Plugin.Log.Error("PlarticleConfigManager, GetCustomParticleFirstOrDefault: customParticles.Count == 0 | DEFAULT NOT IMPLEMENTED! | Returning NULL");
			return null;
		}

		public int IndexOf(string particleName)
		{
			for (int i = 0; i < customParticles.Count - 1; i++)
				if (particleName == customParticles[i].Name)
					return i;
			return 0;
		}

		public void SelectCustomParticle(int index)
		{
			selectedCustomParticleIndex = index;

			InvokeSelectedCustomParticleChanged();
		}
	}
}
