using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Main_Menu : MonoBehaviour
{
    private bool IsMuted;

    public TMP_Dropdown ScreenResolutionDropdown;

    Resolution[] Resolutions;

    public Scene_Fader SceneFader;

    public string FirstLevel = "Level 1 - Stage 1";

    void Start()
    {
        IsMuted = PlayerPrefs.GetInt("Muted") == 1;

        AudioListener.pause = IsMuted;

        Resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        ScreenResolutionDropdown.ClearOptions();

        List<string> Options = new List<string>();

        int CurrentResolutionIndex = 0;

        for (int i = 0; i < Resolutions.Length; i++)
        {
            string Option = Resolutions[i].width + " x " + Resolutions[i].width;

            Options.Add(Option);

            if (Resolutions[i].width == Screen.width && Resolutions[i].height == Screen.height)
            {
                CurrentResolutionIndex = i;
            }
        }

        ScreenResolutionDropdown.AddOptions(Options);

        ScreenResolutionDropdown.value = CurrentResolutionIndex;

        ScreenResolutionDropdown.RefreshShownValue();
    }

    public void Play()
    {
        SceneFader.FadeTo(FirstLevel); 
    }

    public void Quit()
    {
        Debug.Log("QUIT!");

        Application.Quit();
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }

    public void SetResolution (int ResolutionIndex)
    {
        Resolution Resolution = Resolutions[ResolutionIndex];

        Screen.SetResolution(Resolution.width, Resolution.height, Screen.fullScreen);
    }

    public void FullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }

    public void Mute()
    {
        IsMuted = !IsMuted;

        AudioListener.pause = IsMuted;

        PlayerPrefs.SetInt("Muted", IsMuted ? 1 : 0);
    }

}
