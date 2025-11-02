using UnityEngine;
using Zenject;

namespace Module.Core.Scripts.MVC
{
    public abstract class ControllerBase<TModel, TView> : IInitializable, System.IDisposable
        where TModel : ModelBase
        where TView : ViewBase
    {
        protected TModel Model { get; }
        protected TView View { get; }
        protected SignalBus SignalBus { get; }
        protected DiContainer Container { get; }

        protected ControllerBase(TModel model, TView view, SignalBus signalBus, DiContainer container)
        {
            Model = model;
            View = view;
            SignalBus = signalBus;
            Container = container;
        }

        public virtual void Initialize() { }
        public virtual void Dispose() { }

        protected void Show() => View.gameObject.SetActive(true);
        protected void Hide() => View.gameObject.SetActive(false);
        protected void SetInteractable(bool value)
        {
            if (View.TryGetComponent<CanvasGroup>(out var cg))
                cg.interactable = cg.blocksRaycasts = value;
        }
    }
}