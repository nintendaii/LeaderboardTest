using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Module.PopupService.Scripts.Addressable
{
    public class AddressablePopupLoader : IAddressableLoader
    {
        public async Task<GameObject> LoadAsync(string name)
        {
            var handle = Addressables.InstantiateAsync(name);
            await handle.Task;
            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new System.Exception($"Failed to load popup: {name}");
            return handle.Result;
        }

        public void Unload(GameObject popup)
        {
            Addressables.ReleaseInstance(popup);
        }
    }
}