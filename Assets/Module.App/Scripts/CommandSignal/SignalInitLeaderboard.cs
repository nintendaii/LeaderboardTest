using Module.Core.Scripts.CommandSignal;
using UnityEngine;

namespace Module.App.Scripts.CommandSignal
{
    public class SignalInitLeaderboard: ISignal
    {
        public object Parameters;
        public Transform ParentTransform;
        public SignalInitLeaderboard(object param, Transform transform)
        {
            Parameters = param;
            ParentTransform = transform;
        }
    }
}