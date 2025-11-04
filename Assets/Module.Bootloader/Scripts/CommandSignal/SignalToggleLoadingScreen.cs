using Module.Core.CommandSignal;

namespace Module.Bootloader.Scripts.CommandSignal
{
    public class SignalToggleLoadingScreen: ISignal
    {
        public bool IsActive;

        public SignalToggleLoadingScreen(bool isActive)
        {
            IsActive = isActive;
        }
    }
}