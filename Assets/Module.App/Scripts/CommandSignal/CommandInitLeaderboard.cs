using System.Collections.Generic;
using Module.App.Scripts.Data;
using Module.App.Scripts.Factory;
using Module.Core.CommandSignal;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandInitLeaderboard: ICommandWithParameter
    {
        [Inject] private LeaderboardRecordPooledFactory _leaderboardRecordPooledFactory;
        
        public void Execute(ISignal signal)
        {
            var param = (SignalInitLeaderboard) signal;
            var data = param.Parameters as List<LeaderboardRecordData>;
            if (data is null)
            {
                Debug.LogError("Data is null");
                return;
            }
            foreach (var d in data)
            {
                var record = _leaderboardRecordPooledFactory.CreateRecord(d, param.ParentTransform);
            }
        }
    }
}