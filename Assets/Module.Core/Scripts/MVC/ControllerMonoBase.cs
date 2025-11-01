namespace Module.Core.Scripts.MVC {
    public abstract class ControllerMonoBase : UnityEngine.MonoBehaviour, Zenject.IInitializable, System.IDisposable {
        public UnityEngine.Transform Transform { get; private set; }
        public UnityEngine.GameObject GameObject { get; private set; }
        
        protected Zenject.DiContainer Container { get; private set; }
        protected Zenject.SignalBus SignalBus { get; private set; }
        
        public virtual void CheckAssert() { }
        
        public virtual void Initialize() { }

        public virtual void Dispose() { }

        protected T GetComponent<T>(ref T component) => component ??= GetComponent<T>();

        protected T[] GetComponents<T>(ref T[] components) => components ??= GetComponents<T>();
        
        [Zenject.Inject]
        private void Construct(Zenject.DiContainer container, Zenject.SignalBus signalBus) {
            Container = container;
            SignalBus = signalBus;

            Transform = transform;
            GameObject = gameObject;

            CheckAssert();
            Initialize();
        }

        private void OnDestroy() {
            Dispose();
        }
    }
}