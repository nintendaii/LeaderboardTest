using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Controllers.Leaderboard;
using Module.App.Scripts.Services;
using Module.Core.Scripts.Launcher;
using Zenject;

namespace Module.App.Scripts.Launcher
{
    public class LauncherInstallerApp: LauncherInstaller
    {
        protected override void InstallComponents()
        {
            base.InstallComponents();
            
            Container.Bind<MainMenuController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }

        protected override void InstallServices()
        {
            base.InstallServices();
            Container.Bind<LeaderboardService>().AsSingle();
        }

        protected override void InstallCommandSignals()
        {
            base.InstallCommandSignals();
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SignalOpenPopup>();
            Container.BindSignal<SignalOpenPopup>().ToMethod<CommandOpenPopup>(command => command.Execute).FromNew();
            Container.DeclareSignal<SignalClosePopup>();
            Container.BindSignal<SignalClosePopup>().ToMethod<CommandClosePopup>(command => command.Execute).FromNew();
        }
    }
}