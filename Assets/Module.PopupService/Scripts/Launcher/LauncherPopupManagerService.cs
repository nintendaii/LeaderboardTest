using Module.Core.Scripts.Launcher;
using SimplePopupManager;

namespace Module.PopupService.Scripts.Launcher
{
    public class LauncherPopupManagerService: LauncherInstaller
    {
        protected override void InstallComponents()
        {
            base.InstallComponents();
            Container.Bind<IPopupManagerService>()
                .To<PopupManagerServiceService>()
                .AsSingle();
        }
    }
}