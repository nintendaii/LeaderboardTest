using System;
using Module.App.Scripts.Data;
using Module.App.Scripts.Factory;
using Module.App.Scripts.UI;
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
        public float DefaultScoreFontSize;
        public float DefaultNameFontSize;
    }

    [Serializable]
    public class UnitLeaderboardRecordView : ViewBase
    {
        [SerializeField] public RawImage avatarRawImage;
        [SerializeField] public TMP_Text playerName;
        [SerializeField] public TMP_Text playerScore;
        [SerializeField] public Image recordBackground;
        [SerializeField] public LoaderSpinner loaderSpinner;

        public void SetUp(LeaderboardRecordData data)
        {
            playerName.text = data.name;
            playerScore.text = data.score.ToString();
            recordBackground.color = Utils.Utils.GetColorByRank(data.type);
            playerName.fontSize *= Utils.Utils.GetScaleByRank(data.type);
            playerScore.fontSize *= Utils.Utils.GetScaleByRank(data.type);
        }

        public void Reset(float defaultScoreSize, float defaultNameSize)
        {
            playerName.text = string.Empty;
            playerScore.text = string.Empty;
            recordBackground.color = Color.white;
            playerName.fontSize = defaultNameSize;
            playerScore.fontSize = defaultScoreSize;
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
        public void InitializeUnit(LeaderboardRecordData leaderboardRecordData)
        {
            Model.LeaderboardRecordData = leaderboardRecordData;
            Model.DefaultScoreFontSize = View.playerScore.fontSize;
            Model.DefaultNameFontSize = View.playerName.fontSize;
            View.SetUp(leaderboardRecordData);
            View.ToggleAvatarLoading(true);
        }

        public void ResetUnit()
        {
            View.Reset(Model.DefaultScoreFontSize, Model.DefaultNameFontSize);
        }

        public void SetAvatar(Texture2D avatarTexture2D)
        {
            View.ToggleAvatarLoading(false);
            View.avatarRawImage.texture = avatarTexture2D;
        }
    }
    
    public class UnitLeaderboardRecordFactory : PlaceholderFactory<LeaderboardRecordController>
    {
    }
}