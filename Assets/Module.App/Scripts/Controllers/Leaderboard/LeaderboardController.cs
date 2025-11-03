using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Data;
using Module.Common.Scripts;
using Module.Core.MVC;
using SimplePopupManager;
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
            Debug.Log("Initialized");
            base.Initialize();
            View.closeButton.onClick.AddListener(ViewOnOnCloseEvent);
        }
        
        private void ViewOnOnCloseEvent()
        {
            Debug.Log("InCloseEvent");
            SignalBus.Fire(new SignalCloseLeaderboardPopup());
        }

        public override void Dispose()
        {
            Debug.Log("Dispose");
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