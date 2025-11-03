//© 2023 Sophun Games LTD. All rights reserved.
//This code and associated documentation are proprietary to Sophun Games LTD.
//Any use, reproduction, distribution, or release of this code or documentation without the express permission
//of Sophun Games LTD is strictly prohibited and could be subject to legal action.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.PopupService.Scripts.Services.Addressable;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SimplePopupManager
{
    /// <summary>
    ///     Manages popups, providing functionality for opening, closing, and loading popups.
    /// </summary>
    public class PopupManagerServiceService : IPopupManagerService
    {
        private readonly IAddressableLoader _loader;
        private readonly IAddressableInjection _injector;
        private readonly Dictionary<string, GameObject> _popups = new();

        public PopupManagerServiceService(IAddressableLoader loader, IAddressableInjection injector)
        {
            _loader = loader;
            _injector = injector;
        }

        public async Task<GameObject> OpenPopup(string name, object param)
        {
            if (_popups.ContainsKey(name))
            {
                Debug.LogError($"Popup with name {name} already open.");
                return _popups[name];
            }

            try
            {
                var popup = await _loader.LoadAsync(name);
                await _injector.Initialize(popup, param);
                _popups[name] = popup;
                return popup;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to open popup {name}: {ex}");
                return null;
            }
        }

        public void ClosePopup(string name)
        {
            if (!_popups.TryGetValue(name, out var popup))
                return;

            _loader.Unload(popup);
            _popups.Remove(name);
        }
    }

}