using Module.Core.CommandSignal;

namespace Module.App.Scripts.CommandSignal
{
    public class SignalOpenPopup: ISignal
    {
        public string PopupName;

        public object Parameters;
        public SignalOpenPopup(string popupName, object parameters)
        {
            PopupName = popupName;
            Parameters = parameters;
        }
    }
}