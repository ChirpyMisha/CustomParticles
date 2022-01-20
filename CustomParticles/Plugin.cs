using CustomParticles.Patches;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace CustomParticles
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		public static Harmony harmony;

		internal static Plugin Instance { get; private set; }
		internal static IPALogger Log { get; private set; }

		[Init]
		/// <summary>
		/// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
		/// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
		/// Only use [Init] with one Constructor.
		/// </summary>
		public Plugin(IPALogger logger)
		{
			Instance = this;
			Plugin.Log = logger;
		}

		#region BSIPA Config
		//Uncomment to use BSIPA's config
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.Config.Instance = conf.Generated<Configuration.Config>();
            Plugin.Log?.Debug("Config loaded");
        }
		#endregion

		private void SceneManager_activeSceneChanged(Scene scene1, Scene scene2)
		{
			if (scene1.name == "MainMenu" || scene2.name == "GameCore")
				GlobalDustParticlesController.LoadCustomParticles();
		}


		[OnEnable]
		public void OnApplicationStart()
		{
			harmony = new Harmony("com.ChirpyMisha.BeatSaber.CustomParticles");
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		}

		[OnDisable]
		public void OnApplicationQuit()
		{
			harmony.UnpatchSelf();
			GlobalDustParticlesController.LoadCustomParticles();

			SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
		}
	}
}
