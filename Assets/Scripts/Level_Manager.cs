﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    public float RespawnDelay;

    public PlayerController GamePlayer;

    [SerializeField]

    private AudioSource Trap;
   
    void Start()
    {
        GamePlayer = FindObjectOfType<PlayerController>();
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");

        Trap.Play();

    }

    public IEnumerator RespawnCoroutine()
    {
        GamePlayer.gameObject.SetActive (false);

        yield return new WaitForSeconds(RespawnDelay);

        GamePlayer.transform.position = GamePlayer.RespawnPoint;

        GamePlayer.gameObject.SetActive(true);
    }
}
