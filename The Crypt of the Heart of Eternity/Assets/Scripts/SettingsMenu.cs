using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{

    public AudioSource music;
    public Dropdown resdropdown;
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;

        resdropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResindex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height==Screen.currentResolution.height)
            {
                currentResindex = i;
            }
        }
        resdropdown.AddOptions(options);
        resdropdown.value = currentResindex;
        resdropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void setFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }

    public void setResolution(int resindex)
    {
        Resolution resolution = resolutions[resindex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void toggle(bool ismusic)
    {
        music.mute = ismusic;
    }
}
