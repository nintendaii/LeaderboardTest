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
        [Inject] private readonly LeaderboardService _service;

        public override void Initialize()
        {
            Debug.Log("Initialized");
            base.Initialize();
            View.closeButton.onClick.AddListener(ViewOnOnCloseEvent);
            //View.loadButton.onClick.AddListener(ViewOnOnLoadLeaderboardEvent);
        }

        private async void ViewOnOnLoadLeaderboardEvent()
        {
            await LoadAndShowAsync();
        }

        public Transform GetSpawnParent() => View.contentParent;
        
        private void ViewOnOnCloseEvent()
        {
            Debug.Log("InCloseEvent");
            SignalBus.Fire(new SignalClosePopup(GlobalConstants.Addressable.LEADERBOARD_ADDRESSABLE_PATH));
        }

        private async Task LoadAndShowAsync()
        {
            var entries = await _service.LoadAsync();
            foreach (var e in entries)
            {
                Debug.Log($"{e.name} {e.score} {e.type} {e.avatar}");
            }
            Model.Entries.Clear();
            Model.Entries.AddRange(entries);
            //View.Refresh(Model);
            ShowComponent();
        }

        public override void Dispose()
        {
            Debug.Log("Dispose");
            base.Dispose();
            View.closeButton.onClick.RemoveListener(ViewOnOnCloseEvent);
            //View.loadButton.onClick.RemoveListener(ViewOnOnLoadLeaderboardEvent);
        }

        public Task Init(object param)
        {
            return Task.Delay(1);
            //throw new NotImplementedException();
        }
    }
}