//© 2023 Sophun Games LTD. All rights reserved.
//This code and associated documentation are proprietary to Sophun Games LTD.
//Any use, reproduction, distribution, or release of this code or documentation without the express permission
//of Sophun Games LTD is strictly prohibited and could be subject to legal action.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.PopupService.Scripts.Addressable;
using UnityEngine;

namespace Module.PopupService.Scripts.Services
{
    /// <summary>
    ///     Manages popups, providing functionality for opening, closing, and loading popups.
    /// </summary>
    public class PopupManagerService : IPopupManagerService
    {
        private readonly IAddressableLoader _loader;
        private readonly Dictionary<string, GameObject> _popups = new();

        public PopupManagerService(IAddressableLoader loader)
        {
            _loader = loader;
        }

        public async Task<GameObject> OpenPopup(string name)
        {
            if (_popups.TryGetValue(name, out var existingPopup))
            {
                Debug.LogWarning($"Popup '{name}' is already open.");
                return existingPopup;
            }

            try
            {
                var popup = await _loader.LoadAsync(name);
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