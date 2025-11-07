using System;
using System.Collections.Generic;
using Dino.UtilityTools.Singleton;
using UnityEngine;
using UnityEngine.Events;

public class ResponsiveManager : Singleton<ResponsiveManager>
{
    private Vector2 _lastScreenSize;

    public ScreenOrientation CurrentOrientation => GetScreenOrientation();
    public DeviceType CurrentDeviceType { get => GetDeviceTypeByResolution(Screen.width, Screen.height); }
    public bool IsPortrait() => Screen.width < Screen.height;
    public bool IsLandscape() => Screen.width > Screen.height;

    public Vector2 CurrentScreenSize => new Vector2(Screen.width, Screen.height);
    public UnityEvent OnScreenSizeChanged { get; private set; } = new UnityEvent();


    private void OnEnable()
    {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        Application.onBeforeRender += CheckScreenSizeChange;
    }

    private void OnDisable()
    {
        Application.onBeforeRender -= CheckScreenSizeChange;
    }

    private void Start()
    {
    }

    private void CheckScreenSizeChange()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if(_lastScreenSize != currentScreenSize)
        {
            _lastScreenSize = currentScreenSize;
            OnScreenSizeChanged?.Invoke();
        }
    
    }
    private ScreenOrientation GetScreenOrientation()
    {
        return IsPortrait() ? ScreenOrientation.Portrait : ScreenOrientation.Landscape;
    }

    private DeviceType GetDeviceTypeByResolution(int width, int height)
    {
        float aspectRatio = (float)Math.Max(width, height) / Mathf.Min(width, height);
        int minDimension = Math.Min(width, height);

        if (minDimension >= 600 && aspectRatio < 2.0f)
            return DeviceType.Tablet;
        else
            return DeviceType.Mobile;
    }
}

public enum ScreenOrientation { Portrait, Landscape }

public enum DeviceType { Mobile, Tablet}