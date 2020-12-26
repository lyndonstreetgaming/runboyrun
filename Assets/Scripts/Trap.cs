using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private PlayerController Player;

    [SerializeField]

    private AudioSource trap;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Damage(1);

            StartCoroutine(Player.Reaction(0.02f, 350, Player.transform.position));

            trap.Play();
        
        }
    }
}
