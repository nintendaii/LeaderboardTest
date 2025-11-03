using System;
using Module.App.Scripts.Data;
using Module.Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Module.App.Scripts.Controllers.Leaderboard.Record
{
    [Serializable]
    public class UnitLeaderboardRecordModel : ModelBase
    {
        public LeaderboardRecordData LeaderboardRecordData;
    }

    [Serializable]
    public class UnitLeaderboardRecordView : ViewBase
    {
        [SerializeField] public Image avatarImage;
        [SerializeField] public TMP_Text playerName;
        [SerializeField] public TMP_Text playerScore;
        [SerializeField] public Image recordBackground;

        public void SetUp(LeaderboardRecordData data)
        {
            playerName.text = data.name;
            playerScore.text = data.score.ToString();
            recordBackground.color = Utils.Utils.GetColorByRank(data.type);
            playerName.fontSize *= Utils.Utils.GetScaleByRank(data.type);
            playerScore.fontSize *= Utils.Utils.GetScaleByRank(data.type);
        }
    }
    
    public class UnitLeaderboardRecordController: ComponentControllerBase<UnitLeaderboardRecordModel, UnitLeaderboardRecordView>
    {
        public void SetUp(LeaderboardRecordData leaderboardRecordData)
        {
            Model.LeaderboardRecordData = leaderboardRecordData;
            View.SetUp(leaderboardRecordData);
        }
    }
    
    public class UnitLeaderboardRecordFactory : PlaceholderFactory<UnitLeaderboardRecordController>
    {
    }
}