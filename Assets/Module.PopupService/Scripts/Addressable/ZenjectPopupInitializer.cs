using System.Threading.Tasks;
using SimplePopupManager;
using UnityEngine;
using Zenject;

namespace Module.PopupService.Scripts.Addressable
{
    public class ZenjectPopupInitializer : IAddressableInjection
    {
        private readonly DiContainer _container;

        public ZenjectPopupInitializer(DiContainer container)
        {
            _container = container;
        }

        public async Task Initialize(GameObject popup, object param)
        {
            popup.SetActive(false);
            _container.InjectGameObject(popup);

            var initComponents = popup.GetComponents<IPopupInitialization>();
            foreach (var component in initComponents)
                await component.Init(param);

            popup.SetActive(true);
        }
    }
}