using System;

namespace Module.Core.MVC {
    
    public abstract class ViewBase : Zenject.IInitializable, System.IDisposable {
        public virtual void Initialize() { }

        public virtual void Dispose() { }
    }
}