using System;
using Module.Core.Utilities;
using UnityEngine;

namespace Module.Core.MVC {
    public abstract class ComponentControllerBase : ComponentControllerBase<ModelBase, ViewBase> { }
    
    public abstract class ComponentControllerBase<TModel, TView> : ControllerMonoBase where TModel : ModelBase where TView : ViewBase {
        [SerializeField] private TModel model = default;
        [SerializeField] private TView view = default;
        
        protected TModel Model => model;
        protected TView View => view;

        public bool IsVisible { get; private set; }
        public bool IsInteractable { get; private set; }
        
        protected virtual bool VisibilityOnAwake => true;
        protected virtual bool InteractableOnAwake => true;
        
        protected event Action ShowComponentEvent;
        protected event Action HideComponentEvent;
        
        private CanvasGroup CanvasGroup => GetComponent(ref canvasGroup);
        private CanvasGroup canvasGroup;
        
        public override void Initialize() {
            base.Initialize();
            model?.Initialize();
            view?.Initialize();
            SetVisibility(VisibilityOnAwake);
            SetInteractable(InteractableOnAwake);
        }

        public override void Dispose() {
            model?.Dispose();
            view?.Dispose();
            base.Dispose();
        }

        public void ShowComponent() {
            SetVisibility(true);
            ShowComponentEvent?.Invoke();
        }

        public void HideComponent() {
            SetVisibility(false);
            HideComponentEvent?.Invoke();
        }
        
        public void SetInteractable(bool isInteractable) {
            IsInteractable = isInteractable;

            if (CanvasGroup != null) {
                CanvasGroup.SetActive(IsVisible, IsInteractable);
            }
        }

        private void SetVisibility(bool isVisible) {
            IsVisible = isVisible;
            
            if (CanvasGroup != null) {
                CanvasGroup.SetActive(isVisible, IsInteractable);
            } else if (GameObject != null) {
                GameObject.SetActive(isVisible);
            }
        }
    }
}