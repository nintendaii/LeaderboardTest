using Module.Core.CommandSignal;

namespace Module.App.Scripts.CommandSignal
{
    public class SignalClosePopup: ISignal
    {
        public string PopupName;

        public SignalClosePopup(string popupName)
        {
            PopupName = popupName;
        }
    }
}