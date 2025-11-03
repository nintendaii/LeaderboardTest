using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Controllers.Leaderboard;
using Module.App.Scripts.Controllers.Leaderboard.Record;
using Module.App.Scripts.Factory;
using Module.App.Scripts.Services;
using Module.Core.Scripts.Launcher;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.Launcher
{
    public class LauncherInstallerApp: LauncherInstaller
    {
        [SerializeField] private UnitLeaderboardRecordController _unitLeaderboardRecordController;
        protected override void InstallComponents()
        {
            base.InstallComponents();
            
            Container.Bind<MainMenuController>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<LeaderboardRecordFactoryContainer>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }

        protected override void InstallServices()
        {
            base.InstallServices();
            Container.Bind<LeaderboardService>().AsSingle();
        }


        protected override void InstallFactory()
        {
            base.InstallFactory();
            Container.BindFactory<UnitLeaderboardRecordController, UnitLeaderboardRecordFactory>()
                .FromComponentInNewPrefab(_unitLeaderboardRecordController);
        }

        protected override void InstallCommandSignals()
        {
            base.InstallCommandSignals();
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SignalOpenPopup>();
            Container.BindSignal<SignalOpenPopup>().ToMethod<CommandOpenPopup>(command => command.Execute).FromNew();
            Container.DeclareSignal<SignalClosePopup>();
            Container.BindSignal<SignalClosePopup>().ToMethod<CommandClosePopup>(command => command.Execute).FromNew();
            Container.DeclareSignal<SignalOpenLeaderboardPopup>();
            Container.BindSignal<SignalOpenLeaderboardPopup>().ToMethod<CommandOpenLeaderboardPopup>(command => command.Execute).FromNew();
            Container.DeclareSignal<SignalInitLeaderboard>();
            Container.BindSignal<SignalInitLeaderboard>().ToMethod<CommandInitLeaderboard>(command => command.Execute).FromNew();
        }
    }
}