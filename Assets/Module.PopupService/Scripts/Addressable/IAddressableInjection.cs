using System.Threading.Tasks;
using UnityEngine;

namespace Module.PopupService.Scripts.Addressable
{
    public interface IAddressableInjection
    {
        Task Initialize(GameObject popup, object param);
    }
}