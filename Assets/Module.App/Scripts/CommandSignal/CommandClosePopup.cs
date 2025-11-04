using Module.Core.Scripts.CommandSignal;
using Module.PopupService.Scripts.Services;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandClosePopup: ICommandWithParameter
    {
        [Inject] private readonly IPopupManagerService _popupManagerService;

        public void Execute(ISignal signal)
        {
            var param = (SignalClosePopup) signal;
            _popupManagerService.ClosePopup(param.PopupName);
        }
    }
}