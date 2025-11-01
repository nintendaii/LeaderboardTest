namespace Module.Core.Scripts.Launchers {
    public abstract class LauncherInstaller : LauncherBase {
        public override void InstallBindings() {
            InstallCommandSignals();
            InstallServices();
            InstallComponents();
            InstallFactory();
        }
        
        protected virtual void InstallCommandSignals() { }
        protected virtual void InstallServices() { }
        protected virtual void InstallComponents() { }
        protected virtual void InstallFactory() { }
    }
}