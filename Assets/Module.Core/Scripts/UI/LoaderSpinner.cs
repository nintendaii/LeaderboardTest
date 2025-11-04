using Module.Core.MVC;

namespace Module.App.Scripts.UI
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class LoaderSpinner : MonoBehaviour, IHideComponent, IShowComponent
    {
        [Header("Spin Settings")]
        [SerializeField] private float spinDuration = 1f;
        [SerializeField] private int loops = -1; // -1 = infinite
        [SerializeField] private Ease easeType = Ease.Linear;
        [SerializeField] private RectTransform _targetRectTransform;
        private Tween _spinTween;

        /// <summary>
        /// Starts spinning the loader
        /// </summary>
        public void StartLoading()
        {
            ShowComponent();
            StopTwin();

            _spinTween = _targetRectTransform
                .DORotate(new Vector3(0, 0, 360), spinDuration, RotateMode.FastBeyond360)
                .SetLoops(loops, LoopType.Restart)
                .SetEase(easeType)
                .Play();
        }

        /// <summary>
        /// Stops spinning and hides the loader
        /// </summary>
        public void FinishLoading()
        {
            StopTwin();
            HideComponent();
        }

        private void StopTwin()
        {
            _spinTween?.Kill();
            _spinTween = null;
        }

        private void OnDestroy()
        {
            StopTwin();
        }

        public void HideComponent()
        {
            gameObject.SetActive(false);
        }

        public void ShowComponent()
        {
            gameObject.SetActive(true);
        }
    }
}