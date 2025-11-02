using UnityEngine;
using Zenject;

namespace Module.Core.Scripts.MVC
{
    public abstract class ViewBase : MonoBehaviour, IInitializable, System.IDisposable
    {
        public virtual void Initialize() { }
        public virtual void Dispose() { }
    }
}