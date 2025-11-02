using System;
using UnityEngine;
using Zenject;

namespace Module.Core.Scripts.MVC
{
    public class MVCComponent<TModel, TView, TController> : MonoBehaviour
        where TModel : ModelBase
        where TView : ViewBase
        where TController : ControllerBase<TModel, TView>
    {
        private TController _controller;

        [Inject]
        public void Construct(TModel model, TView view, SignalBus signalBus, DiContainer container)
        {
            _controller = Activator.CreateInstance(typeof(TController), model, view, signalBus, container) as TController;
            _controller?.Initialize();
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}