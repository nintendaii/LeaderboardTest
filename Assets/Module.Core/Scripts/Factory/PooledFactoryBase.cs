using System.Collections.Generic;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.Pool;

namespace Module.App.Scripts.Factory
{
    public abstract class PooledFactoryBase<T> : ControllerMonoBase where T : ControllerMonoBase, IFactoryUnitResettable, IShowComponent, IHideComponent
    {
        protected IObjectPool<T> _objectPool;
        protected readonly List<T> _activeItems = new();

        public override void Initialize()
        {
            base.Initialize();
            _objectPool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
        }

        public override void Dispose()
        {
            _objectPool.Clear();
            base.Dispose();
        }

        public virtual T CreateItem(Transform parent = null)
        {
            var item = _objectPool.Get();
            _activeItems.Add(item);
            if (parent != null)
            {
                item.Transform.SetParent(parent);
                item.Transform.localScale = Vector3.one;
            }
            return item;
        }

        public virtual void ReleaseItem(T item)
        {
            _objectPool.Release(item);
            item.ResetUnit();
        }

        /// <summary>
        /// Releases pooled objects for caching
        /// </summary>
        public virtual void ReleaseContainer()
        {
            foreach (var item in _activeItems)
            {
                item.Transform.SetParent(Transform);
                ReleaseItem(item);
            }
            _activeItems.Clear();
        }
        
        public virtual void DestroyAll()
        {
            foreach (var item in _activeItems)
                Destroy(item.gameObject);
            _activeItems.Clear();

            _objectPool.Clear();
        }

        protected abstract T CreatePooledItem();
        protected virtual void OnTakeFromPool(T item) { item.ShowComponent(); }
        protected virtual void OnReturnedToPool(T item) {  item.HideComponent();}
        protected virtual void OnDestroyPoolObject(T item) { Destroy(item.gameObject); }
    }
}
