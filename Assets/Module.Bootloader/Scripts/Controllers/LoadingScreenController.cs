using System;
using System.Threading.Tasks;
using DG.Tweening;
using Module.Core.Scripts.MVC;
using Module.Core.Scripts.UI;
using UnityEngine;

namespace Module.Bootloader.Scripts.Controllers
{
    [Serializable]
    public class LoadingScreenControllerView : ViewBase
    {
        [SerializeField] public LoaderSpinner spinner;
        [SerializeField] public CanvasGroup canvasGroup;

        private Tween _fadeTween;
        public void Reset()
        {
            canvasGroup.alpha = 1f;
        }

        public async Task Fade(float duration)
        {
            if (canvasGroup == null)
            {
                Debug.LogError("CanvasGroup is null!");
                return;
            }

            _fadeTween?.Kill();
            _fadeTween = null;

            _fadeTween = canvasGroup.DOFade(0f, duration)
                .SetAutoKill(false);

            while (_fadeTween.IsActive() && !_fadeTween.IsComplete())
                await Task.Yield();

            _fadeTween?.Kill();
            _fadeTween = null;
        }
    }
    public class LoadingScreenController: ComponentControllerBase<ModelBase, LoadingScreenControllerView>
    {
        private void Start()
        {
            StartSpin();
        }

        private void StartSpin()
        {
            View.Reset();
            View.spinner.StartLoading();
        }

        public async Task FadeOut(float duration = 0.4f)
        {
            await View.Fade(duration);
            View.spinner.FinishLoading();
        }
    }
}