using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace CustomParticles.UI.Managers
{
	internal class MenuButtonManager : IInitializable, IDisposable
	{
		private readonly MenuButton menuButton;
		private readonly MainFlowCoordinator mainFlowCoordinator;
		private readonly ParticleFlowCoordinator particleFlowCoordinator;

		public MenuButtonManager(MainFlowCoordinator mainFlowCoordinator, ParticleFlowCoordinator notesFlowCoordinator)
		{
			this.mainFlowCoordinator = mainFlowCoordinator;
			this.particleFlowCoordinator = notesFlowCoordinator;
			this.menuButton = new MenuButton("Custom Particles", "Change Custom Particles Here!", ShowCustomParticlesFlow, true);
		}

		public void Initialize()
		{
			MenuButtons.instance.RegisterButton(menuButton);
		}

		public void Dispose()
		{
			if (MenuButtons.IsSingletonAvailable)
			{
				MenuButtons.instance.UnregisterButton(menuButton);
			}
		}

		private void ShowCustomParticlesFlow()
		{
			mainFlowCoordinator.PresentFlowCoordinator(particleFlowCoordinator);
		}
	}
}
