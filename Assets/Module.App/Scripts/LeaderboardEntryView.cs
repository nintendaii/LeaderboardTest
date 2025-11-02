using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text rankText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private RawImage avatarImage;
        [SerializeField] private Image background;

        public void Setup(LeaderboardEntry entry, int rank)
        {
            rankText.text = $"#{rank + 1}";
            nameText.text = entry.name;
            scoreText.text = entry.score.ToString();
            background.color = entry.GetColor();
            transform.localScale = Vector3.one * entry.GetScale();

            //TODO Avatar loading (async via service)
            //StartCoroutine(AvatarLoader.Load(entry.avatar, avatarImage));
        }
    }
}