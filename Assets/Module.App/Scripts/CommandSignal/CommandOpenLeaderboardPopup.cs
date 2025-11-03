using Module.App.Scripts.Factory;
using Module.App.Scripts.Services;
using Module.Common.Scripts;
using Module.Core.CommandSignal;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenLeaderboardPopup: ICommand
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private LeaderboardService _leaderboardService;
        [Inject] private LeaderboardRecordPooledFactory _leaderboardRecordPooledFactory;
        
        public async void Execute()
        {
            var data = await _leaderboardService.LoadAsync();
            _signalBus.Fire(new SignalOpenPopup(GlobalConstants.Addressable.LEADERBOARD_ADDRESSABLE_PATH, data));
        }
    }
}