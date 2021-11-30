using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public GameObject Pause_UI;

    public static bool IsPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        Pause_UI.SetActive(false);

        Time.timeScale = 1f;

        IsPaused = false;
    }


    private void Pause()
    {
        Pause_UI.SetActive(true);

        Time.timeScale = 0f;

        IsPaused = true;
    }
}
