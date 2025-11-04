using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.App.Scripts.Controllers.Leaderboard.Record;
using Module.App.Scripts.Data;
using Module.App.Scripts.Factory;
using Module.App.Scripts.Services;
using Module.Core.CommandSignal;
using UnityEngine;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandInitLeaderboard: ICommandWithParameter
    {
        [Inject] private readonly LeaderboardRecordPooledFactory _leaderboardRecordPooledFactory;
        [Inject] private readonly IAvatarCacheService _avatarCacheService;

        private Dictionary<LeaderboardRecordData, LeaderboardRecordController> _recordDataDictionary = new();
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
                _recordDataDictionary[d] = record;
                
            }

            LoadAvatars();
        }

        private async void LoadAvatars()
        {
            try
            {
                foreach (var kvp in _recordDataDictionary)
                {
                    var texture = await _avatarCacheService.GetAvatarAsync(kvp.Key.avatar);
                    kvp.Value.SetAvatar(texture);
                }

            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            finally
            {
                _recordDataDictionary.Clear();
            }
        }
    }
}