using System;
using Module.Core.MVC;
using Module.Core.Scripts.MVC;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    public class LeaderboardView : ViewBase
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private GameObject entryPrefab;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button loadButton;
        
        public event Action OnCloseEvent;
        public event Action OnLoadLeaderboardEvent;

        private void OnEnable()
        {
            loadButton.onClick.AddListener(LoadLeaderboard);
            closeButton.onClick.AddListener(ClosePopup);
        }

        private void OnDisable()
        {
            loadButton.onClick.RemoveListener(LoadLeaderboard);
            closeButton.onClick.RemoveListener(ClosePopup);
        }

        private void ClosePopup()
        {
            Debug.Log("ClosePopupInvoking");
            OnCloseEvent?.Invoke();
        }
        
        private void LoadLeaderboard()
        {
            OnLoadLeaderboardEvent?.Invoke();
        }
    }
}