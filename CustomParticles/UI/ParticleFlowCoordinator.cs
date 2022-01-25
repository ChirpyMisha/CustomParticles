using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using Zenject;

namespace CustomParticles.UI
{
	internal class ParticleFlowCoordinator : FlowCoordinator
	{
		private MainFlowCoordinator mainFlow;
		private ParticleListViewController particleListView;
		private ParticleSettingsViewController particleSettingsView;
		private ParticlePreviewViewController particlePreviewView;

		[Inject]
		public void Construct(MainFlowCoordinator mainFlow, ParticleListViewController particleListView, ParticleSettingsViewController particleSettingsView, ParticlePreviewViewController particlePreviewView)
		{
			this.mainFlow = mainFlow;
			this.particleListView = particleListView;
			this.particleSettingsView = particleSettingsView;
			this.particlePreviewView = particlePreviewView;
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("Custom Particles");
				showBackButton = true;
			}
			ProvideInitialViewControllers(particleSettingsView);//, null, particlePreviewView);

			//ProvideInitialViewControllers(particleListView, particleSettingsView, particlePreviewView);
			//particleListView.customParticleChanged += particleSettingsView.OnParticleWasChanged;
			//particleListView.customParticleChanged += particlePreviewView.OnParticleWasChanged;
		}

		protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
		{
			//particleListView.customParticleChanged -= particleSettingsView.OnParticleWasChanged;
			//particleListView.customParticleChanged -= particlePreviewView.OnParticleWasChanged;
			base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			mainFlow.DismissFlowCoordinator(this, null);
		}
	}
}
