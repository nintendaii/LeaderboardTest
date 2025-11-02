namespace Module.Core.MVC {
    public abstract class ControllerBase : Zenject.IInitializable, System.IDisposable {
        protected Zenject.DiContainer Container { get; private set; }
        protected Zenject.SignalBus SignalBus { get; private set; }
        
        public virtual void Initialize() { }

        public virtual void Dispose() { }

        [Zenject.Inject]
        private void Construct(Zenject.DiContainer container, Zenject.SignalBus signalBus) {
            Container = container;
            SignalBus = signalBus;
            
            Initialize();
        }
    }
}