using Module.Core.Scripts.CommandSignal;

namespace Module.Bootloader.Scripts.CommandSignal
{
    public class CommandToggleLoadingScreen: ICommandWithParameter
    {
        public void Execute(ISignal signal)
        {
            var param = (SignalToggleLoadingScreen)signal;
        }
    }
}