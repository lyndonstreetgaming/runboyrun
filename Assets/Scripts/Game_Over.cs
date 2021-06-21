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

    private void Update()
    {
        Player.SetActive(false);

        PlayerUI.SetActive(false);
    }
}
