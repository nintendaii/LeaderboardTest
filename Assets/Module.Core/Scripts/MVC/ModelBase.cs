using Zenject;

namespace Module.Core.Scripts.MVC
{
    public abstract class ModelBase : IInitializable, System.IDisposable
    {
        public virtual void Initialize() { }
        public virtual void Dispose() { }
    }
}