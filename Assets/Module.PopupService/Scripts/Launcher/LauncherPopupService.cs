using Module.Core.Scripts.Launcher;
using Module.PopupService.Scripts.Addressable;
using Module.PopupService.Scripts.Services;

namespace Module.PopupService.Scripts.Launcher
{
    public class LauncherPopupService: LauncherInstaller
    {
        protected override void InstallComponents()
        {
            base.InstallComponents();
            Container.Bind<IPopupManagerService>()
                .To<PopupManagerService>()
                .AsSingle();
            Container.Bind<IAddressableLoader>().
                To<AddressablePopupLoader>().
                AsSingle();
            Container.Bind<IAddressableInjection>().
                To<ZenjectPopupInitializer>().
                AsSingle();
        }
    }
}