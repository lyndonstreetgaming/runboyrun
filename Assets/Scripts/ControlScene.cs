using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadScene();
        }

        void LoadScene()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
