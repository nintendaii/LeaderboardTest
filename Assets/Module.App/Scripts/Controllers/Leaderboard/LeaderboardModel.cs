using System.Collections.Generic;
using Module.Core.MVC;
using Module.Core.Scripts.MVC;
using UnityEngine;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    [System.Serializable]
    public class LeaderboardModel : ModelBase
    {
        public List<LeaderboardEntry> Entries { get; } = new();
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public int score;
        public string avatar;
        public string type;

        public Color GetColor() => type switch
        {
            "Diamond" => new Color(0.18f, 0.8f, 0.94f),
            "Gold"    => new Color(1f, 0.84f, 0f),
            "Silver"  => new Color(0.75f, 0.75f, 0.75f),
            "Bronze"  => new Color(0.8f, 0.5f, 0.3f),
            _         => Color.white
        };

        public float GetScale() => type switch
        {
            "Diamond" => 1.3f,
            "Gold"    => 1.2f,
            "Silver"  => 1.1f,
            "Bronze"  => 1.05f,
            _         => 1.0f
        };
    }
}