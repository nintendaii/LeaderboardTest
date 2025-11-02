using System.Threading.Tasks;
using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Services;
using Module.Common.Scripts;
using Module.Core.Scripts.MVC;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    public class MainMenuController : ControllerBase<MainMenuModel, MainMenuView>
    {
        private SignalBus _signalBus;

        public MainMenuController(
            MainMenuModel model,
            MainMenuView view,
            SignalBus signalBus,
            DiContainer container)
            : base(model, view, signalBus, container)
        {
            _signalBus = signalBus;
        }

        public override void Initialize()
        {
            base.Initialize();
            View.OnShowLeaderboardEvent += ViewOnOnShowLeaderboardEvent;
        }

        private void ViewOnOnShowLeaderboardEvent()
        {
            _signalBus.Fire(new SignalOpenPopup(GlobalConstants.Addressable.LEADERBOARD_ADDRESSABLE_PATH, null));
        }

        public override void Dispose()
        {
            View.OnShowLeaderboardEvent -= ViewOnOnShowLeaderboardEvent;
            base.Dispose();
        }
    }
}