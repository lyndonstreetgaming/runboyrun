using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField]

    private Animator DoorAnimationController;

    [SerializeField]

    private GameObject Player;

    [SerializeField]

    private GameObject StageComplete_UI;

    private void Start()
    {
        DoorAnimationController = GetComponent<Animator>();

        DoorAnimationController.SetBool("Open", true);

        StageComplete_UI.SetActive(false);

        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoorAnimationController.SetBool("Open", false);

            Player.SetActive(false);

            StageComplete_UI.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
