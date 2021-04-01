﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level 1 - Stage 1");
    }

    public void Quit()
    {
        Debug.Log("QUIT!");

        Application.Quit();
    }
}
