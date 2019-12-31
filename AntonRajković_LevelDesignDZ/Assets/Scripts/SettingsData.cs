using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float volume;
    public bool FullScreen;
    public int GraphicsQuality;
    public int Resolution;

    public SettingsData(SettingsMenu settings)
    {
        volume = settings.CurrentVolume;
        FullScreen = settings.CurrentFullScreenStatus;
        GraphicsQuality = settings.CurrentQuality;
        Resolution = settings.CurrentResolution;
    }
}
