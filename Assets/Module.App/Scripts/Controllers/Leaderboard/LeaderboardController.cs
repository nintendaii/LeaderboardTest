using System.Threading.Tasks;
using Module.App.Scripts.CommandSignal;
using Module.App.Scripts.Services;
using Module.Common.Scripts;
using Module.Core.Scripts.MVC;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    public class LeaderboardController : ControllerBase<LeaderboardModel, LeaderboardView>
    {
        [Inject] private readonly LeaderboardService _service;

        public LeaderboardController(
            LeaderboardModel model,
            LeaderboardView view,
            SignalBus signalBus,
            DiContainer container)
            : base(model, view, signalBus, container)
        {
            Debug.Log($"InConstructor - {view.name}");
        }

        public override void Initialize()
        {
            Debug.Log("Initialized");
            base.Initialize();
            View.OnCloseEvent += ViewOnOnCloseEvent;
            View.OnLoadLeaderboardEvent += ViewOnOnLoadLeaderboardEvent;
        }

        private async void ViewOnOnLoadLeaderboardEvent()
        {
            await LoadAndShowAsync();
        }
        
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
            Show();
        }

        public override void Dispose()
        {
            Debug.Log("Dispose");
            base.Dispose();
            View.OnCloseEvent -= ViewOnOnCloseEvent;
            View.OnLoadLeaderboardEvent -= ViewOnOnLoadLeaderboardEvent;
        }
    }
}