using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField]

    private Animator CheckpointAnimationController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointAnimationController.SetBool("CheckpointTriggered", true);
        }

        
    }
}