using Module.Core.CommandSignal;
using SimplePopupManager;
using Zenject;

namespace Module.App.Scripts.CommandSignal
{
    public class CommandOpenPopup: ICommandWithParameter
    {
        [Inject] private readonly IPopupManagerService _popupManagerService;
        [Inject] private readonly DiContainer _diContainer;
        public async void Execute(ISignal signal)
        {
            var param = (SignalOpenPopup)signal;
            var go = await _popupManagerService.OpenPopup(param.PopupName, param.Parameters);
            _diContainer.InjectGameObject(go);
        }
    }
}