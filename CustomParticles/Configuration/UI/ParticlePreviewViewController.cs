using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using CustomParticles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomParticles.Configuration.UI
{
	internal class ParticlePreviewViewController : BSMLResourceViewController
	{
		public override string ResourceName => "CustomParticles.Configuration.UI.Views.ParticlePreview.bsml";
		private string selectedParticle = "";

		[UIValue("img-path")]
		private string ImagePath => ImgUtils.FullPath(selectedParticle);

		public ParticlePreviewViewController()
		{
			PluginConfig config = PluginConfig.Instance;
			if (config.IsEnabled(PartSysID.GlobalDust))
				selectedParticle = config.GetCustomParticleName(PartSysID.GlobalDust);
		}

		//internal void OnSelectedParticleSystemChanged(string selectedParticle)
		//{
		//	this.selectedParticle = selectedParticle;
		//	NotifyPropertyChanged(nameof(ImagePath));
		//}

		//internal void OnSelectedImageChanged()
		//{
		//	NotifyPropertyChanged(nameof(ImagePath));
		//}

		internal void OnSelectedCustomParticleChanged(CustomParticle particle, int index)
		{
			selectedParticle = particle.Name;
			NotifyPropertyChanged(nameof(ImagePath));
		}
	}
}
