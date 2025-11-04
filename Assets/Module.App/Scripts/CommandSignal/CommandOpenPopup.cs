using Module.Core.Scripts.CommandSignal;
using Module.PopupService.Scripts.Addressable;
using Module.PopupService.Scripts.Services;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenPopup : ICommandWithParameter
    {
        [Inject] private IPopupManagerService _popupManagerService;
        [Inject] private readonly IAddressableInjection _injector;


        public async void Execute(ISignal signal)
        {
            var param = (SignalOpenPopup)signal;
            var popup = await _popupManagerService.OpenPopup(param.PopupName);
            await _injector.Initialize(popup, param.Parameters);
        }
    }
}