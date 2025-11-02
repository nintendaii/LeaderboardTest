using System;
using Module.Core.Scripts.MVC;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    public class MainMenuView : ViewBase
    {
        [SerializeField] private Button showLeaderboardButton;
        
        public event Action OnShowLeaderboardEvent;

        private void OnEnable()
        {
            showLeaderboardButton.onClick.AddListener(ShowLeaderboardPopup);
        }

        private void OnDisable()
        {
            showLeaderboardButton.onClick.RemoveListener(ShowLeaderboardPopup);
        }

        private void ShowLeaderboardPopup()
        {
            OnShowLeaderboardEvent?.Invoke();
        }
    }
}