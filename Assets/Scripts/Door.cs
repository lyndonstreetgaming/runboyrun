using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    public GameObject Player;

    public GameObject StageComplete_UI;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StageComplete_UI.SetActive(false);

            animator.SetBool("Open", true);

            StartCoroutine(ClosedDoor());
        }
    }

    IEnumerator ClosedDoor()
    { 
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("Open", false);

        Time.timeScale = 0f;

        Player.SetActive(false);

        StageComplete_UI.SetActive(true);
    }
}
