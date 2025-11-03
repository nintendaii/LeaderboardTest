using System;
using Module.App.Scripts.Data;
using Module.Common.Scripts;
using UnityEngine;

namespace Module.App.Scripts.Controllers.Leaderboard.Utils
{
    public static class Utils
    {
        public static Color GetColorByRank(PlayerRank rank)
        {
            return rank switch
            {
                PlayerRank.Default => GlobalConstants.Record.RANK_DEFAULT_COLOR,
                PlayerRank.Bronze => GlobalConstants.Record.RANK_BRONZE_COLOR,
                PlayerRank.Silver => GlobalConstants.Record.RANK_SILVER_COLOR,
                PlayerRank.Gold => GlobalConstants.Record.RANK_GOLD_COLOR,
                PlayerRank.Diamond => GlobalConstants.Record.RANK_DIAMON_COLOR,
                _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
            };
        }
        
        public static Color GetColorByRank(string rankName)
        {
            var rankType = GetRankByName(rankName);
            return GetColorByRank(rankType);
        }

        public static float GetScaleByRank(PlayerRank rank)
        {
            return rank switch
            {
                PlayerRank.Default => GlobalConstants.Record.RANK_DEFAULT_SIZE,
                PlayerRank.Bronze => GlobalConstants.Record.RANK_BRONZE_SIZE,
                PlayerRank.Silver => GlobalConstants.Record.RANK_SILVER_SIZW,
                PlayerRank.Gold => GlobalConstants.Record.RANK_GOLDEN_SIZE,
                PlayerRank.Diamond => GlobalConstants.Record.RANK_DIAMOND_SIZE,
                _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
            };
        }

        public static float GetScaleByRank(string rankName)
        {
            var rankType = GetRankByName(rankName);
            return GetScaleByRank(rankType);
        }
        
        public static PlayerRank GetRankByName(string rankName)
        {
            return rankName switch
            {
                "Default" => PlayerRank.Default,
                "Bronze" => PlayerRank.Bronze,
                "Silver" => PlayerRank.Silver,
                "Gold" => PlayerRank.Gold,
                "Diamond" => PlayerRank.Diamond,
                _ => throw new ArgumentOutOfRangeException(nameof(rankName), rankName, null)
            };
        }
    }
}