namespace Module.Core.Scripts.MVC {
    public abstract class ModelBase : Zenject.IInitializable, System.IDisposable {
        public virtual void Initialize() { }

        public virtual void Dispose() { }
    }
}