using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour 
{
    [SerializeField] private Transform _windowsContainer;

    private Dictionary<System.Type, WindowBase> _allWindows;

    private void Awake()
    {
        _allWindows = new Dictionary<System.Type, WindowBase>();

        WindowBase[] windows = _windowsContainer.GetComponentsInChildren<WindowBase>(true);
        for (int i = 0; i < windows.Length; i++)
        {
            var window = windows[i];

            window.Init();
            _allWindows.Add(window.GetType(), window);
        }
    }

    public void Show<T>(object info = null) where T : WindowBase
    {
        if (_allWindows.ContainsKey(typeof(T)))
        {
            WindowBase window = _allWindows[typeof(T)];

            if (window is UpdatableWindowBase)
            {
                UpdatableWindowBase updatable = (UpdatableWindowBase)window;
                updatable.UpdateInfo(info);
            }

            window.Show();
        }
        else
            Debug.LogError("There is NO window with type " + typeof(T).ToString());
    }

    public void Hide<T>() where T : WindowBase
    {
        WindowBase window = _allWindows[typeof(T)];
        window.Hide();
    }
}
