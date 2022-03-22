using CustomParticles.Configuration;
using CustomParticles.Configuration.UI;
using CustomParticles.Configuration.UI.Managers;
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
			Container.BindInterfacesAndSelfTo<CustomParticleConfigManager>().AsSingle();
			Container.Bind<CustomParticlesInfoController>().FromNewComponentAsViewController().AsSingle();
			Container.Bind<ParticlePreviewViewController>().FromNewComponentAsViewController().AsSingle();
			Container.Bind<CustomParticlesMainViewController>().FromNewComponentAsViewController().AsSingle();
			Container.Bind<ParticleFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
			Container.BindInterfacesTo<CustomParticlesViewManager>().AsSingle();
			Container.BindInterfacesTo<MenuButtonManager>().AsSingle();

		}
	}
}
