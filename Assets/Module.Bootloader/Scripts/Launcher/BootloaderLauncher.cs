using Module.Bootloader.Scripts.CommandSignal;
using Module.Bootloader.Scripts.Controllers;
using Module.Bootloader.Scripts.Services;
using Module.Core.Scripts.Launcher;
using Zenject;

namespace Module.Bootloader.Scripts.Launcher
{
    public class BootloaderLauncher: LauncherInstaller
    {
        protected override void InstallComponents()
        {
            base.InstallComponents();
            Container.Bind<LoadingScreenController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }

        protected override void InstallServices()
        {
            base.InstallServices();
            Container.Bind<BootloaderService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }

        protected override void InstallCommandSignals()
        {
            base.InstallCommandSignals();
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SignalToggleLoadingScreen>();
            Container.BindSignal<SignalToggleLoadingScreen>().ToMethod<CommandToggleLoadingScreen>(command => command.Execute).FromNew();
            Container.DeclareSignal<SignalFinishedInitialization>();
            Container.BindSignal<SignalFinishedInitialization>().ToMethod<CommandFinishedInitialization>(command => command.Execute).FromNew();
        }
    }
}