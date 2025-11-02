using Module.Core.Utilities;
using Zenject;

namespace Module.Core.Scripts.Launcher {
    public abstract class LauncherBase : MonoInstaller {
        
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