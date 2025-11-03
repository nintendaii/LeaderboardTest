using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Data;
using Module.App.Scripts.Services;
using Module.Common.Scripts;
using Module.Core.MVC;
using SimplePopupManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        [SerializeField] public Button loadButton;
    }

    public class LeaderboardController : ComponentControllerBase<LeaderboardModel, LeaderboardView>, IPopupInitialization
    {
        public override void Initialize()
        {
            Debug.Log("Initialized");
            base.Initialize();
            View.closeButton.onClick.AddListener(ViewOnOnCloseEvent);
        }
        public Transform GetSpawnParent() => View.contentParent;
        
        private void ViewOnOnCloseEvent()
        {
            Debug.Log("InCloseEvent");
            SignalBus.Fire(new SignalClosePopup(GlobalConstants.Addressable.LEADERBOARD_ADDRESSABLE_PATH));
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