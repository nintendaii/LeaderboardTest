using System.Threading.Tasks;
using UnityEngine;

namespace Module.PopupService.Scripts.Services.Addressable
{
    public interface IAddressableLoader
    {
        Task<GameObject> LoadAsync(string name);
        void Unload(GameObject popup);
    }
}