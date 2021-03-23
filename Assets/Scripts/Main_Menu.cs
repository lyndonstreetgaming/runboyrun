using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    public GameObject MainMenu;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            MainMenu.SetActive(MainMenu.activeSelf);
        }
    }
}
