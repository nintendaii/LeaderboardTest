using Module.App.Scripts.Controllers.Leaderboard;
using Module.App.Scripts.Factory;
using Module.App.Scripts.Services;
using Module.Core.CommandSignal;
using SimplePopupManager;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenPopup : ICommandWithParameter
    {
        [Inject] private IPopupManagerService _popupManagerService;
        [Inject] private LeaderboardService _leaderboardService;
        [Inject] private LeaderboardRecordFactoryContainer _leaderboardRecordFactory;
    
        private LeaderboardController _leaderboardController;

        public async void Execute(ISignal signal)
        {
            var param = (SignalOpenPopup)signal;
            var popup = await _popupManagerService.OpenPopup(param.PopupName, param.Parameters);
            _leaderboardController = popup.GetComponent<LeaderboardController>();

            var data = await _leaderboardService.LoadAsync();
            foreach (var d in data)
            {
                var record = _leaderboardRecordFactory.CreateRecord(d, _leaderboardController.GetSpawnParent());
                record.SetUp(d);
            }
        }
    }

}