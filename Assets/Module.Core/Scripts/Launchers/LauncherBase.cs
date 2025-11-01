using System;
using Module.Core.Utilities;
using Zenject;

namespace Module.Core.Scripts.Launchers {
    public abstract class LauncherBase : MonoInstaller {
        protected void RegisterComponents<T>() where T : IBindComponentInHierarchy {
            var types = Helper.Assembly.GetTypeListWithInterface<T>(false);
            foreach (var type in types) {
                var binder = Container.Bind(type, typeof(IDisposable)).To(type).FromComponentInHierarchy();
                if (type.ContainsInterface<IBindComponentFlagAsSingle>()) {
                    binder.AsSingle();
                }

                if (type.ContainsInterface<IBindComponentFlagNonLazy>()) {
                    binder.NonLazy();
                }
            }
        }
        
        protected void RegisterSubclass<T>() {
            var types = Helper.Assembly.GetSubclassListThroughHierarchy<T>(false);
            foreach (var type in types) {
                var binder = Container.Bind(type);
                binder.AsSingle();
                binder.NonLazy();
            }
        }
    }
}