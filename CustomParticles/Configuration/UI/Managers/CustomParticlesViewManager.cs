using BeatSaberMarkupLanguage.MenuButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace CustomParticles.Configuration.UI.Managers
{
	internal class CustomParticlesViewManager : IInitializable, IDisposable
	{
		//private CustomParticlesMainViewController particleListViewController;
		private GameplaySetupViewController gameplaySetupViewController;

		public CustomParticlesViewManager(/*CustomParticlesMainViewController particleListViewController,*/ GameplaySetupViewController gameplaySetupViewController)
		{
			//this.particleListViewController = particleListViewController;
			this.gameplaySetupViewController = gameplaySetupViewController;
		}

		public void Initialize()
		{
			//particleListViewController.customParticlesReloaded += NoteListViewController_CustomNotesReloaded;
			gameplaySetupViewController.didActivateEvent += GameplaySetupViewController_DidActivateEvent;
		}

		public void Dispose()
		{
			//particleListViewController.customParticlesReloaded -= NoteListViewController_CustomNotesReloaded;
			gameplaySetupViewController.didActivateEvent -= GameplaySetupViewController_DidActivateEvent;
		}

		private void NoteListViewController_CustomNotesReloaded()
		{
			//particleListViewController.SetupList();
		}

		private void GameplaySetupViewController_DidActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			//particleListViewController.ParentControllerActivated();
		}
	}
}
