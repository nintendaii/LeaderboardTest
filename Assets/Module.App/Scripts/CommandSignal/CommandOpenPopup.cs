using Module.Core.CommandSignal;
using SimplePopupManager;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenPopup : ICommandWithParameter
    {
        [Inject] private IPopupManagerService _popupManagerService;

        public async void Execute(ISignal signal)
        {
            var param = (SignalOpenPopup)signal;
            await _popupManagerService.OpenPopup(param.PopupName, param.Parameters);
        }
    }

}