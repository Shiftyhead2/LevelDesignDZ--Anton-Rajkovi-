using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown ResolutionDropDown;
    public Dropdown GraphicsDropDown;
    public Toggle fullScreenToggle;
    public AudioMixer EnemyAudioMixer;
    public Text VolumeText;
    public Slider VolumeSlider;
    public float CurrentVolume;
    public bool CurrentFullScreenStatus;
    public int CurrentQuality;
    public int CurrentResolution;

    private void Start()
    {
        LoadSettings();

        GraphicsDropDown.value = CurrentQuality;
        GraphicsDropDown.RefreshShownValue();

        fullScreenToggle.isOn = CurrentFullScreenStatus;


        VolumeText.text = string.Format("{0:0}", CurrentVolume) + " dB";
        VolumeSlider.value = CurrentVolume;


    }

    public void SetVolume(float volume)
    {
        CurrentVolume = volume;
        EnemyAudioMixer.SetFloat("Volume", volume);
        VolumeText.text = string.Format("{0:0}", CurrentVolume) + " dB";
    }

    public void SetResolution(int ResolutionIndex)
    {
        CurrentResolution = ResolutionIndex;
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        CurrentQuality = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        CurrentFullScreenStatus = isFullscreen;
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    public  void SaveSettings()
    {
        SaveManager.SaveSettings(this);
    }

    void LoadSettings()
    {
        SettingsData data = SaveManager.LoadSettings();

        if (data != null)
        {
            CurrentVolume = data.volume;
            CurrentFullScreenStatus = data.FullScreen;
            CurrentQuality = data.GraphicsQuality;
            CurrentResolution = data.Resolution;

            resolutions = Screen.resolutions;

            ResolutionDropDown.ClearOptions();
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
            }
            ResolutionDropDown.AddOptions(options);
            ResolutionDropDown.value = CurrentResolution;
            ResolutionDropDown.RefreshShownValue();
        }
        else
        {
            CurrentVolume = 20f;
            CurrentFullScreenStatus = true;
            CurrentQuality = QualitySettings.GetQualityLevel();

            resolutions = Screen.resolutions;

            ResolutionDropDown.ClearOptions();
            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    CurrentResolution = i;
                }


            }
            ResolutionDropDown.AddOptions(options);
            ResolutionDropDown.value = CurrentResolution;
            ResolutionDropDown.RefreshShownValue();
        }
        

    }

}
