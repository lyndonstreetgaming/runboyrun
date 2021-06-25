using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public GameObject PlayerUI;

    public GameObject Player;

    [SerializeField]

    private AudioSource GameOver;

    [SerializeField]

    private float LoadingDelay = 15f;

    [SerializeField]

    private float TimeElasped;

    public Level_Changer level_changer;

    private void Update()
    {
        Player.SetActive(false);

        PlayerUI.SetActive(false);

        TimeElasped += Time.deltaTime;

        if (TimeElasped > LoadingDelay)
        {
            level_changer.FadeTo((int)Scene_Indexes.Main_Menu, LoadSceneMode.Single);
        }
    }
}