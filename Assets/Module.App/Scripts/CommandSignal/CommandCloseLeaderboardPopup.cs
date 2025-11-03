using Module.App.Scripts.Factory;
using Module.Common.Scripts;
using Module.Core.CommandSignal;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandCloseLeaderboardPopup: ICommand
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LeaderboardRecordPooledFactory _leaderboardRecordPooledFactory;
        
        public void Execute()
        {
            _leaderboardRecordPooledFactory.ReleaseContainer();
            _signalBus.Fire(new SignalClosePopup(GlobalConstants.Addressable.LEADERBOARD_ADDRESSABLE_PATH));
        }
    }
}