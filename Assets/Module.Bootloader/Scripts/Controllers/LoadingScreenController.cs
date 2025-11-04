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

        public void Reset()
        {
            canvasGroup.alpha = 1f;
        }

        public async Task Fade(float duration, Action onComplete = null)
        {
            if (canvasGroup == null)
            {
                Debug.LogError("CanvasGroup is null!");
                onComplete?.Invoke();
                return;
            }

            var tween = canvasGroup.DOFade(0f, duration)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });

            while (tween.IsActive() && !tween.IsComplete())
                await Task.Yield();

            if (tween.IsComplete())
                onComplete?.Invoke();
        }
    }
    public class LoadingScreenController: ComponentControllerBase<ModelBase, LoadingScreenControllerView>
    {
        public override void Initialize()
        {
            base.Initialize();
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