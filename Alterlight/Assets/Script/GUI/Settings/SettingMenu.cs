using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using Michsky.UI.Shift;

public class SettingMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    public Button applyButton;
    public SwitchManager SwitchManager;

    public AudioMixer AudioMixer;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionsIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionsIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionsIndex;
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log(resolution.width + "x" + resolution.height);
    }

    public void setVolume(float volume)
    {
        AudioMixer.SetFloat("volume", volume);
    }

    public void setFullscreen(bool isFullscreen)
    {
        // if SwitchManager is on, set bool to true
        if (SwitchManager.isOn)
        {
            Screen.fullScreen = true;
        }
        // if SwitchManager is off, set bool to false
        else
        {
            Screen.fullScreen = false;
        }

    }
}
