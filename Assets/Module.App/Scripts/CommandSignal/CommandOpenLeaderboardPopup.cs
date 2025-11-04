using System.Collections.Generic;
using Module.App.Scripts.Data;
using Module.App.Scripts.Factory;
using Module.App.Scripts.Services;
using Module.Common.Scripts;
using Module.Core.Scripts.CommandSignal;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenLeaderboardPopup: ICommand
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private LeaderboardDataService _leaderboardDataService;
        [Inject] private LeaderboardRecordPooledFactory _leaderboardRecordPooledFactory;
        
        public async void Execute()
        {
            var data = await _leaderboardDataService.LoadAsync();
            SortLeaderboard(data);
            _signalBus.Fire(new SignalOpenPopup(GlobalConstants.Addressable.LEADERBOARD_ADDRESSABLE_PATH, data));
        }
        
        /// <summary>
        /// Sorts the list by score value. The sort algorithm has O(n log n) complexity which is the best possible complexity for sorting  
        /// </summary>
        /// <param name="records">List of loaded records from JSON</param>
        private void SortLeaderboard(List<LeaderboardRecordData> records)
        {
            if (records == null || records.Count <= 1) return;

            records.Sort((a, b) => b.score.CompareTo(a.score)); //O(n log n) complexity
        }
    }
}