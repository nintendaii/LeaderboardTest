using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Data;
using Module.Core.Scripts.MVC;
using Module.PopupService.Scripts.PopupInitialization;
using UnityEngine;
using UnityEngine.UI;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    [Serializable]
    public class LeaderboardModel : ModelBase
    {
        public List<LeaderboardRecordData> Entries { get; } = new();
    }
    
    [Serializable]
    public class LeaderboardView : ViewBase
    {
        [SerializeField] public Transform contentParent;
        [SerializeField] public Button closeButton;
    }

    public class LeaderboardController : ComponentControllerBase<LeaderboardModel, LeaderboardView>, IPopupInitialization
    {
        public override void Initialize()
        {
            base.Initialize();
            View.closeButton.onClick.AddListener(ViewOnOnCloseEvent);
        }
        
        private void ViewOnOnCloseEvent()
        {
            SignalBus.Fire(new SignalCloseLeaderboardPopup());
        }

        public override void Dispose()
        {
            base.Dispose();
            View.closeButton.onClick.RemoveListener(ViewOnOnCloseEvent);
        }

        public Task Init(object param)
        {
            SignalBus.Fire(new SignalInitLeaderboard(param, View.contentParent));
            return Task.CompletedTask;
        }
    }
}