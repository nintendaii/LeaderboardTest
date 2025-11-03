using Module.App.Scripts.Controllers.Leaderboard;
using Module.App.Scripts.Factory;
using Module.App.Scripts.Services;
using Module.Core.CommandSignal;
using SimplePopupManager;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenPopup: ICommandWithParameter
    {
        [Inject] private readonly IPopupManagerService _popupManagerService;
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly LeaderboardService _leaderboardService;
        [Inject] private readonly LeaderboardRecordFactoryContainer _leaderboardRecordFactory;
        
        private LeaderboardController _leaderboardController;
        public async void Execute(ISignal signal)
        {
            var param = (SignalOpenPopup)signal;
            var go = await _popupManagerService.OpenPopup(param.PopupName, param.Parameters);
            _diContainer.InjectGameObject(go);
            _leaderboardController = go.GetComponent<LeaderboardController>();
            var data = await _leaderboardService.LoadAsync();
            foreach (var d in data)
            {
                var g = _leaderboardRecordFactory.CreateRecord(d, _leaderboardController.GetSpawnParent());
                g.SetUp(d);
            }
        }
    }
}