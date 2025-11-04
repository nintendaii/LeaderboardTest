using System;
using Module.App.Scripts.CommandSignal;
using Module.Core.Scripts.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace Module.App.Scripts.Controllers.MainMenu
{
    [Serializable]
    public class MainMenuView : ViewBase
    {
        [SerializeField] public Button showLeaderboardButton;
    }
    public class MainMenuController : ComponentControllerBase<ModelBase, MainMenuView>
    {
        public override void Initialize()
        {
            base.Initialize();
            View.showLeaderboardButton.onClick.AddListener(ViewOnOnShowLeaderboardEvent);
        }

        private void ViewOnOnShowLeaderboardEvent()
        {
            SignalBus.Fire(new SignalOpenLeaderboardPopup());
        }

        public override void Dispose()
        {
            View.showLeaderboardButton.onClick.RemoveListener(ViewOnOnShowLeaderboardEvent);
            base.Dispose();
        }
    }
}