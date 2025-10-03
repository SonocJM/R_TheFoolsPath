using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Dino.UtilityTools.Singleton;

public class UIManager : Singleton<UIManager>
{
    // List of all UI windows 
    [SerializeField] private List<UIWindow> uiWindows = new List<UIWindow>();
    
    public void ShowUI(string windowUI)
    {
        foreach (var window in uiWindows)
        {
            if (window.WindowUI == windowUI)
            {
                window.Show();
                return;
            }
        }
        Debug.LogWarning($"UI Window with name {windowUI} not found.");
    }
    
    public void HideUI(string windowUI)
    {
        foreach (var window in uiWindows)
        {
            if (window.WindowUI == windowUI)
            {
                window.Hide();
                return;
            }
        }
        Debug.LogWarning($"UI Window with name {windowUI} not found.");
    }
    
    /// Hides all UI windows.
    public void HideAllUI()
    {
        foreach (var window in uiWindows)
        {
            window.Hide();
        }
    }

    ///  Retrieves the UI window with the specified identifier.

    public UIWindow GetUIWindow(string windowUI)
    {
        foreach (var window in uiWindows)
        {
            if (window.WindowUI == windowUI)
            {
                return window;
            }
        }
        Debug.LogWarning($"UI Window with name {windowUI} not found.");
        return null;
    }
    
}

public static class WindowsIDs
{
    public static string Popup = "PopupUI";
    public static string Inventory = "InventoryUI";

}