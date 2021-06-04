using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public GameObject Pause_UI;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        Pause_UI.SetActive(!Pause_UI.activeSelf);

        if (Pause_UI.activeSelf)
        {
            Time.timeScale = 0f;
        }

        else
        {
            Time.timeScale = 1f;
        }
    }
}
