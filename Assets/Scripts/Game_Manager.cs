using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    private void Awake()
    {
        Instance = this;

        SceneManager.LoadSceneAsync((int)Scene_Indexes.Main_Menu, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync((int)Scene_Indexes.Main_Menu);

        SceneManager.LoadSceneAsync((int)Scene_Indexes.Level1_Stage1, LoadSceneMode.Additive);
    }
}
