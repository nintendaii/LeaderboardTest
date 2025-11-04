using System;
using Module.App.Scripts.Data;
using Module.Core.Scripts.Factory;
using Module.Core.Scripts.MVC;
using Module.Core.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.App.Scripts.Controllers.Leaderboard.Record
{
    [Serializable]
    public class UnitLeaderboardRecordModel : ModelBase
    {
        public LeaderboardRecordData LeaderboardRecordData;
        public float DefaultScoreFontSize;
        public float DefaultNameFontSize;
        public float DefaultRankFontSize;
    }

    [Serializable]
    public class UnitLeaderboardRecordView : ViewBase
    {
        [SerializeField] public RawImage avatarRawImage;
        [SerializeField] public TMP_Text playerName;
        [SerializeField] public TMP_Text playerScore;
        [SerializeField] public TMP_Text playerRank;
        [SerializeField] public Image recordBackground;
        [SerializeField] public LoaderSpinner loaderSpinner;

        public void SetUp(LeaderboardRecordData data)
        {
            playerName.text = data.name;
            playerScore.text = data.score.ToString();
            playerRank.text = data.type;
            recordBackground.color = Utils.Utils.GetColorByRank(data.type);
            playerName.fontSize *= Utils.Utils.GetScaleByRank(data.type);
            playerScore.fontSize *= Utils.Utils.GetScaleByRank(data.type);
            playerRank.fontSize *= Utils.Utils.GetScaleByRank(data.type);
        }

        public void Reset(float defaultScoreSize, float defaultNameSize, float defaultRankFontSize)
        {
            playerName.text = string.Empty;
            playerScore.text = string.Empty;
            playerRank.text = string.Empty;
            recordBackground.color = Color.white;
            playerName.fontSize = defaultNameSize;
            playerScore.fontSize = defaultScoreSize;
            playerRank.fontSize = defaultRankFontSize;
        }

        public void ToggleAvatarLoading(bool isLoading)
        {
            if (isLoading)
            {
                loaderSpinner.StartLoading();
            }
            else
            {
                loaderSpinner.FinishLoading();
            }
            avatarRawImage.enabled = !isLoading;
        }
    }
    
    public class LeaderboardRecordController: ComponentControllerBase<UnitLeaderboardRecordModel, UnitLeaderboardRecordView>, IFactoryUnitResettable, IFactoryUnitInitializable<LeaderboardRecordData>
    {
        public void UnitInitialize(LeaderboardRecordData leaderboardRecordData)
        {
            Model.LeaderboardRecordData = leaderboardRecordData;
            Model.DefaultScoreFontSize = View.playerScore.fontSize;
            Model.DefaultNameFontSize = View.playerName.fontSize;
            Model.DefaultRankFontSize = View.playerRank.fontSize;
            View.SetUp(leaderboardRecordData);
            View.ToggleAvatarLoading(true);
        }

        public void UnitReset()
        {
            View.Reset(Model.DefaultScoreFontSize, Model.DefaultNameFontSize, Model.DefaultRankFontSize);
        }

        public void SetAvatar(Texture2D avatarTexture2D)
        {
            View.ToggleAvatarLoading(false);
            View.avatarRawImage.texture = avatarTexture2D;
        }
    }
}