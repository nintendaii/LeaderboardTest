using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.App.Scripts.Controllers.Leaderboard;
using Module.Common.Scripts;
using UnityEngine;

namespace Module.App.Scripts.Services
{
    public class LeaderboardService
    {
        public async Task<List<LeaderboardEntry>> LoadAsync()
        {
            var textAsset = Resources.Load<TextAsset>(GlobalConstants.Resources.LEADERBOARD_JSON_PATH);
            if (!textAsset)
            {
                Debug.LogError($"Leaderboard JSON not found: {GlobalConstants.Resources.LEADERBOARD_JSON_PATH}");
                return new List<LeaderboardEntry>();
            }

            await Task.Yield();

            try
            {
                var wrapper = JsonUtility.FromJson<Wrapper>(textAsset.text);
                return wrapper?.leaderboard ?? new List<LeaderboardEntry>();
            }
            catch (Exception e)
            {
                Debug.LogError($"JSON Parse Error: {e.Message}\nJSON: {textAsset.text}");
                return new List<LeaderboardEntry>();
            }
        }

        [Serializable]
        private class Wrapper
        {
            public List<LeaderboardEntry> leaderboard;
        }
    }
}