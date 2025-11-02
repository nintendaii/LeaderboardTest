using UnityEngine;
using Zenject;
using System.Reflection;
using UnityEngine.InputSystem;

public class DebugBindings : MonoBehaviour
{
    [Inject] private DiContainer _container;

    private bool _show = false;
    private Vector2 _scroll;

    private void Update()
    {
        if (Keyboard.current.dKey.wasPressedThisFrame)
            _show = !_show;
    }

    private void OnGUI()
    {
        if (!_show) return;

        GUILayout.BeginArea(new Rect(10, 10, 800, 800));
        GUILayout.Label("<b>ZENJECT BINDINGS (F1)</b>", GUILayout.Width(500));
        if (GUILayout.Button("Copy")) GUIUtility.systemCopyBuffer = GetAllBindings();
        GUILayout.EndArea();

        _scroll = GUILayout.BeginScrollView(_scroll, GUILayout.Height(700));

        var bindingInfo = GetAllBindingInfo();
        foreach (var info in bindingInfo)
        {
            GUILayout.Label(info);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private string[] GetAllBindingInfo()
    {
        var lines = new System.Collections.Generic.List<string>();

        var containerField = typeof(DiContainer).GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance);
        var container = containerField?.GetValue(_container);

        if (container == null) return new[] { "<color=red>Failed to access internal container</color>" };

        var bindingsField = container.GetType().GetField("_bindings", BindingFlags.NonPublic | BindingFlags.Instance);
        var bindings = bindingsField?.GetValue(container) as System.Collections.IEnumerable;

        if (bindings == null) return new[] { "<color=red>No bindings found</color>" };

        foreach (var binding in bindings)
        {
            var bindingType = binding.GetType();
            var contractTypes = bindingType.GetProperty("ContractTypes")?.GetValue(binding) as System.Collections.Generic.IEnumerable<System.Type>;
            var provider = bindingType.GetProperty("Provider")?.GetValue(binding);

            if (contractTypes != null && provider != null)
            {
                foreach (var type in contractTypes)
                {
                    lines.Add($"<color=cyan>{type.Name}</color> → <color=lime>{provider.GetType().Name}</color>");
                }
            }
        }

        return lines.ToArray();
    }

    private string GetAllBindings()
    {
        return string.Join("\n", GetAllBindingInfo());
    }
}