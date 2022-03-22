using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using Zenject;
using CustomParticles.Configuration;

namespace CustomParticles.Configuration.UI
{
	internal class ParticleFlowCoordinator : FlowCoordinator
	{
		private MainFlowCoordinator mainFlow;
		private CustomParticlesInfoController infoView;
		private CustomParticlesMainViewController customParticlesMainView;
		private ParticlePreviewViewController particlePreviewView;
		//private CustomParticleConfigManager particleConfigManager;

		[Inject]
		public void Construct(MainFlowCoordinator mainFlow, CustomParticlesMainViewController customParticlesMainView, ParticlePreviewViewController particlePreviewView, CustomParticlesInfoController infoView)
		{
			this.mainFlow = mainFlow;
			this.infoView = infoView;
			this.customParticlesMainView = customParticlesMainView;
			this.particlePreviewView = particlePreviewView;
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("Custom Particles");
				showBackButton = true;
			}

			ProvideInitialViewControllers(customParticlesMainView, infoView, particlePreviewView);

			customParticlesMainView.SelectedParticleSystemChange += CustomParticleConfigManager.Instance.OnSelectedParticleSystemChange;
			customParticlesMainView.SelectedCustomParticleChange += CustomParticleConfigManager.Instance.OnSelectedCustomParticleChange;
			customParticlesMainView.CustomParticlesListReload += CustomParticleConfigManager.Instance.OnCustomParticleListReload;

			CustomParticleConfigManager.Instance.CustomParticlesListChanged += customParticlesMainView.OnCustomParticlesListChanged;
			CustomParticleConfigManager.Instance.SelectedCustomParticleChanged += customParticlesMainView.OnSelectedCustomParticleChange;
			CustomParticleConfigManager.Instance.SelectedCustomParticleChanged += particlePreviewView.OnSelectedCustomParticleChanged;
		}

		protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
		{
			customParticlesMainView.SelectedParticleSystemChange -= CustomParticleConfigManager.Instance.OnSelectedParticleSystemChange;
			customParticlesMainView.SelectedCustomParticleChange -= CustomParticleConfigManager.Instance.OnSelectedCustomParticleChange;
			customParticlesMainView.CustomParticlesListReload -= CustomParticleConfigManager.Instance.OnCustomParticleListReload;

			CustomParticleConfigManager.Instance.CustomParticlesListChanged -= customParticlesMainView.OnCustomParticlesListChanged;
			CustomParticleConfigManager.Instance.SelectedCustomParticleChanged -= customParticlesMainView.OnSelectedCustomParticleChange;

			base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			mainFlow.DismissFlowCoordinator(this, null);
		}
	}
}
