using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    private bool IsMuted;

    public Dropdown dropdown;

    Resolution[] resolution;

    private void Start()
    {
        IsMuted = PlayerPrefs.GetInt("Muted") == 1;

        AudioListener.pause = IsMuted;

        resolution = Screen.resolutions;

        dropdown.ClearOptions();

        List<string> Options = new List<string>();

        for (int i = 0; i < resolution.Length; i++)
        {
            string Option = resolution[i].width + " x " + resolution[i].height;

            Options.Add(Option);

        }

        dropdown.AddOptions(Options);

    }

    public void Play()
    {
        SceneManager.LoadScene("Level 1 - Stage 1"); 
    }

    public void Quit()
    {
        Debug.Log("QUIT!");

        Application.Quit();
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
