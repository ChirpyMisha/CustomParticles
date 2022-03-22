using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CustomParticles.Configuration.UI
{
	class CustomParticlesInfoController : BSMLResourceViewController
	{
		public override string ResourceName => "CustomParticles.Configuration.UI.Views.CustomParticlesInfo.bsml";

		private PluginConfig config;

		[Inject]
		public void Construct(PluginConfig config)
		{
			this.config = config;
		}
	}
}
