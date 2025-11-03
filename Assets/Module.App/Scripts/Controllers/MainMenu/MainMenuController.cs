using System;
using System.Threading.Tasks;
using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Services;
using Module.Common.Scripts;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Module.App.Scripts.Controllers.Leaderboard
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
            Debug.Log("ININ");
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