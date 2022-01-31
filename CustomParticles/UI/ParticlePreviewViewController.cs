using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using CustomParticles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomParticles.UI
{
	internal class ParticlePreviewViewController : BSMLResourceViewController
	{
		public override string ResourceName => "CustomParticles.UI.Views.ParticlePreview.bsml";
		private ParticleSettings settings = PluginConfig.Instance.GlobalDustParticles;

		[UIValue("img-path")]
		private string ImagePath => ImgUtils.FullPath(settings.fileName);

		internal void OnSelectedParticleSystemChanged(ParticleSettings settings)
		{
			this.settings = settings;
			NotifyPropertyChanged(nameof(ImagePath));
		}

		internal void OnSelectedImageChanged()
		{
			NotifyPropertyChanged(nameof(ImagePath));
		}
	}
}
