using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Dino.UtilityTools.Singleton;

public class UIManager : Singleton<UIManager>
{
    // List of all UI windows 
    [SerializeField] private List<UIWindow> uiWindows = new List<UIWindow>();
    
    /// <summary>
    ///  Shows the UI window with the specified identifier.
    /// </summary>
    /// <param name="windowUI"> Window identifier</param>
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
    
    /// <summary>
    ///  Hides the UI window with the specified identifier.
    /// </summary>
    /// <param name="windowUI"> Window identifier</param>
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
    
    /// <summary>
    /// Hides all UI windows.
    /// </summary>
    public void HideAllUI()
    {
        foreach (var window in uiWindows)
        {
            window.Hide();
        }
    }

    /// <summary>
    ///  Retrieves the UI window with the specified identifier.
    /// </summary>
    /// <param name="windowUI"></param>
    /// <returns></returns>
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

    #region Editor
    [Button]
    private void GetAllUIWindows()
    {
        //uiWindows.Clear();
        //UIWindow[] windows = FindObjectsByType<UIWindow>(FindObjectsSortMode.InstanceID);
        //uiWindows.AddRange(windows);
    }

    #endregion
    
}

public static class WindowsIDs
{
    public static string Popup = "PopupUI";
    public static string Inventory = "InventoryUI";

}