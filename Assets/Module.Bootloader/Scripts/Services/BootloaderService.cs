using Module.Bootloader.Scripts.CommandSignal;
using Module.Core.Scripts.MVC;

namespace Module.Bootloader.Scripts.Services
{
    using System.Threading.Tasks;

    public class BootloaderService : ControllerMonoBase
    {
        private void Start()
        {
            BootApp();
        }

        private async void BootApp()
        {
            SignalBus.Fire(new SignalToggleLoadingScreen(true));
            await InitialiseSystems();
            SignalBus.Fire(new SignalFinishedInitialization());
            SignalBus.Fire(new SignalToggleLoadingScreen(false));
        }

        /// <summary>
        /// Function for initializing systems
        /// </summary>
        private async Task InitialiseSystems()
        {
            // Short delay for testing purposes so the reviewer sees the spinner
            // In real project sets up all systems required for the app to work. Might be needed to fire signals related to systems initialization
            await Task.Delay(1000);
        }
    }
}