using CustomParticles.UI;
using CustomParticles.UI.Managers;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace CustomParticles.Installers
{
	class CPMenuInstaller : Installer
	{
		private SiraLog _siraLog;

		internal CPMenuInstaller(SiraLog siraLog)
		{
			_siraLog = siraLog;
		}

		public override void InstallBindings()
		{
			_siraLog.Info("Installing menu containers..");
			Container.Bind<ParticlePreviewViewController>().FromNewComponentAsViewController().AsSingle();
			Container.Bind<ParticleSettingsViewController>().FromNewComponentAsViewController().AsSingle();
			Container.Bind<ParticleListViewController>().FromNewComponentAsViewController().AsSingle();
			Container.Bind<ParticleFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
			Container.BindInterfacesTo<CustomParticlesViewManager>().AsSingle();
			Container.BindInterfacesTo<MenuButtonManager>().AsSingle();

		}
	}
}
