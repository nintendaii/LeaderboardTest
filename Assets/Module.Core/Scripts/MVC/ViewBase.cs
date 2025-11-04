namespace Module.Core.Scripts.MVC {
    
    public abstract class ViewBase : Zenject.IInitializable, System.IDisposable {
        public virtual void Initialize() { }

        public virtual void Dispose() { }
    }
}